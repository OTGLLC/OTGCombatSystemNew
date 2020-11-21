

namespace OTG.CombatSM.Core
{
    public class CollisionHandler
    {
        #region Fields
        private CollisionsHandlerData m_handlerData;
        #endregion

        #region Properties

        #endregion

        #region Public API
        public CollisionHandler(HandlerDataGroup _dataGroup)
        {
            InitHandler(_dataGroup);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitHandler(HandlerDataGroup _dataGroup)
        {
            m_handlerData = _dataGroup.CollisionHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        #endregion
    }
}