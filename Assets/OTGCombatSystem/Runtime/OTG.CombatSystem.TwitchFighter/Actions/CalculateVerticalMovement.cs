

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class CalculateVerticalMovement : TwitchFighterBaseAction
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

            bool isGrounded = twitch.Comp_CharacterControl.isGrounded;

            if(isGrounded)
            {
                ApplyGravity(twitch);
            }
            else
            {
                ApplyStickingForce(twitch);
            }
        }
        private void ApplyGravity(TwitchMovementParams _twitchMove)
        {
            _twitchMove.VerticalSpeed = -_twitchMove.GlobalCombatConfig.GravitySetting;
        }
        private void ApplyStickingForce(TwitchMovementParams _twitchMove)
        {
            _twitchMove.VerticalSpeed = -_twitchMove.GlobalCombatConfig.StickingForce;
        }
        private void ApplyJumpingForce()
        {

        }
    }
}
