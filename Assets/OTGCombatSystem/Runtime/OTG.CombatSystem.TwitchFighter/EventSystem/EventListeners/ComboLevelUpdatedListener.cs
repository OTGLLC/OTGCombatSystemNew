

using UnityEngine;
using OTG.EventSystem.Core;
using UnityEngine.Events;

namespace OTG.TwitchFighter
{
    public class ComboLevelUpdatedListener : OTGEventListener<int>
    {
        [SerializeField] private ComboLevelUpdatedUnityEvent m_unityEvent;
        [SerializeField] private ComboLevelUpdatedEvent m_event;

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
    public class ComboLevelUpdatedUnityEvent : UnityEvent<int> { }
}
