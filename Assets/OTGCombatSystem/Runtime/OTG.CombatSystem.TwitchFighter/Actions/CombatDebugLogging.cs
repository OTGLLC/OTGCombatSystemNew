#if UNITY_EDITOR
#pragma warning disable CS0649

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    [CreateAssetMenu]
    public class CombatDebugLogging : TwitchFighterBaseAction
    {
        public enum E_StateStatus
        {
            OnEnter,
            OnUpdate,
            OnAnimationUpdate,
            OnExit,
            Misc
        }
        public enum LoggingType
        {
            CollisionID,
            Generic
        }

        [SerializeField] private E_StateStatus m_stateStatus;
        [SerializeField] private LoggingType m_loggingType;

        [SerializeField] private bool m_shouldPause;
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.Debuging;
            m_processType = E_ProcessType.Debuging;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            string stateName = _controller.CurrentState.name;
            float stateTime = _controller.CurrentState.StateTime;
            string stateStatus = m_stateStatus.ToString();
            bool stateReentered = _controller.CurrentState.StateReEntered;
            bool hasCollisionID = _controller.Handler_Collision.HasCollisionID;

            


            switch(m_loggingType)
            {
                case LoggingType.CollisionID:
                    ProduceCollisionIDStopLog(stateName, stateTime, stateStatus, stateReentered, hasCollisionID);
                    break;
                case LoggingType.Generic:
                    ProduceContinuousLog(stateName, stateTime, stateStatus, stateReentered);
                    break;
            }

            _controller.Handler_Collision.HasCollisionID = false;

        }

        private void ProduceCollisionIDStopLog(string stateName, float stateTime,string stateStatus,bool stateReentered, bool _hasCollisionID)
        {
            string message = string.Format("{0} >> StateTime: {1} >> StateStatus: {2} >> StateReEntered: {3} >> CollisionID: {4}", stateName, stateTime, stateStatus, stateReentered, _hasCollisionID);
            if (m_shouldPause && _hasCollisionID)
            {
                Debug.LogError(message);
                return;
            }
            Debug.Log(message);
        }
        private void ProduceContinuousLog(string stateName, float stateTime, string stateStatus, bool stateReentered)
        {
            string message = string.Format("{0} >> StateTime: {1} >> StateStatus: {2} >> StateReEntered: {3}", stateName, stateTime, stateStatus, stateReentered);
            if (m_shouldPause)
            {
                Debug.LogError(message);
                return;
            }
            Debug.Log(message);
        }
    }
}

#endif
