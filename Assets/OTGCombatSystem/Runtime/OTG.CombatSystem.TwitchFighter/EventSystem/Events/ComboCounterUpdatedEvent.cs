
using UnityEngine;
using OTG.EventSystem.Core;

namespace OTG.TwitchFighter
{
    [CreateAssetMenu(menuName = "TwitchFighter/Events/ComboCounterUpdatedEvent")]
    public class ComboCounterUpdatedEvent : OTGEvent<int>
    {
        
        public override void Raise(int t)
        {
            for(int i = 0; i < m_listeners.Count; i++)
            {
                m_listeners[i].OnEventRaised(t);
            }
        }
    }
}
