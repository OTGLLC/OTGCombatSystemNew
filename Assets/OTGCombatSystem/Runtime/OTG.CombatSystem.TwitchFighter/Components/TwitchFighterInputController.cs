
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using OTG.CombatSM.Core;
using System;

namespace OTG.TwitchFighter
{
    public class TwitchFighterInputController : MonoBehaviour
    {

        #region Fields
        private Coroutine m_rightAttack;
        private Coroutine m_leftAttack;
        private Coroutine m_switchLanesUp;
        private Coroutine m_switchLanesDown;
        private WaitForSeconds m_delay;
        private InputHandler m_inputHandler;
        #endregion

        private void Start()
        {
            m_delay = new WaitForSeconds(0.2f);
            m_inputHandler = GetComponent<OTGCombatSMC>().Handler_Input;
        }
        private void OnDisable()
        {
            StopAllCoroutines();
            m_delay = null;
            m_inputHandler = null;
        }

        #region Input Callbacks
        public void OnLeftAttack(InputAction.CallbackContext context)
        {
            StartCorrectCoroutine(ref m_leftAttack, context, LeftAttack_CO());
        }

        public void OnRightAttack(InputAction.CallbackContext context)
        {
            StartCorrectCoroutine(ref m_rightAttack, context, RightAttack_CO());
        }

        public void OnSwitchLaneDown(InputAction.CallbackContext context)
        {
            StartCorrectCoroutine(ref m_switchLanesDown, context, SwitchlanesDown_CO());
        }

        public void OnSwitchLaneUp(InputAction.CallbackContext context)
        {
            StartCorrectCoroutine(ref m_switchLanesUp, context, SwitchlanesUp_CO());
        }

        #endregion

        #region Utility
        private void StartCorrectCoroutine(ref Coroutine _target, InputAction.CallbackContext _ctx, IEnumerator _targetCoroutine)
        {
            if (_ctx.phase != InputActionPhase.Performed)
                return;

            if (_target != null)
                StopCoroutine(_target);

            _target = StartCoroutine(_targetCoroutine);
        }
        #endregion

        #region Coroutines
        private IEnumerator RightAttack_CO()
        {
            m_inputHandler.TwitchInput.HasRightInput = true;
            yield return m_delay;
            m_inputHandler.TwitchInput.HasRightInput = false;

        }
        private IEnumerator LeftAttack_CO()
        {
            m_inputHandler.TwitchInput.HasLeftInput = true;
            yield return m_delay;
            m_inputHandler.TwitchInput.HasLeftInput = false;

        }
        private IEnumerator SwitchlanesDown_CO()
        {
            m_inputHandler.TwitchInput.HasSwitchLanesDownInpu = true;
            yield return m_delay;
            m_inputHandler.TwitchInput.HasSwitchLanesDownInpu = false;

        }
        private IEnumerator SwitchlanesUp_CO()
        {
            m_inputHandler.TwitchInput.HasSwitchLanesUpInput = true;
            yield return m_delay;
            m_inputHandler.TwitchInput.HasSwitchLanesUpInput = false;

        }
        #endregion
    }
}