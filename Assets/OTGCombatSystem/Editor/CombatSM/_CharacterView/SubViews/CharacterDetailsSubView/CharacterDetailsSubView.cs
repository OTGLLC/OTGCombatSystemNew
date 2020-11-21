
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterDetailsSubView : CharacterSubViewBase
    {


        #region abstract implementatiosn
        
        protected override void HandleCharacterSelection(OTGCombatSMC _selectedCharacter)
        {
            throw new System.NotImplementedException();
        }
        protected override void HandleOnProjectUpdate()
        {
            throw new System.NotImplementedException();
        }
        protected override void HandleOnViewFocused()
        {
            
        }
        protected override void HandleViewLostFocus()
        {
            throw new System.NotImplementedException();
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
