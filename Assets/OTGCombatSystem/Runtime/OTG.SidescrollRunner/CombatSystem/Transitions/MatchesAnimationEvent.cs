#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.InfiniteRunner
{
    public class MatchesAnimationEvent : OTGTransitionDecision
    {
        [SerializeField] private OTGAnimationEvent m_event;
        public override bool Decide(OTGCombatSMC _controller)
        {
            AnimationHandler animHandler = _controller.Handler_Animation;
            return m_event == animHandler.CurrentAnimationEvent;
        }
    }

}

