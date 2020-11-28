
using OTG.CombatSM.Core;

namespace OTG.CombatSM.Concrete
{
    public class HasReachedDesiredDashDistance : OTGTransitionDecision
    {
        private void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitchParams = _controller.Handler_Movement.TwitchParams;

            if (twitchParams.DesiredDashDistance - twitchParams.CurrentDashDistance <= 0.1f)
                return true;
            else
                return false;
        }
    }
}
