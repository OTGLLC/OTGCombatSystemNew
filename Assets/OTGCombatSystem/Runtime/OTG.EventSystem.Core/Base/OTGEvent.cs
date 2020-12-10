
using System.Collections.Generic;
using UnityEngine;

namespace OTG.EventSystem.Core
{
    public abstract class OTGEvent<T> : ScriptableObject
    {
        protected List<OTGEventListener<T>> m_listeners = new List<OTGEventListener<T>>();
        public abstract void Raise(T t);

        public void AddListener(OTGEventListener<T> _listener)
        {
            if (!m_listeners.Contains(_listener))
                m_listeners.Add(_listener);
        }
        public void RemoveListener(OTGEventListener<T> _listener)
        {
            if (m_listeners.Contains(_listener))
                m_listeners.Remove(_listener);
        }
    }
}
