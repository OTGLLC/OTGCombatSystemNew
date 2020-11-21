
using OTG.CombatSM.Core;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


namespace OTG.CombatSM.EditorTools
{
    public class CharacterNameSlide : NewCharacterSlideBase
    {


        #region base class implementation
        public CharacterNameSlide(NewCharacterCreationData _data) : base(_data) { }
        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterNameSlide/CharacterNameSlideStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterNameSlide/CharacterNameSlideTemplate.uxml";
            ContainerStyleName = "character-name-slide";
        }
        protected override void HandleSlideInvisible()
        {
            UpdateData();
        }

        protected override void HandleSlideVisible()
        {

        }


        #endregion

        #region Utility
        private void UpdateData()
        {
            m_creationData.CharacterName = ContainerElement.Q<TextField>("charcter-name-text").value;
            m_creationData.CharacterType = (e_CombatantType)ContainerElement.Q<EnumField>("character-type-filter").value;
        }
        #endregion
    }

}
