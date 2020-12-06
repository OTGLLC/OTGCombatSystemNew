﻿#pragma warning disable CS0649


using System.Collections.Generic;
using UnityEngine;
 

namespace OTG.CombatSM.Core
{
    [CreateAssetMenu(menuName =OTGCombatUtilities.CombatStatePath,fileName ="OTGCombatState")]
    public class OTGCombatState : ScriptableObject
    {
        #region Fields

        #endregion

        #region Inspector Vars
        [SerializeField] private OTGCombatAnimation m_combatAnim;
        [SerializeField] private OTGCombatAction[] m_onEnterActions;
        [SerializeField] private OTGCombatAction[] m_onUpdateActions;
        [SerializeField] private OTGCombatAction[] m_animUpdateActions;
        [SerializeField] private OTGCombatAction[] m_onExitActions;
        [SerializeField] private CombatStateTransition[] m_stateTransitions;
        #endregion

        #region Properties
        public ushort ID { get; private set; }
        #endregion

        #region Fields
        private float m_stateTime;
        #endregion


        #region Public API
        public void AssignID(ushort _id)
        {
            ID = _id;
        }
        public void OnStateEnter(OTGCombatSMC _controller, bool _reentry = false)
        {
            SetAnimationData(_controller);
            SetHurtColliderData(_controller);
            SetHitColliderData(_controller);

            if (_reentry)
            {
                Debug.Log("State Re-entry: " + name + "State time: " + m_stateTime);
                return;
            }

            ResetStateTime();
            UpdateHandlerStateTime(_controller);

            Debug.Log(name + "Entered : ComboCounter " + _controller.Handler_Combat.ConsecutiveHit);
           
            PlayAnimation(_controller);
            PerformActions(m_onEnterActions, _controller);
        }
        public void OnStateUpdate(OTGCombatSMC _controller)
        {
            UpdateHandlerStateTime(_controller);
            CountdownComboTimer(_controller);
            PerformActions(m_onUpdateActions, _controller);
            EvaluateTransitions(_controller);
            IncrementStateTime();
            
        }
        public void OnStateAnimUpdate(OTGCombatSMC _controller)
        {
            PerformActions(m_animUpdateActions, _controller);
        }
        public void OnStateExit(OTGCombatSMC _controller)
        {
            Debug.Log(name + "Exited : ComboCounter " + _controller.Handler_Combat.ConsecutiveHit);
            PerformActions(m_onExitActions, _controller);
        }
        #endregion

        #region Utility
        private void PerformActions(OTGCombatAction[] _actions, OTGCombatSMC _controller)
        {
            for(int i = 0; i < _actions.Length; i++)
            {
                _actions[i].Act(_controller);
            }
        }
        private void PlayAnimation(OTGCombatSMC _controller)
        {
            if (m_combatAnim == null || m_combatAnim.AnimClip == null)
                return;

            _controller.Handler_Animation.PlayAnimation(m_combatAnim.AnimClip);

        }
        private void EvaluateTransitions(OTGCombatSMC _controller)
        {
            for(int i = 0; i < m_stateTransitions.Length; i++)
            {
                m_stateTransitions[i].MakeDecision(_controller);
            }
        }
        private void SetHitColliderData(OTGCombatSMC _controller)
        {
            CombatAnimHitCollisionData data = m_combatAnim.HitCollisionData;
            _controller.Handler_Collision.HitCollider.OnDataUpdate(data);
        }
        private void SetHurtColliderData(OTGCombatSMC _controller)
        {
            CombatAnimHurtCollisionData data = m_combatAnim.HurtCollisionData;
            _controller.Handler_Collision.SetHurtColliderData(data);
        }
        private void ResetStateTime()
        {
            m_stateTime = 0;
        }
        private void IncrementStateTime()
        {
            m_stateTime += Time.deltaTime;
        }
        private void UpdateHandlerStateTime(OTGCombatSMC _controller)
        {
            _controller.Handler_Animation.UpdateStateTime(m_stateTime);
        }
        private void SetAnimationData(OTGCombatSMC _controller)
        {
            if (m_combatAnim.AnimClip == null)
                return;

            _controller.Handler_Animation.UpdateAnimData(m_combatAnim.AnimData);
        }
        private void CountdownComboTimer(OTGCombatSMC _controller)
        {
            _controller.Handler_Combat.CountdownComboTimer();
        }
        #endregion

    }

    [System.Serializable]
    public struct CombatStateTransition
    {
        [SerializeField] private OTGCombatState m_nextState;
        [SerializeField] private OTGTransitionDecision[] m_decisions;
        [SerializeField] private bool m_usePreviousState;

        public void MakeDecision(OTGCombatSMC _controller)
        {
            for(int i = 0; i < m_decisions.Length; i++)
            {
                if (!m_decisions[i].Decide(_controller))
                    return;
            }
            _controller.OnChangeStateRequested(m_nextState,m_usePreviousState);
        }
    }

}
