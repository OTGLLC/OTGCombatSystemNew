
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class DoesNotHaveAdjacentTarget : TwitchFighterBaseTransition
    {
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            return (_controller.Handler_Collision.TargetingController.ValidTargets == 0);

        }
    }
}

