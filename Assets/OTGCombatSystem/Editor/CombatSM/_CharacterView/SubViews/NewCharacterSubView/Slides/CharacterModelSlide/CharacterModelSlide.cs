using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterModelSlide : NewCharacterSlideBase
    {
        #region Base Class implementation
        public CharacterModelSlide(NewCharacterCreationData _data) : base(_data) 
        {

        }
        protected override void HandleSlideInvisible()
        {

            UnregisterDropAreaCallbacks();
        }

        protected override void HandleSlideVisible()
        {
            RegisterDropAreaCallbacks();

        }

        protected override void SetStrings()
        {
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterModelSlide/CharacterModelSlideTemplate.uxml";
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/Slides/CharacterModelSlide/CharacterModelSlideStyle.uss";
            ContainerStyleName = "new-character-model-slide";
        }
        #endregion

        #region Utility
        private void RegisterDropAreaCallbacks()
        {
            VisualElement dropArea = ContainerElement.Q<VisualElement>("character-model-drop-area");

            dropArea.RegisterCallback<DragEnterEvent>(OnDragEnterEvent);
        }
        private void UnregisterDropAreaCallbacks()
        {
            VisualElement dropArea = ContainerElement.Q<VisualElement>("character-model-drop-area");

            dropArea.UnregisterCallback<DragEnterEvent>(OnDragEnterEvent);
        }
        private void UpdateModelDetails(Object _obj)
        {
            ContainerElement.Q<Label>("model-name-string").text = _obj.name;
            
        }
        private void SetCharacterModel(Object _obj)
        {
            m_creationData.CharacterObject = _obj;
        }
        #endregion

        #region Callbacks
        void OnDragEnterEvent(DragEnterEvent e)
        {
            Object[] draggedItem = DragAndDrop.objectReferences;
            if (draggedItem != null && draggedItem.Length > 0)
            {
                SetCharacterModel(draggedItem[0]);
                UpdateModelDetails(draggedItem[0]);
            }
               

            
        }
        #endregion
    }

}
