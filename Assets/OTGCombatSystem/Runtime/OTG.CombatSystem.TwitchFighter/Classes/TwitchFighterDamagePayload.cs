
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.TwitchFighter
{
    public struct TwitchFighterDamagePayload : IDamagePayload
    {
        float m_stunTime;
        Vector3 m_impactForce;
        float m_physicalDamage;
        float m_energyDamage;
        public TwitchFighterDamagePayload(CombatAnimData _combatData, int _faceDirection, TwitchFighterCombatParams _handler)
        {
            m_stunTime = 0;
            m_impactForce = Vector3.zero;
            m_physicalDamage = 0;
            m_energyDamage = 0;

           CalculateStunTime(_combatData);
            CalculateImpactForce(_combatData, _faceDirection);
            CalculateDamage(_handler);
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
            return m_physicalDamage;   
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
        private void CalculateDamage(TwitchFighterCombatParams _combatHandler)
        {
             m_physicalDamage = _combatHandler.CurrentPhysicalAttack * (_combatHandler.CombatLevel + _combatHandler.CurrentComboCount);
        }
        #endregion
    }
}