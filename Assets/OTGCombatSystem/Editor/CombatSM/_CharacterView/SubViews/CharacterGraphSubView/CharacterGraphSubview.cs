
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterGraphSubview : CharacterSubViewBase
    {
        private JunkGraph m_jGraph;

        #region abstract implementatiosn
        public CharacterGraphSubview()
        {
            m_jGraph = new JunkGraph();
            ContainerElement.Q<VisualElement>("character-graph-subview-main").Add(m_jGraph);
        }
        protected override void HandleCharacterSelection(OTGCombatSMC _selectedCharacter)
        {
           
        }
        protected override void HandleOnProjectUpdate()
        {
         
        }
        protected override void HandleOnViewFocused()
        {

        }
        protected override void HandleViewLostFocus()
        {
            
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
    public class JunkGraph:GraphView
    {

    }
}

