
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterGraphSubview : CharacterSubViewBase
    {
        private CharacterStateGraph m_stateGraph;

        #region abstract implementatiosn
        public CharacterGraphSubview(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) { }
        protected override void HandleCharacterSelection()
        {
            CleanupGraph();
            CreateNewGraph();

            m_stateGraph.OnCharacterSelected();
        }
        protected override void HandleOnProjectUpdate()
        {
         
        }
        protected override void HandleOnViewFocused()
        {
            CleanupGraph();
            CreateNewGraph();
        }
        protected override void HandleViewLostFocus()
        {
            CleanupGraph();
        }
        protected override void HandleOnHierarchyChanged()
        {
            
        }
        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterGraphSubviewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterGraphSubviewTemplate.uxml";
            ContainerStyleName = "character-graph-subview-main";
        }
        #endregion

        #region Utility
        private void CreateNewGraph()
        {
            m_stateGraph = new CharacterStateGraph(m_charViewData)
            {
                name = "State Graph"
            };

            m_stateGraph.StretchToParentSize();
            ContainerElement.Q<VisualElement>("graph-area").Add(m_stateGraph);
        }
        private void CleanupGraph()
        {
            if (m_stateGraph == null)
                return;

            m_stateGraph.OnGraphHidden();
            ContainerElement.Q<VisualElement>("graph-area").Remove(m_stateGraph);
            m_stateGraph = null;
        }
        #endregion
    }

}

