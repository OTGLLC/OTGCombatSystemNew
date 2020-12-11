#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;


namespace OTG.TwitchFighter
{
    public class PostComboCounterUpdateEvent : TwitchFighterBaseAction
    {
        [SerializeField] private ComboCounterUpdatedEvent m_event;
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.EventBased;
            m_processType = E_ProcessType.Perform;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;
            m_event.Raise(twitchCombat.CurrentComboCount);

        }
    }
}

