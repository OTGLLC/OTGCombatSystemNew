

using OTG.CombatSM.Core;

namespace OTG.TwitchFighter
{
    public class SetDashDistance:TwitchFighterBaseAction
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

            twitch.ResetParams();

            float direction = 0;
            if (_controller.Handler_Input.TwitchInput.HasLeftInput)
                direction = -1;
            else if (_controller.Handler_Input.TwitchInput.HasRightInput)
                direction = 1;

            twitch.DesiredDashSpeed = twitch.Data.DashSpeed * direction;
            twitch.DesiredDashDistance = twitch.Data.MaxDashDistance;
            twitch.DashStartPosition = twitch.Comp_Transform.position;
        }
    }
}
