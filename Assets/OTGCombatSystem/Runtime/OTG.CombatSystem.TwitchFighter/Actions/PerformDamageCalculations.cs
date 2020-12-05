
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class PerformDamageCalculations : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.CombatBased;
            m_processType = E_ProcessType.Calculation;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            AnimationHandler animHandler = _controller.Handler_Animation;
            CollisionHandler collisionHandler = _controller.Handler_Collision;
            TwitchMovementParams twitchMove = _controller.Handler_Movement.TwitchParams;

            int faceDirection = DetermineFacingDirection(twitchMove);
            IDamagePayload payload = PrepareDamagePayload(animHandler, faceDirection);
            SendDamageToContacts(collisionHandler, payload);
            ResetNumberOfContacts(collisionHandler);

        }
        private int DetermineFacingDirection(TwitchMovementParams _moveParams)
        {
            int faceDirection = 1;

            if(_moveParams.Comp_Transform.rotation.eulerAngles.y == 0)
            {
                faceDirection= 1;
            }
            else if(_moveParams.Comp_Transform.rotation.eulerAngles.y == 180)
            {
                faceDirection = -1;
            }
            return faceDirection;
        }
        private IDamagePayload PrepareDamagePayload(AnimationHandler _animHandler, int _faceDirection)
        {
            TwitchFighterDamagePayload dPayload = new TwitchFighterDamagePayload(_animHandler.CurrentAnimData, _faceDirection);

            return dPayload;
        }
        private void ResetNumberOfContacts(CollisionHandler _colHandler)
        {
            _colHandler.NumberOfContacts = 0;
           
        }
        private void SendDamageToContacts(CollisionHandler collisionHandler, IDamagePayload _payload)
        {
            for(int i = 0; i < collisionHandler.NumberOfContacts; i++)
            {
                OTGHitColliderController hitCollider = collisionHandler.ScanResults[i].GetComponent<OTGHitColliderController>();
                if(hitCollider != null)
                {
                    hitCollider.OnDamageRecieved(_payload);

                }
            }
        }
    }
}