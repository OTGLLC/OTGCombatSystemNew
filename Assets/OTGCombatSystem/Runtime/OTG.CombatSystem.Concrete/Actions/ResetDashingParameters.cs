
using OTG.CombatSM.Core;


namespace OTG.CombatSM.Concrete
{
    public class ResetDashingParameters : OTGCombatAction
    {
        private void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased_Twitch;
            m_processType = E_ProcessType.SetParameter;
        }
        public override void Act(OTGCombatSMC _controller)
        {
            _controller.Handler_Movement.TwitchParams.ResetParams();
        }
    }
}
