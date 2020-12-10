

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class RotateToMatchInputDirection : TwitchFighterBaseAction
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
            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;
            Vector3 rotation = Vector3.zero;

            if (twitchInput.HasLeftInput)
                rotation = new Vector3(0, -180, 0);
            else if (twitchInput.HasRightInput)
                rotation = new Vector3(0, 0, 0);

            twitch.Comp_Transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
