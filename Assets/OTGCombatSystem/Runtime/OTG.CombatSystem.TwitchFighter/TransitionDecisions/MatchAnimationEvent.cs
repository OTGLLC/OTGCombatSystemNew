#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class MatchAnimationEvent : TwitchFighterBaseTransition
    {
        [SerializeField] private OTGAnimationEvent m_eventToMatch;
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;

            return _controller.Handler_Animation.CurrentAnimationEvent == m_eventToMatch;

        }
    }
}