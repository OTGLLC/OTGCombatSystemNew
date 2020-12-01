
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class HasReachedDesiredDashDistance : TwitchFighterBaseTransition
    {
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;

            return (Mathf.Abs(twitch.DesiredDashDistance) - Mathf.Abs(twitch.CurrentDashDistance) <= twitch.Data.DashDistanceMinThreshold);
        }
    }
}
