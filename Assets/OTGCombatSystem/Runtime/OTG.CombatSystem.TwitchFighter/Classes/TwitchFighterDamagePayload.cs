
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.TwitchFighter
{
    public struct TwitchFighterDamagePayload : IDamagePayload
    {

        #region Interface Implementation
        public Vector3 GetCollisionForce()
        {
            return Vector3.zero;
        }

        public float GetDamage()
        {
            return 0;   
        }

        #endregion
    }
}