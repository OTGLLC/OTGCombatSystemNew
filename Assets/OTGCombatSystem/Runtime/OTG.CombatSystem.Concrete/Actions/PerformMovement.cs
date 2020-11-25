
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.Concrete
{
   
    public class PerformMovement : OTGCombatAction
    {
        private void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.Perform;
        }
        public override void Act(OTGCombatSMC _controller)
        {
            MovementHandler moveHandler = _controller.Handler_Movement;

            Vector3 movement = new Vector3(
                moveHandler.CurrentHorizontalPosition,
                0,
                0
                );

            moveHandler.Comp_CharControl.Move(movement);
        }
    }
}