
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class PerformMovement : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.Perform;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;

           
            Vector3 movement = new Vector3(
                twitch.HorizontalSpeed,
                twitch.VerticalSpeed,
                twitch.DepthSpeed
                );

            twitch.Comp_CharacterControl.Move(movement*Time.deltaTime);
        }
    }
}
