
using System.Collections.Generic;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateSubview : CharacterSubViewBase
    {
        #region Fields
        private List<OTGCombatAction> m_allActions;
        private List<OTGCombatAction> m_instaniatedActions;
        #endregion

        #region abstract implementatiosn
        public CharacterStateSubview(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) 
        {
        
        }
        protected override void Refresh()
        {
            
        }
        protected override void HandleCharacterSelection()
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
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterStateSubview/CharacterStateSubviewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterStateSubview/CharacterStateSubviewTemplate.uxml";
            ContainerStyleName = "character-state-subview-main";
        }
        #endregion

        #region Utility
        
        #endregion


    }

}

