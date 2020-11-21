
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class MovementHandler
    {
        #region Fields
        private MovementHandlerData m_moveHandlerData;
        #endregion

        #region Properties
        public Transform Comp_Transform { get; private set; }
        public CharacterController Comp_CharControl { get; private set; }
        #endregion

        #region Public API
        public MovementHandler(HandlerDataGroup _dataGroup, CharacterController _charControl, Transform _trans)
        {
            m_moveHandlerData = _dataGroup.MoveHandlerData;
            Comp_CharControl = _charControl;
            Comp_Transform = _trans;
        }
        public void CleanupHandler()
        {
            Cleanup();
        }

        #endregion

        #region Utility
        private void Cleanup()
        {
            m_moveHandlerData = null;
            Comp_Transform = null;
            Comp_CharControl = null;
        }

        #endregion
    }
}