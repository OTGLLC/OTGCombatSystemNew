

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class AquireTarget : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;
            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;

            float dashDirection = EstablishDirection(twitchInput);

            EstablishDirectionCorrectedDashSpeed(twitch, dashDirection);
            ScanForNearestTarget(dashDirection);

            if(twitchCombat.NearestTarget == null)
            {
                SetTargetToMaxDashDistance(twitch, twitchCombat);
            }
            
        }
        private float EstablishDirection(TwitchFighterInput _twitchInput)
        {
            float dashDirection = 1;
            if (_twitchInput.HasLeftInput)
                dashDirection = -1;


            return dashDirection;
        }
        private void EstablishDirectionCorrectedDashSpeed(TwitchMovementParams twitch, float _direction)
        {
            twitch.HorizontalSpeed = twitch.Data.DashSpeed * _direction;
        }
        private void ScanForNearestTarget(float _dashDirection)
        {

        }
        private void SetTargetToMaxDashDistance(TwitchMovementParams _moveParams, TwitchFighterCombatParams _combatParams)
        {
            Vector3 target = new Vector3(_moveParams.Comp_Transform.position.x + _moveParams.DesiredDashDistance,
                                          _moveParams.Comp_Transform.position.y,
                                          _moveParams.Comp_Transform.position.z);

            _moveParams.DesiredDashDistance = _moveParams.Data.MaxDashDistance;
            _combatParams.NearestTargetPosition = target;
        }
    }
}

