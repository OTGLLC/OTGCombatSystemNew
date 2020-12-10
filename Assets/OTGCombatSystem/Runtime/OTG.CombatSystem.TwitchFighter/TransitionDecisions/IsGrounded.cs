

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class IsGrounded : TwitchFighterBaseTransition
    {
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;

            bool isGrounded = twitch.Comp_CharacterControl.isGrounded;

            Debug.Log("Is Grounded :  " + isGrounded);
            return isGrounded;

        }
    }
}
