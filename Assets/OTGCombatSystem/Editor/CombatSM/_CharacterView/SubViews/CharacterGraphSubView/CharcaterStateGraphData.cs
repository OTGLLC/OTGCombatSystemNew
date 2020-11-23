
using System.Collections.Generic;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharcaterStateGraphData
    {

        #region Fields
        private SerializedObject m_rootCharacter;

        #endregion

        #region Properties
        public SerializedObject InitialCombatState { get; private set; }
        public CombatStateData InitialCombatStateData { get; private set; }
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
            InitialCombatState = new SerializedObject(m_rootCharacter.FindProperty("m_startingState").objectReferenceValue);
        }
        #endregion
    }
    public class CombatStateData
    {
        public List<TransitionConnections> TransitionsFromThisState;
    }
    public struct TransitionConnections
    {

    }

}
