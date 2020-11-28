
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.Concrete
{
    
    public class CalculateCurrentDashDistance : OTGCombatAction
    {
        private void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased_Twitch;
            m_processType = E_ProcessType.Calculation;
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitchParams = _controller.Handler_Movement.TwitchParams;
            float newPosition = _controller.Handler_Movement.Comp_Transform.position.x + 1;

            twitchParams.Movement = Mathf.Lerp(_controller.Handler_Movement.Comp_Transform.position.x, newPosition, Time.deltaTime * _controller.Handler_Movement.Data.HorizontalMoveSpeed);

            twitchParams.CurrentDashDistance += twitchParams.Movement;
        }
    }
}
