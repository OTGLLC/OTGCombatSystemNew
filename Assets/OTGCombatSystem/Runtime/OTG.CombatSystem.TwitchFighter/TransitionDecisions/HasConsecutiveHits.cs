#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class HasConsecutiveHits : TwitchFighterBaseTransition
    {
        [SerializeField] private int m_hitCount;
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            CombatHandler combatHand = _controller.Handler_Combat;

            return combatHand.ConsecutiveHit == m_hitCount; 

        }
    }
}

