#pragma warning disable CS0649



using UnityEngine;

namespace OTG.CombatSM.Core
{
    [System.Serializable]
    public class OTGCombatAnimation
    {
        #region Inspector Vars
        [SerializeField] private AnimationClip m_animClip;
        [SerializeField] private CombatAnimData m_animData;
        [SerializeField] private CombatAnimHitCollisionData m_hitCollisionData;
        [SerializeField] private CombatAnimHurtCollisionData m_hurtCollisionData;
        #endregion

        #region Properties
        public AnimationClip AnimClip { get { return m_animClip; } }
        public CombatAnimData AnimData { get { return m_animData; } }
        public CombatAnimHitCollisionData HitCollisionData { get { return m_hitCollisionData; } }
        public CombatAnimHurtCollisionData HurtCollisionData { get { return m_hurtCollisionData; } }
        #endregion

    }

    [System.Serializable]
    public class CombatAnimData
    {
        #region Inspector Vars
        [Range(-1f,1f)]
        [SerializeField] private float m_HorizontalImpactForce;
        [SerializeField] private float m_VerticalImpactForce;
        [SerializeField] private float m_DepthImpactForce;
        [SerializeField] private float m_onConnectPauseTime;
        [SerializeField] private float m_onConnectBaseDamage;
        [SerializeField] private OTGVFXIdentification m_vfxID;
        #endregion

        #region Properties
        public Vector3 ImpactForce { get { return new Vector3(m_HorizontalImpactForce, m_VerticalImpactForce, m_DepthImpactForce); } }
        public float OnConnectPauseTime { get { return m_onConnectPauseTime; } }
        public float OnConnectBaseDamage { get { return m_onConnectBaseDamage; } }
        public OTGVFXIdentification VFX_ID { get { return m_vfxID; } }
        #endregion
    }
    [System.Serializable]
    public class CombatAnimHitCollisionData
    {
        [SerializeField] private Vector3 m_colliderPosition;
        [SerializeField] private Vector3 m_colliderSize;

        public Vector3 ColliderPosition { get { return m_colliderPosition; } }
        public Vector3 ColliderSize { get { return m_colliderSize; } }
    }
    [System.Serializable]
    public class CombatAnimHurtCollisionData
    {
        #region Inspector Vars
        [SerializeField] private OTGHurtColliderID m_hurtColliderID;
        [SerializeField] private Vector3 m_hurtBoxExtents;
        [SerializeField] private LayerMask m_validTargets;
        #endregion

        #region Properties
        public OTGHurtColliderID HurtColliderID { get { return m_hurtColliderID; } }
        public Vector3 HurtBoxExtents { get { return m_hurtBoxExtents; } }
        public LayerMask ValidTargets { get { return m_validTargets; } }
        #endregion
    }
}
