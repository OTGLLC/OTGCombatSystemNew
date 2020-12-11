
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class IncrementComboCounter : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;

            twitchCombat.CurrentComboCount++;
            twitchCombat.CurrentComboCount %= twitchCombat.CurrentComboBreakPoint;

        }
    }
}
