
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

        private SerializedObject m_owningObject;
        private CharacterStateGraph m_ownerGraph;

        #region Public API
        public CharacterStateNode(SerializedObject _owningObj, CharacterStateGraph _ownerGraph)
        {
            GUID = Guid.NewGuid().ToString();
            m_owningObject = _owningObj;
            m_ownerGraph = _ownerGraph;
            InitializeStyleSheet();
            RegisterCallback<MouseDownEvent>(OnMouseDown);
        }
        #endregion

        #region Utility
      private void InitializeStyleSheet()
        {
            
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterStateNodeStyle.uss"));
        }
        private void OnMouseDown(MouseDownEvent e)
        {
            m_ownerGraph.OnNodeSelected(m_owningObject);
        }
        private void RegisterNodeCallbacks(Action<SerializedObject> _targetCallback)
        {
            _targetCallback.Invoke(m_owningObject);
        }
        #endregion

        #region Callbacks
        
        #endregion


    }

}
