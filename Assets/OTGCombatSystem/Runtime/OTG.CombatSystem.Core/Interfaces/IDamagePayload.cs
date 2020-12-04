using System.Collections;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public interface IDamagePayload
    {
        float GetDamage();
        Vector3 GetCollisionForce();
    }
}