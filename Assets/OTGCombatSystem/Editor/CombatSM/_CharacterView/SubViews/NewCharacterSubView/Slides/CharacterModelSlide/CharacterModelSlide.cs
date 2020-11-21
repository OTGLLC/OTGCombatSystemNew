using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OTG.CombatSM.EditorTools
{
    public class CharacterModelSlide : NewCharacterSlideBase
    {
        #region Base Class implementation
        public CharacterModelSlide(NewCharacterCreationData _data) : base(_data) { }
        protected override void HandleSlideInvisible()
        {
            

        }

        protected override void HandleSlideVisible()
        {
            

        }

        protected override void SetStrings()
        {
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterModelSlide/CharacterModelSlideTemplate.uxml";
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterModelSlide/CharacterModelSlideStyle.uss";
            ContainerStyleName = "new-character-model-slide";
        }
        #endregion
    }

}
