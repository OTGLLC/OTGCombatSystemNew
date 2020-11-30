#pragma warning disable CS0649
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class ResetAnimationEvent : TwitchFighterBaseAction
    {
        [SerializeField] private OTGAnimationEvent m_neutralEvent;
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            _controller.Handler_Animation.UpdateAnimationEvent(m_neutralEvent);
        }
    }
}
