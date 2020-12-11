﻿
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class LockZpositionToCurrentLane : TwitchFighterBaseAction
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

            float lanePosition = twitch.CurrentLane * twitch.GlobalCombatConfig.LaneDistance;

            Vector3 newPosition = new Vector3(twitch.Comp_Transform.position.x, twitch.Comp_Transform.position.y, lanePosition);

            twitch.Comp_Transform.position = newPosition;
        }
    }
}

