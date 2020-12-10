
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class HitStopTimeExpired : TwitchFighterBaseTransition
    {
        protected override void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Status;
            base.Awake();
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            AnimationHandler animHandler = _controller.Handler_Animation;

            Debug.Log("Transition: StunTimeExpired. Current State Time " + animHandler.StateTime +"Max Stun Time: "+animHandler.CurrentAnimData.HitStopTime);
            return (animHandler.StateTime >= animHandler.CurrentAnimData.HitStopTime);

        }
    }
}

