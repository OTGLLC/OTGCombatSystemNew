
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
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;
            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;
            CollisionHandler collisionHandler = _controller.Handler_Collision;

            IDamagePayload payload = PrepareDamagePayload();
            SendDamageToContacts(collisionHandler, payload);
            ResetNumberOfContacts(collisionHandler);

        }
        private IDamagePayload PrepareDamagePayload()
        {
            TwitchFighterDamagePayload dPayload = new TwitchFighterDamagePayload();

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