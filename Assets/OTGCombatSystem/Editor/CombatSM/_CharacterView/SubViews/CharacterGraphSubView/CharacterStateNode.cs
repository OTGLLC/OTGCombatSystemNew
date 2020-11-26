
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateNode : Node
    {
        
        public string GUID;

        public string StateName;

        public bool EntryPoint = false;

        public SerializedObject OwningSerializedObject { get; private set; }
       
        #region Public API
        public CharacterStateNode(StateNode n)
        {
            GUID = Guid.NewGuid().ToString();
            OwningSerializedObject = n.OwnerStateObject;

            InitializeStyleSheet();
        }
        
        #endregion

        #region Utility
      private void InitializeStyleSheet()
        {
            
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterStateNodeStyle.uss"));
        }
        #endregion

        #region Callbacks
        
        #endregion


    }

}
