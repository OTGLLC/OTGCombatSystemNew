
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class MovementHandler
    {
        
        #region Properties
        public MovementHandlerData Data { get; private set; }
        public Transform Comp_Transform { get; private set; }
        public CharacterController Comp_CharControl { get; private set; }
        public float DesiredHorizontalDistance { get; set; }
        public float CurrentHorizontalPosition { get; set; }
        #endregion

        #region Public API
        public MovementHandler(HandlerDataGroup _dataGroup, CharacterController _charControl, Transform _trans)
        {
            Data = _dataGroup.MoveHandlerData;
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
            Data = null;
            Comp_Transform = null;
            Comp_CharControl = null;
        }

        #endregion
    }
}