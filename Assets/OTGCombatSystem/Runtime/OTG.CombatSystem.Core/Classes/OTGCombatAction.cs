﻿#pragma warning disable CS0649

using UnityEngine;


namespace OTG.CombatSM.Core
{
    public abstract class OTGCombatAction : ScriptableObject
    {
        [SerializeField] protected E_ActionType m_combatActionType;
        [SerializeField] protected E_ProcessType m_processType;

        public E_ActionType CombatActionType { get { return m_combatActionType; } }
        public E_ProcessType ProcessType { get { return m_processType; } }
        public abstract void Act(OTGCombatSMC _controller);
    }

    public enum E_ActionType
    {
        MovementBased,
        CombatBased,
        Misc
    }
    public enum E_ProcessType
    {
        Calculation,
        Perform
    }
}
