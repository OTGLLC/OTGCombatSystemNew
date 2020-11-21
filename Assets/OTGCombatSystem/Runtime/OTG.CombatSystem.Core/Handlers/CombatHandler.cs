


namespace OTG.CombatSM.Core
{
    public class CombatHandler
    {
        #region Fields
        private CombatHandlerData m_handlerData;
        #endregion

        #region Properties

        #endregion

        #region Public API
        public CombatHandler(HandlerDataGroup _dataGroup)
        {
            InitHandlerData(_dataGroup);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitHandlerData(HandlerDataGroup _dataGroup)
        {
            m_handlerData = _dataGroup.CombatsHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        #endregion
    }
}