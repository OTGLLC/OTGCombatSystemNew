
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class CalculatePercentageDistanceTraveled : TwitchFighterBaseAction
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
            float currentDistance = Mathf.Abs(twitch.CurrentDashDistance);
            float maxDashDistance = Mathf.Abs(twitch.DesiredDashDistance);

            twitch.PercentageDistanceTraveled = 1 - ((maxDashDistance - currentDistance) / maxDashDistance);
        }
    }
}
