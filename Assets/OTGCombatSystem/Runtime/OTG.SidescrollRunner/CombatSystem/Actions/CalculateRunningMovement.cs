

using UnityEngine;
using OTG.CombatSM.Core;


namespace OTG.InfiniteRunner
{
    public class CalculateRunningMovement : OTGCombatAction
    {
        public override void Act(OTGCombatSMC _controller)
        {
            MovementHandler moveHandler = _controller.Handler_Movement;

            moveHandler.Speed_DesiredXaxis = moveHandler.Data.MaxXaxisSpeed;
            moveHandler.Speed_CurrentXaxis = Mathf.MoveTowards(moveHandler.Speed_CurrentXaxis, 
                                                                moveHandler.Speed_DesiredXaxis, 
                                                                moveHandler.Data.GroundAccelreration * Time.deltaTime);
        }
    }

}
