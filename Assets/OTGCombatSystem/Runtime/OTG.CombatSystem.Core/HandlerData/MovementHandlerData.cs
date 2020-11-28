

using UnityEngine;

namespace OTG.CombatSM.Core
{
    [System.Serializable]
    public class MovementHandlerData
    {
        [SerializeField] private float m_horizontalMoveSpeed;
        [SerializeField] private float m_horizontalDashDistance;


        #region Properties
        public float HorizontalMoveSpeed { get { return m_horizontalMoveSpeed; } }
        public float HorizontalDashDistance { get { return m_horizontalDashDistance; } }
        #endregion
    }
}