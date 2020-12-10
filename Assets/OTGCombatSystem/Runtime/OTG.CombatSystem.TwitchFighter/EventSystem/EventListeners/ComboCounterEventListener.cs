
using UnityEngine;
using OTG.EventSystem.Core;
using UnityEngine.Events;

namespace OTG.TwitchFighter
{
    public class ComboCounterEventListener : OTGEventListener<int>
    {
        [SerializeField] private ComboCounterUnityEvent m_unityEvent;
        [SerializeField] private ComboCounterUpdatedEvent m_event;

        public override void OnDisable()
        {
            m_event.RemoveListener(this);
        }

        public override void OnEnable()
        {
            m_event.AddListener(this);
        }

        public override void OnEventRaised(int t)
        {
            m_unityEvent.Invoke(t);
        }
    }

    [System.Serializable]
    public class ComboCounterUnityEvent : UnityEvent<int> { }
}
