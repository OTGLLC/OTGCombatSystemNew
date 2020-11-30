using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class CalculateHorizontalMovement : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.Calculation;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;


            twitch.HorizontalSpeed = Mathf.MoveTowards(twitch.HorizontalSpeed, twitch.DesiredDashSpeed, twitch.Data.DashSpeed);
            float currentXPosition = Mathf.MoveTowards(twitch.Comp_Transform.position.x, twitch.DesiredDashDistance, twitch.HorizontalSpeed);

            twitch.CurrentDashDistance = currentXPosition - twitch.DashStartPosition.x;
        }
    }
}



