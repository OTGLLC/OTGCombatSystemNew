
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
        private PropertyField m_moveDataPropField;

        private VisualElement m_inputHandlerData;
        private PropertyField m_inputDataPropField;

        private VisualElement m_collisionHandlerData;
        private PropertyField m_collisionDataPropField;

        private VisualElement m_combatHandlerData;
        private PropertyField m_combatHandlerDataPropField;

        private VisualElement m_animationHandlerData;
        private PropertyField m_animhandlerDataPropField;
        #endregion

        #region abstract implementatiosn
        public CharacterDetailsSubView(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) { }
        protected override void HandleCharacterSelection()
        {
            UnBindData(ref m_movementHandlerData, ref m_moveDataPropField);
            UnBindData(ref m_inputHandlerData, ref m_inputDataPropField);
            UnBindData(ref m_collisionHandlerData, ref m_collisionDataPropField);
            UnBindData(ref m_combatHandlerData, ref m_combatHandlerDataPropField);
            UnBindData(ref m_animationHandlerData, ref m_animhandlerDataPropField);

            BindData(ref m_movementHandlerData,ref m_moveDataPropField, m_charViewData.CharacterHandlerDataObj,m_charViewData.MovementDataProp);
            BindData(ref m_inputHandlerData, ref m_inputDataPropField, m_charViewData.CharacterHandlerDataObj, m_charViewData.InputHandlerDataProp);
            BindData(ref m_collisionHandlerData, ref m_collisionDataPropField, m_charViewData.CharacterHandlerDataObj, m_charViewData.CollisionHandlerDataProp);
            BindData(ref m_combatHandlerData, ref m_combatHandlerDataPropField, m_charViewData.CharacterHandlerDataObj, m_charViewData.CombatHandlerDataProp);
            BindData(ref m_animationHandlerData, ref m_animhandlerDataPropField, m_charViewData.CharacterHandlerDataObj, m_charViewData.AnimHandlerDataProp);
        }
        protected override void Refresh()
        {

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
            FindDataBox(ref m_combatHandlerData, "combathandler-data-box");
        }
        private void FindDataBox(ref VisualElement _target, string _boxName)
        {
            _target = ContainerElement.Q<VisualElement>(_boxName);
        }
        private void BindData(ref VisualElement _target, ref PropertyField _targetField, SerializedObject _owner, SerializedProperty _propTarget)
        {
            _targetField = new PropertyField(_propTarget);
            _targetField.Bind(_owner);
            _target.Add(_targetField);
        }
        private void UnBindData(ref VisualElement _target, ref PropertyField _targetField)
        {
            if (_targetField == null || _target == null)
                return; 

            _targetField.Unbind();
            _target.Remove(_targetField);
        }
        #endregion
    }

}
