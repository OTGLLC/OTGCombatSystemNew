#pragma warning disable CS0649


using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class HasComboLevelValue : TwitchFighterBaseTransition
    {
        [SerializeField] private int m_desiredComboLevel;
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            TwitchFighterCombatParams twitchCOmbat = _controller.Handler_Combat.TwitchCombat;

            return twitchCOmbat.ComboLevel == m_desiredComboLevel;
        }
    }
}
