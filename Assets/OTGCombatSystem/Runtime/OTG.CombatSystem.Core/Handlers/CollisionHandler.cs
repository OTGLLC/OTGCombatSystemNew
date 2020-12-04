

namespace OTG.CombatSM.Core
{
    public class CollisionHandler
    {
        #region Fields
        private CollisionsHandlerData m_handlerData;
        #endregion

        #region Properties
        public OTGHitColliderController HitCollider { get; private set; }
        #endregion

        #region Public API
        public CollisionHandler(HandlerDataGroup _dataGroup, OTGHitColliderController _hitCollider)
        {
            InitHandler(_dataGroup, _hitCollider);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitHandler(HandlerDataGroup _dataGroup, OTGHitColliderController _hitCollider)
        {
            m_handlerData = _dataGroup.CollisionHandlerData;
            HitCollider = _hitCollider;
        }
        private void Cleanup()
        {
            m_handlerData = null;
            HitCollider = null;
        }
        #endregion
    }
}