
using UnityEditor;
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class OTGHurtColliderEditor 
    {

        [DrawGizmo(GizmoType.InSelectionHierarchy)]
        public static void DrawHurtBoxTriggerZone(OTGHurtColliderController _hitBox, GizmoType _gizmoType)
        {
            BoxCollider collider = _hitBox.gameObject.GetComponent<BoxCollider>();
            Transform trans = _hitBox.GetComponent<Transform>();

            Gizmos.color = new Color(0, 1, 0, .5f);
            Gizmos.matrix = trans.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, collider.size);
           
        }
    }
}