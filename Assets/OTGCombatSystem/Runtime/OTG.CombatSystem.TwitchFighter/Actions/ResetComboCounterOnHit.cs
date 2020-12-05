

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class ResetComboCounterOnHit : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {

            _controller.Handler_Combat.ResetComboCounter();
        }
    }
}
