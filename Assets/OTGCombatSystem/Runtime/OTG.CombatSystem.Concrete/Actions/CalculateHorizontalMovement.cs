
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.Concrete
{
    [CreateAssetMenu]
    public class CalculateHorizontalMovement : OTGCombatAction
    {
      
        private void Awake()
        {
            
            m_combatActionType = E_ActionType.Misc;
            m_processType = E_ProcessType.Perform;
            Debug.Log("Became enabled");
        }
       
        public override void Act(OTGCombatSMC _controller)
        {
            MovementHandler moveHandler = _controller.Handler_Movement;
            InputHandler inputHandle = _controller.Handler_Input;

            moveHandler.DesiredHorizontalDistance = inputHandle.MovementVector.x * moveHandler.Data.HorizontalMoveSpeed;
            moveHandler.CurrentHorizontalPosition = Mathf.MoveTowards(moveHandler.CurrentHorizontalPosition, moveHandler.DesiredHorizontalDistance, Time.deltaTime);
        }
    }
}