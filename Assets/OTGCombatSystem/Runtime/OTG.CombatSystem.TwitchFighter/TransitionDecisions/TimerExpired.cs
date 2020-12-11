#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class TimerExpired : TwitchFighterBaseTransition
    {
        [SerializeField] private float m_maxTime;
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Status;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            AnimationHandler animHandler = _controller.Handler_Animation;

            return animHandler.StateTime >= m_maxTime;
        }
    }
}
