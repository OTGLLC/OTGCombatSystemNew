
using UnityEngine;
using UnityEngine.InputSystem;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.Concrete
{
    public class TwitchFighterInputController : MonoBehaviour
    {
        #region Fields
        private OTGCombatSMC m_smc;
        #endregion

        #region UnityAPI
        private void Start()
        {
            m_smc = GetComponent<OTGCombatSMC>();
        }
        private void OnDisable()
        {
            m_smc = null;
        }
        #endregion

        #region Input Callbacks
        public void OnRightAttack(InputAction.CallbackContext ctx)
        {
            bool val;

            SetCorrectInput(out val, ctx);
            m_smc.Handler_Input.TwitchInput.HasRightInput = val;
        }
        public void OnLeftAttack(InputAction.CallbackContext ctx)
        {
            bool val;

            SetCorrectInput(out val, ctx);
            m_smc.Handler_Input.TwitchInput.HasLeftInput = val;
        }
        public void OnRightAttackHold(InputAction.CallbackContext ctx)
        {
            bool val;

            SetCorrectInput(out val, ctx);
            m_smc.Handler_Input.TwitchInput.IsHoldingRightInput = val;
        }
        public void OnLeftAttackHold(InputAction.CallbackContext ctx)
        {
            bool val;

            SetCorrectInput(out val, ctx);
            m_smc.Handler_Input.TwitchInput.IsHoldingLeftInput = val;
        }
        public void OnSwitchLanesUp(InputAction.CallbackContext ctx)
        {
            bool val;

            SetCorrectInput(out val, ctx);
            m_smc.Handler_Input.TwitchInput.HasSwitchLanesUpInput = val;
        }
        public void OnSwitchLanesDown(InputAction.CallbackContext ctx)
        {
            bool val;

            SetCorrectInput(out val, ctx);
            m_smc.Handler_Input.TwitchInput.HasSwitchLanesDownInpu = val;
        }
        #endregion

        #region Utility
        private void SetCorrectInput( out bool _boolVal, InputAction.CallbackContext _ctx)
        {
            _boolVal = false;
            if (_ctx.phase == InputActionPhase.Performed)
            {
                _boolVal = true;
            }
            if (_ctx.phase == InputActionPhase.Canceled || _ctx.phase == InputActionPhase.Disabled)
            {
                _boolVal = false;
            }
        }
        #endregion
    }
}
