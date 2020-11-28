

using UnityEngine;

namespace OTG.CombatSM.Core
{
    public abstract class OTGTransitionDecision : ScriptableObject
    {
        [SerializeField] protected E_TransitionDecisionType m_transitionDecisionType;
        public E_TransitionDecisionType TransitionDecisionType { get { return m_transitionDecisionType; } }
        public abstract bool Decide(OTGCombatSMC _controller);
    }
    public enum E_TransitionDecisionType
    {
        Movement,
        Combat,
        Status,
        Input,
        Misc,
        All
    }
}
