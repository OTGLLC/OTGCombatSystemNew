
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;
using System.Collections.Generic;


namespace OTG.CombatSM.EditorTools
{
    public class CombatantViewData
    {
        public class StateViewData
        {
            #region Properties
            public SerializedObject SelectedCombatState { get; private set; }
            #endregion

            #region Public API

            #endregion
        }
        public class AnimationViewData
        {

            #region Fields
            private SerializedObject m_selectedCombatState;
            #endregion

            #region Properties
            public SerializedProperty AnimationClipData { get; set; }
            #endregion

            #region Public API
            public AnimationViewData(SerializedObject _selectedCombatState)
            {
                m_selectedCombatState = _selectedCombatState;
            }
            public void DetermineAnimationClipData()
            {
                if(m_selectedCombatState != null)
                {
                    AnimationClipData = m_selectedCombatState.FindProperty("m_combatAnim").FindPropertyRelative("m_animData");
                }
                
            }
            #endregion
        }


        public SerializedObject SObj_InitialState { get; private set; }
        public SerializedProperty SProp_InitialState { get; private set; }
        
        public List<OTGCombatState> AvailableStates { get; private set; }
        public OTGCombatState SelectedState { get; private set; }
        public AnimationClip SelectedAnimationClip { get; private set; }
        
       

        #region SelectionData
        
        #endregion

        #region ViewData
        public AnimationViewData AnimView { get; private set; }
        #endregion

        #region Public API
        public CombatantViewData()
        {

           

            AvailableStates = new List<OTGCombatState>();
           
        }
        public void SetViewData(OTGCombatSMC _selectedCombatant, CombatantAnimationView _animView)
        {
            AvailableStates.Clear();
            DetermineCombatStateObj(_selectedCombatant);
         
            DetermineAvailableCombatStates(_selectedCombatant);
            DetermineSelectedAnimationClip();
         
            
            _animView.OnSelectionMade(this);
        }
        public void Cleanup()
        {
            SObj_InitialState = null;
            SProp_InitialState = null;
            
            SelectedAnimationClip = null;
           
            AvailableStates.Clear();
            AvailableStates = null;
          
        }
        public void SetSelectedCombatState(OTGCombatState _selectedState)
        {
            SelectedState = _selectedState;
            DetermineCombatStateObj(SelectedState);
            DetermineSelectedAnimationClip();
           
        }
        public void PopulateActions()
        {
           
        }
        public void PopulateTransitions()
        {
            
        }
        #endregion

        #region Utility
        private void DetermineCombatStateObj(OTGCombatSMC _selectedCombatant)
        {
            SerializedObject cObj = new SerializedObject(_selectedCombatant);
            SProp_InitialState = cObj.FindProperty("m_startingState");
            SObj_InitialState = new SerializedObject(SProp_InitialState.objectReferenceValue);
        }

        private void DetermineCombatStateObj(OTGCombatState _selectedState)
        {
            SObj_InitialState = new SerializedObject(_selectedState);

        }

        
        private void DetermineSelectedAnimationClip()
        {
            SelectedAnimationClip = (AnimationClip)SObj_InitialState.FindProperty("m_combatAnim").FindPropertyRelative("m_animClip").objectReferenceValue;
        }
        private void DetermineAvailableCombatStates(OTGCombatSMC _selectedCombatant)
        {
            AvailableStates.Add((OTGCombatState)SObj_InitialState.targetObject);
            SerializedProperty transitions = SObj_InitialState.FindProperty("m_stateTransitions");

            GetStatesFromTransition(transitions);

        }

        private void GetStatesFromTransition(SerializedProperty _currentTransitions)
        {
            if (_currentTransitions == null || _currentTransitions.arraySize == 0)
                return;

            int amountOfTransitions = _currentTransitions.arraySize;
            for (int i = 0; i < amountOfTransitions; i++)
            {
                SerializedProperty nextStateProp = _currentTransitions.GetArrayElementAtIndex(i).FindPropertyRelative("m_nextState");
                OTGCombatState nextState = (OTGCombatState)nextStateProp.objectReferenceValue;
                if (nextState != null && !AvailableStates.Contains(nextState))
                {
                    SerializedObject stateSOBJ = new SerializedObject(nextState);
                    GetStatesFromTransition(stateSOBJ.FindProperty("m_stateTransitions"));
                    AvailableStates.Add(nextState);
                }
            }
        }
       
        
       

        #endregion
    }
}
