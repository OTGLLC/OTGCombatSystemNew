#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class SetComboBreakPoint : TwitchFighterBaseAction
    {
        [SerializeField] private int m_breakPoint;
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;

            twitchCombat.CurrentComboBreakPoint = m_breakPoint;
        }
    }
}
