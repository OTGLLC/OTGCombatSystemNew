
using UnityEngine;
using OTG.CombatSM.Core;


namespace OTG.InfiniteRunner
{
    public class PerformMovement : OTGCombatAction
    {
        public override void Act(OTGCombatSMC _controller)
        {
            MovementHandler moveHandler = _controller.Handler_Movement;

            Vector3 movement = new Vector3(moveHandler.Speed_CurrentXaxis, moveHandler.Speed_CurrentYaxis, moveHandler.Speed_CurrentZaxis);

            moveHandler.Comp_CharacterControl.Move(movement * Time.deltaTime);
        }
    }

}
