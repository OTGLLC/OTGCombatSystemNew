

using UnityEngine;

namespace OTG.CombatSM.Core
{
    [System.Serializable]
    public class MovementHandlerData
    {
        [SerializeField] private TwitchMovementData m_twitchData;


        public TwitchMovementData TwitchData { get { return m_twitchData; } }
    }
    [System.Serializable]
    public class TwitchMovementData
    {
        [SerializeField] private float m_dashDistance;
        [SerializeField] private float m_dashSpeed;
        [SerializeField] private float m_acceleration;
        [SerializeField] private float m_changeLaneSpeed;
        [SerializeField] private float m_dashDistanceMinThreshold;
        [SerializeField] private AnimationCurve m_dashCurve;
        
        public float DashDistance { get { return m_dashDistance; } }
        public float DashSpeed { get { return m_dashSpeed; } }
        public float Acceleration { get { return m_acceleration; } }
        public float ChangeLaneSpeed { get { return m_changeLaneSpeed; } }
        public float DashDistanceMinThreshold { get { return m_dashDistanceMinThreshold; } }
        public AnimationCurve DashCurve { get { return m_dashCurve; } }
    }
}