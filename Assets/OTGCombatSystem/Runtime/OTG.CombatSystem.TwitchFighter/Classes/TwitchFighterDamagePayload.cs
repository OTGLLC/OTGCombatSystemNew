
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.TwitchFighter
{
    public struct TwitchFighterDamagePayload : IDamagePayload
    {
        float m_stunTime;
        Vector3 m_impactForce;
        public TwitchFighterDamagePayload(CombatAnimData _combatData, int _faceDirection)
        {
            m_stunTime = 0;
            m_impactForce = Vector3.zero;
           CalculateStunTime(_combatData);
            CalculateImpactForce(_combatData, _faceDirection);
        }
        #region Interface Implementation
        public Vector3 GetImpactForce()
        {
            return m_impactForce;
        }
        public float GetStunTime()
        {
            return m_stunTime;
        }
        public float GetDamage()
        {
            return 0;   
        }

        #endregion

        #region Utility
        private void CalculateStunTime(CombatAnimData _animData)
        {
            m_stunTime =  _animData.HitStopTime;
        }
        private void CalculateImpactForce(CombatAnimData _animData, int _faceDirection)
        {
            Vector3 rawForce = _animData.ImpactForce;
            float xForce = _faceDirection * rawForce.x;
            float yForce = rawForce.y;
            float zForce = rawForce.z;

            m_impactForce = new Vector3(xForce, yForce, zForce);
            
        }
        #endregion
    }
}