
using OTG.CombatSM.Core;

namespace OTG.CombatSM.Concrete
{
    public class SetHorizontalDashingDistance : OTGCombatAction
    {
        private void Awake()
        {
            m_processType = E_ProcessType.SetParameter;
            m_combatActionType = E_ActionType.MovementBased_Twitch;
        }
        public override void Act(OTGCombatSMC _controller)
        {
            InputHandler inputHandler = _controller.Handler_Input;
            MovementHandler moveHandler = _controller.Handler_Movement;

            if(inputHandler.TwitchInput.HasRightInput)
            {
                moveHandler.TwitchParams.DesiredDashDistance = moveHandler.Data.HorizontalDashDistance;
            }
            if(inputHandler.TwitchInput.HasLeftInput)
            {
                moveHandler.TwitchParams.DesiredDashDistance = -moveHandler.Data.HorizontalDashDistance;
            }
            
        }
    }
}
