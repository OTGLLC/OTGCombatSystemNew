

using OTG.CombatSM.Core;

namespace OTG.CombatSM.TwitchFighter
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

            twitch.DesiredDashSpeed = twitch.Data.DashSpeed;
            twitch.DesiredDashDistance = twitch.Data.DashDistance;
            twitch.DashStartPosition = twitch.Comp_Transform.position;
        }
    }
}
