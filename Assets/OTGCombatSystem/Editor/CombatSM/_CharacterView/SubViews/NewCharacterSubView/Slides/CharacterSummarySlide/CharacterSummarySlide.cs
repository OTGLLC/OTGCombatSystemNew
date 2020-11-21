

using UnityEngine.UIElements;


namespace OTG.CombatSM.EditorTools
{
    public class CharacterSummarySlide : NewCharacterSlideBase
    {
        #region Form Constants
        private const string CharName_Field = "name-string";
        private const string CharType_Field = "type-string";
        #endregion

        #region Base Class Implementation
        public CharacterSummarySlide(NewCharacterCreationData _data) : base(_data) { }
        protected override void HandleSlideInvisible()
        {
           
        }

        protected override void HandleSlideVisible()
        {
            UpdateStringData(m_creationData.CharacterName, CharName_Field);
            UpdateStringData(m_creationData.CharacterType.ToString(), CharType_Field);
        }

        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterSummarySlide/CharacterSummarySlideStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterSummarySlide/CharacterSummarySlideTemplate.uxml";
            ContainerStyleName = "new-character-summary-slide";
        }
        #endregion

        #region Utility
        private void UpdateStringData(string _message,string _messageContainer)
        {
            ContainerElement.Q<Label>(_messageContainer).text = _message;

        }
        #endregion
    }

}
