
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

        #region Public API
        public CharacterStateNode()
        {
            GUID = Guid.NewGuid().ToString();
            InitializeStyleSheet();

        }
        #endregion

        #region Utility
      private void InitializeStyleSheet()
        {
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterStateNodeStyle.uss"));
        }
        
        #endregion


    }

}
