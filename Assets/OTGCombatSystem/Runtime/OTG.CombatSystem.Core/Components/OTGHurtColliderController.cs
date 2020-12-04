using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class OTGHurtColliderController : MonoBehaviour
    {
        #region Inspector Vars
        [SerializeField] private OTGHurtColliderID m_hurtColliderID;
        #endregion

        #region Properties
        public OTGHurtColliderID HurtColliderID { get { return m_hurtColliderID; } }
        public int NumberOfContacts { get; set; }
        public Collider[] ScanResults { get; private set; }
       
        #endregion

        #region Fields
        private CombatAnimHurtCollisionData m_data;
        private Transform m_trans;
        #endregion

        #region Unity API
        private void OnEnable()
        {
            ScanResults = new Collider[OTGCombatSystemConfig.MAX_HIT_SCAN_ELEMENTS];
            m_trans = GetComponent<Transform>();
        }
        private void OnDisable()
        {
            ScanResults = null;
            m_trans = null;
        }
        #endregion

        #region Publc API
        public void OnDataUpdate(CombatAnimHurtCollisionData _data)
        {
            m_data = _data;
        }
        public void OnPerformDamageScan()
        {
            NumberOfContacts = Physics.OverlapBoxNonAlloc(m_trans.position, m_data.HurtBoxExtents, ScanResults, m_trans.rotation, m_data.ValidTargets);
            

        }
        #endregion
    }
    

}
