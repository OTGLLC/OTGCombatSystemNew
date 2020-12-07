
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class CombatHandler
    {
        #region Fields
        private CombatHandlerData m_handlerData;
        #endregion

        #region Properties
        public TwitchFighterCombatParams TwitchCombat { get; private set; }
        #endregion

        #region Public API
        public CombatHandler(HandlerDataGroup _dataGroup)
        {
            InitHandlerData(_dataGroup);
            InitializeIndividualParams(_dataGroup);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitializeIndividualParams(HandlerDataGroup _dataGroup)
        {
            TwitchCombat = new TwitchFighterCombatParams(_dataGroup);
        }
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
    public class TwitchFighterCombatParams
    {
        public OTGCombatSMC NearestTarget { get; set; }
        public Vector3 NearestTargetPosition { get; set; }

        public TwitchFighterCombatParams(HandlerDataGroup _datGroup)
        {
            Initialize();
        }

        public void CleanupParams()
        {
            NearestTarget = null;
        }
        public void ResetParams()
        {
            NearestTarget = null;
        }
        public void RecieveDamagePayload(IDamagePayload _payload)
        {
            Debug.Log("Recieved damage payload");
        }

        private void Initialize()
        {

        }
    }
}