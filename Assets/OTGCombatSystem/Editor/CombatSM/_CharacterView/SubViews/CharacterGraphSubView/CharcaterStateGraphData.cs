
using System.Collections.Generic;
using UnityEditor;
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.EditorTools
{
    public class CharcaterStateGraphData
    {

        #region Fields
        private SerializedObject m_rootCharacter;

        #endregion

        #region Properties
        public SerializedObject InitialCombatState { get; private set; }
       public StateDataNode StateDataNodeRoot { get; private set; }
       
        #endregion



        #region Public API
        public CharcaterStateGraphData()
        {

        }
        public void PopulateExistingStateData(OTGCombatSMC _selectedCharacter)
        {
            GetRootCharacterObject(_selectedCharacter);
            GetInitialCombatState();
        }
        public void Cleanup()
        {
            CleanupReferences();
        }
        #endregion

        #region Utility
        private void CleanupReferences()
        {

            InitialCombatState = null;


            m_rootCharacter = null;
            
        }
        private void GetRootCharacterObject(OTGCombatSMC _selectedChar)
        {
            m_rootCharacter = new SerializedObject(_selectedChar);
        }
        private void GetInitialCombatState()
        {
            Object obj = m_rootCharacter.FindProperty("m_startingState").objectReferenceValue;
            InitialCombatState = new SerializedObject(m_rootCharacter.FindProperty("m_startingState").objectReferenceValue);
            StateDataNodeRoot = new StateDataNode();

            GetAvailableTransitions(InitialCombatState,StateDataNodeRoot);
        }
        private void GetAvailableTransitions(SerializedObject _state, StateDataNode _n)
        {
            if (_state == null)
                return;

            _n.StateObj = _state;

            int transitionCount = _state.FindProperty("m_stateTransitions").arraySize;

            for(int i = 0; i < transitionCount; i++)
            {
                SerializedProperty transition = _state.FindProperty("m_stateTransitions").GetArrayElementAtIndex(i);
                
                SerializedObject nextState = new SerializedObject(transition.FindPropertyRelative("m_nextState").objectReferenceValue);

                string ID = _state.targetObject.name + "-to-" + nextState.targetObject.name;

                if(!_n.NextStates.ContainsKey(ID))
                {
                    StateDataNode n = new StateDataNode();
                    _n.NextStates.Add(ID, n);
                    GetAvailableTransitions(nextState, n);
                }

                int decisionCount = transition.FindPropertyRelative("m_decisions").arraySize;
                for(int j = 0; j < decisionCount; j++)
                {
                    SerializedProperty decision = transition.FindPropertyRelative("m_decisions").GetArrayElementAtIndex(j);
                    _n.Decisions.Add(decision.name);   
                }
                
            }

            
        }
        #endregion

    }
   
    public class StateDataNode
    {
        public SerializedObject StateObj;
        public List<string> Decisions;
        public Dictionary<string, StateDataNode> NextStates;

        public StateDataNode()
        {
            StateObj = null;
            Decisions = new List<string>();
            NextStates = new Dictionary<string, StateDataNode>();
        }
    }

   
}
