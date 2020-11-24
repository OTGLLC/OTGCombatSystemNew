
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterDetailsSubView : CharacterSubViewBase
    {


        #region abstract implementatiosn
        public CharacterDetailsSubView(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) { }
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
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterDetailsSubView/CharacterDetailsSubViewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterDetailsSubView/CharacterDetailsSubViewTemplate.uxml";
            ContainerStyleName = "character-subview-main";
        }
        #endregion
    }

}
