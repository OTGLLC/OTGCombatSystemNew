
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class SetAnimationPlaybackTime : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.AnimationBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            AnimationHandler animHandler = _controller.Handler_Animation;

            animHandler.PlayAnimationByTime(twitch.PercentageDistanceTraveled);
        }
    }
}

