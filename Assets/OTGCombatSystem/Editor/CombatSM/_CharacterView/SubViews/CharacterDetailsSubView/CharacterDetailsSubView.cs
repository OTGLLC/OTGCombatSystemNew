
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterDetailsSubView : CharacterSubViewBase
    {
        #region Data Boxes
        private VisualElement m_movementHandlerData;
        private VisualElement m_inputHandlerData;
        private VisualElement m_collisionHandlerData;
        private VisualElement m_combatHandlerData;
        private VisualElement m_animationHandlerData;
        #endregion

        #region abstract implementatiosn
        public CharacterDetailsSubView(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) { }
        protected override void HandleCharacterSelection()
        {
            BindData(ref m_movementHandlerData, m_charViewData.SelectedCharacterSObject.FindProperty("m_handlerDataGroup").FindPropertyRelative("m_moveHandlerData"));
        }
        protected override void HandleOnProjectUpdate()
        {
           
        }
        protected override void HandleOnViewFocused()
        {
            FindAllDataBoxes();
            
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
            ContainerStyleName = "character-details-subview-main";
        }
        #endregion

        #region Utility
        private void FindAllDataBoxes()
        {
            FindDataBox(ref m_movementHandlerData, "movehandler-data-box");
            FindDataBox(ref m_animationHandlerData, "animationhandler-data-box");
            FindDataBox(ref m_collisionHandlerData, "collisionhandler-data-box");
            FindDataBox(ref m_inputHandlerData, "inputhandler-data-box");
            FindDataBox(ref m_movementHandlerData, "movehandler-data-box");
        }
        private void FindDataBox(ref VisualElement _target, string _boxName)
        {
            _target = ContainerElement.Q<VisualElement>(_boxName);
        }
        private void BindData(ref VisualElement _target, SerializedProperty _source)
        {
            PropertyField field = new PropertyField(_source);

            _target.Add(field);
        }
        #endregion
    }

}
