﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class OTGHitColliderController : MonoBehaviour
    {

        #region Fields
        private Transform m_trans;
        private BoxCollider m_collider;
        #endregion

        #region Unity API
        private void OnEnable()
        {
            m_trans = GetComponent<Transform>();
            m_collider = GetComponent<BoxCollider>();
        }
        private void OnDisable()
        {
            m_trans = null;
            m_collider = null;
            
        }
        #endregion

        #region Public API
        public void OnDataUpdate(CombatAnimHitCollisionData _data)
        {
            SetColliderSizeAndPosition(_data);
        }
        public void OnDamageRecieved(IDamagePayload _payload)
        {

        }
        #endregion
        private void SetColliderSizeAndPosition(CombatAnimHitCollisionData _data)
        {
            m_trans.localPosition = _data.ColliderPosition;
            m_collider.size = _data.ColliderSize;
        }
    }

}
