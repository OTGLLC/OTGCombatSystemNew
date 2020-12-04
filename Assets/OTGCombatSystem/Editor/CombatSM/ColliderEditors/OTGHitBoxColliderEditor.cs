
using UnityEditor;
using UnityEngine;
using OTG.CombatSM.Core;


namespace OTG.CombatSM.EditorTools
{
    public class OTGHitBoxColliderEditor
    {


        [DrawGizmo(GizmoType.InSelectionHierarchy)]
        public static void DrawHitBoxTriggerZone(OTGHitColliderController _hitBox, GizmoType _gizmoType)
        {
            BoxCollider collider = _hitBox.gameObject.GetComponent<BoxCollider>();
            Transform trans = _hitBox.GetComponent<Transform>();

            Gizmos.color = new Color(1,0,0,.5f);
            Gizmos.DrawCube(trans.position, collider.size);
        }
    }

    
}
