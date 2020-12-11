
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class IsFacingLeft : TwitchFighterBaseTransition
    {
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Movement;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;

            return twitch.Comp_Transform.rotation.eulerAngles.y == twitch.GlobalCombatConfig.FacingLeftRotation;

        }
    }
}
