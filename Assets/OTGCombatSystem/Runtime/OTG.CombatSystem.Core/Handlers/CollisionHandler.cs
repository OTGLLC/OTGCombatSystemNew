using UnityEngine;
using System.Collections.Generic;

namespace OTG.CombatSM.Core
{
    public class CollisionHandler
    {
        #region Fields
        private CollisionsHandlerData m_handlerData;
        private CombatAnimHurtCollisionData m_hurtData;
        #endregion

        #region Properties
        public OTGHitColliderController HitCollider { get; private set; }
        public Dictionary<OTGHurtColliderID, OTGHurtColliderController> HurtColliders;
        public int NumberOfContacts { get; set; }
        public Collider[] ScanResults { get; private set; }

        #endregion

        #region Public API
        public CollisionHandler(HandlerDataGroup _dataGroup, OTGHitColliderController _hitCollider, OTGHurtColliderController[] _hurtColliders)
        {
            InitHandler(_dataGroup, _hitCollider);
            InitHurtColliderLookup(_hurtColliders);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        public void SetHurtColliderData(CombatAnimHurtCollisionData _data)
        {
            m_hurtData = _data;
        }
        public void OnAnimationEvent(OTGAnimationEvent _ev)
        {
            if (_ev.HurtColliderID == null)
                return;

            if (HurtColliders.ContainsKey(_ev.HurtColliderID))
                NumberOfContacts = HurtColliders[_ev.HurtColliderID].OnPerformDamageScan( ScanResults , m_hurtData);
        }
        #endregion

        #region Utility
        private void InitHandler(HandlerDataGroup _dataGroup, OTGHitColliderController _hitCollider)
        {
            m_handlerData = _dataGroup.CollisionHandlerData;
            HitCollider = _hitCollider;
            ScanResults = new Collider[OTGCombatSystemConfig.MAX_HIT_SCAN_ELEMENTS];
        }
        private void InitHurtColliderLookup(OTGHurtColliderController[] _hurtColliders)
        {
            HurtColliders = new Dictionary<OTGHurtColliderID, OTGHurtColliderController>();
            for(int i = 0; i < _hurtColliders.Length; i++)
            {
                OTGHurtColliderController ctrl = _hurtColliders[i];
                if (!HurtColliders.ContainsKey(ctrl.HurtColliderID))
                    HurtColliders.Add(ctrl.HurtColliderID, ctrl);
            }
        }
        private void Cleanup()
        {
            m_handlerData = null;
            m_hurtData = null;
            HitCollider = null;
            HurtColliders.Clear();
            HurtColliders = null;
            ScanResults = null;
        }
        #endregion

        
    }
}