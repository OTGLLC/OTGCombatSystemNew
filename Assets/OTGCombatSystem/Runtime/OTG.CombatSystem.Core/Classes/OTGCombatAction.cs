
using UnityEngine;


namespace OTG.CombatSM.Core
{
    public abstract class OTGCombatAction : ScriptableObject
    {
        [SerializeField] protected E_ActionType m_combatActionType;
        public E_ActionType CombatActionType { get { return m_combatActionType; } }
        public abstract void Act(OTGCombatSMC _controller);
    }

    public enum E_ActionType
    {
        MovementBased,
        CombatBased,
        Misc
    }
}
