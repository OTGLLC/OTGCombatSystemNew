
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterViewData
    {
        #region Properties
        public List<OTGCombatSMC> CharactersInScene { get; private set; }
        public OTGCombatSMC SelectedCharacter { get; private set; }
        public OTGCombatState StartingState { get; private set; }
        public SerializedObject SelectedCharacterSObject { get; private set; }
        public SerializedObject CharacterHandlerDataObj { get; private set; }
        public SerializedProperty MovementDataProp { get; private set; }
        public SerializedProperty AnimHandlerDataProp { get; private set; }
        public SerializedProperty InputHandlerDataProp { get; private set; }
        public SerializedProperty CollisionHandlerDataProp { get; private set; }
        public SerializedProperty CombatHandlerDataProp { get; private set; }
        public CharacterStateTree StateTree { get; private set; }
        #endregion

        #region Public API
        public CharacterViewData()
        {
            CharactersInScene = new List<OTGCombatSMC>();
        }
        public void GetAllCharactersInScene()
        {
            CharactersInScene.Clear();

            OTGCombatSMC[] chars = Object.FindObjectsOfType<OTGCombatSMC>();

            for(int i = 0; i < chars.Length; i++)
            {
                if (!CharactersInScene.Contains(chars[i]))
                    CharactersInScene.Add(chars[i]);
            }

            
        }
        public void SetSelectedCharacter(OTGCombatSMC _selection)
        {
            SelectedCharacter = _selection;
            SelectedCharacterSObject = new SerializedObject(SelectedCharacter);

            GetStartingState();
            GetCharacterHandlerData();
            GetHandlerDataProperties();
            StateTree = new CharacterStateTree(StartingState);
        }

        #endregion
        #region Utility
        private void GetStartingState()
        {
              Object stateObj = SelectedCharacterSObject.FindProperty("m_startingState").objectReferenceValue;
            StartingState = (OTGCombatState)stateObj;

        }
        private void GetCharacterHandlerData()
        {
            CharacterHandlerDataObj = new SerializedObject(SelectedCharacterSObject.FindProperty("m_handlerDataGroup").objectReferenceValue);
        }
        private void GetHandlerDataProperties()
        {
            MovementDataProp = CharacterHandlerDataObj.FindProperty("m_moveHandlerData");
            AnimHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_animHandlerData");
            InputHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_inputHandlerData");
            CollisionHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_collisionHandlerData");
            CombatHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_combatHandlerData");
        }
        #endregion
    }
    public class StateNode
    {
        public OTGCombatState OwnerState { get; private set; }
        public SerializedObject OwnerStateObject { get; private set; }
        public int Level { get; private set; }
        public Dictionary<OTGCombatState, StateNode> StateTransitions { get; private set; }

        public StateNode(OTGCombatState _newState, int _level)
        {
            StateTransitions = new Dictionary<OTGCombatState, StateNode>();
           
            Level = _level;
            OwnerState = _newState;
            PopulateStateObject();
            FindTransitions(OwnerStateObject);
        }


        #region Utility
        private void PopulateStateObject()
        {
            OwnerStateObject = new SerializedObject(OwnerState);
        }
        private void FindTransitions(SerializedObject _ownerObj)
        {
            int amountOfTransitions = _ownerObj.FindProperty("m_stateTransitions").arraySize;
            for(int i = 0; i < amountOfTransitions; i++)
            {
                Object nextStateObj = _ownerObj.FindProperty("m_stateTransitions").GetArrayElementAtIndex(i).FindPropertyRelative("m_nextState").objectReferenceValue;
                if(nextStateObj != null)
                {
                    OTGCombatState state = (OTGCombatState)nextStateObj;
                    StateNode n = new StateNode(state,1);
                    if(!StateTransitions.ContainsKey(state))
                    {
                        StateTransitions.Add(state, n);
                    }

                }
            }
        }
        #endregion
    }
    public class StateNodeTransition
    {
        public string TransitionName { get; private set; }
        public OTGCombatState NextState { get; private set; }
        public SerializedObject NextStateObject { get; private set; }
        public StateNodeTransition(string _transitionName, OTGCombatState _owner)
        {
            TransitionName = _transitionName;
            NextState = _owner;
            NextStateObject = new SerializedObject(_owner);
        }
        public void Cleanup()
        {
            NextState = null;
            NextStateObject = null;
        }
    }
    public class CharacterStateTree
    {
        public StateNode RootNode { get; private set; }
        public Dictionary<OTGCombatState, int> RecordOfStates { get; private set; }

        public CharacterStateTree(OTGCombatState _startingState)
        {
            RootNode = new StateNode(_startingState,0);
        }
    }

}
