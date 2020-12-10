#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{

    public class HasComboCountValue : TwitchFighterBaseTransition
    {
        [SerializeField] private int m_desiredComboValue;
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            
            TwitchFighterCombatParams twitchCOmbat = _controller.Handler_Combat.TwitchCombat;
            return twitchCOmbat.CurrentComboCount == m_desiredComboValue;
        }
    }
}
