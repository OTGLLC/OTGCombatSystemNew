

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class DirectionInputDoesNotMatchFaceDirection : TwitchFighterBaseTransition
    {
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Input;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            TwitchFighterInput input = _controller.Handler_Input.TwitchInput;

            int faceDirection = (twitch.Comp_Transform.rotation.eulerAngles.y == 180) ? -1 : 1;

            if (!input.HasLeftInput && !input.HasRightInput)
                return false;

            bool value = true;

            if (faceDirection < 0 && input.HasLeftInput)
                value = false;
            if (faceDirection > 0 && input.HasRightInput)
                value = false;


            return value;
        }
    }
}