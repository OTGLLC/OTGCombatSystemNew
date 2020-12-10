
using UnityEngine;

namespace OTG.EventSystem.Core
{
    public abstract class OTGEventListener<T> : MonoBehaviour
    {
        public abstract void OnEventRaised(T t);
        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}
