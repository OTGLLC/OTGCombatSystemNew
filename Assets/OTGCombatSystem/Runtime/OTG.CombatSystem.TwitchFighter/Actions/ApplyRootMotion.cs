
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class ApplyRootMotion : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;
            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;

            AnimationHandler animHandler = _controller.Handler_Animation;

            Vector3 deltaMotion = new Vector3(animHandler.Comp_Anim.deltaPosition.z,0,0);


            twitch.Comp_CharacterControl.Move(deltaMotion);
        }
    }
}
