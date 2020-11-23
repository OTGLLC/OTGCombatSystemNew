
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateSubview : CharacterSubViewBase
    {
        #region abstract implementatiosn
        public CharacterStateSubview(CharacterViewData _charViewData) : base(_charViewData) { }
        protected override void HandleCharacterSelection()
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
        protected override void HandleOnHierarchyChanged()
        {
            throw new System.NotImplementedException();
        }
        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterStateSubview/CharacterStateSubviewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterStateSubview/CharacterStateSubviewTemplate.uxml";
            ContainerStyleName = "character-state-subview-main";
        }
        #endregion
    }

}

