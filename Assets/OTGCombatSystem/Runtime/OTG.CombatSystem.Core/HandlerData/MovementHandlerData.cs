

using UnityEngine;

namespace OTG.CombatSM.Core
{
    [System.Serializable]
    public class MovementHandlerData
    {
        [SerializeField] private float m_horizontalMoveSpeed;


        #region Properties
        public float HorizontalMoveSpeed { get { return m_horizontalMoveSpeed; } }
        #endregion
    }
}