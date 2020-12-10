
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class DebugPauseError : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.Debuging;
            m_processType = E_ProcessType.Debuging;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            Debug.LogError("Pausing Editor");

        }
    }
}


