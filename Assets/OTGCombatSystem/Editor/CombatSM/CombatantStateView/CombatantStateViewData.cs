

using OTG.CombatSM.Core;
using System.Collections.Generic;
using UnityEditor;

namespace OTG.CombatSM.EditorTools
{
    public class CombatantStateViewData
    {
        #region Properties
        public SerializedObject SelectedCombatState { get; private set; }
        public SerializedProperty CurrentStateEnterActions { get; private set; }
        public SerializedProperty CurrentStateUpdateActions { get; private set; }
        public SerializedProperty CurrentStateAnimMoveActions { get; private set; }
        public SerializedProperty CurrentStateOnExitActions { get; private set; }
        public SerializedProperty CurrentStateTransitions { get; private set; }
        public List<OTGCombatAction> AllActionsAvailable { get; private set; }
        public List<OTGTransitionDecision> AllTransitionsAvailable { get; private set; }
        #endregion

        #region Public API
        public void OnCombatStateSelected(OTGCombatState  _selectedState)
        {
            SelectedCombatState = new SerializedObject(_selectedState);
            AllActionsAvailable = new List<OTGCombatAction>();
            AllTransitionsAvailable = new List<OTGTransitionDecision>();
            DiscoverActionsOnState();
        }
        #endregion

        #region Utility 
       private void DiscoverActionsOnState()
        {
            CurrentStateEnterActions = SelectedCombatState.FindProperty("m_onEnterActions");
            CurrentStateUpdateActions = SelectedCombatState.FindProperty("m_onUpdateActions");
            CurrentStateAnimMoveActions = SelectedCombatState.FindProperty("m_animUpdateActions");
            CurrentStateOnExitActions = SelectedCombatState.FindProperty("m_onExitActions");
            CurrentStateTransitions = SelectedCombatState.FindProperty("m_stateTransitions");
        }
        private void RetrieveAllActionsInProject()
        {
            string[] actionGuids = AssetDatabase.FindAssets("t:OTGCombatAction");

            AllActionsAvailable.Clear();
            for (int i = 0; i < actionGuids.Length; i++)
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(actionGuids[i]);
                AllActionsAvailable.Add(AssetDatabase.LoadAssetAtPath<OTGCombatAction>(assetPath));
            }
        }
        private void RetrieveAllTransitionsInProject()
        {
            string[] transitionGuids = AssetDatabase.FindAssets("t:OTGTransitionDecision");

            AllTransitionsAvailable.Clear();
            for (int i = 0; i < transitionGuids.Length; i++)
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(transitionGuids[i]);
                AllTransitionsAvailable.Add(AssetDatabase.LoadAssetAtPath<OTGTransitionDecision>(assetPath));
            }
        }
        #endregion
    }

   
}
