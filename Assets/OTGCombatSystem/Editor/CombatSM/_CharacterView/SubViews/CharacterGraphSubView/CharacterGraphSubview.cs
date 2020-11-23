
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
        public CharacterGraphSubview()
        {
            
           
        }
        protected override void HandleCharacterSelection(OTGCombatSMC _selectedCharacter)
        {
            
            m_stateGraph.OnCharacterSelected(_selectedCharacter);
        }
        protected override void HandleOnProjectUpdate()
        {
         
        }
        protected override void HandleOnViewFocused()
        {
            m_stateGraph = new CharacterStateGraph()
            {
                name = "State Graph"
            };
            
            m_stateGraph.StretchToParentSize();
            ContainerElement.Q<VisualElement>("graph-area").Add(m_stateGraph);
        }
        protected override void HandleViewLostFocus()
        {
            ContainerElement.Q<VisualElement>("character-graph-subview-main").Remove(m_stateGraph);
            m_stateGraph = null;
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
    }
   
}

