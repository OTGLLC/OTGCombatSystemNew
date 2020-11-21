

namespace OTG.CombatSM.Core
{
    public class InputHandler
    {
        #region Fields
        private InputHandlerData m_handlerData;
        #endregion

        #region Properties

        #endregion

        #region Public API
        public InputHandler(HandlerDataGroup _dataGroup)
        {
            InitializeData(_dataGroup);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitializeData(HandlerDataGroup _group)
        {
            m_handlerData = _group.InputsHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        #endregion

    }
}