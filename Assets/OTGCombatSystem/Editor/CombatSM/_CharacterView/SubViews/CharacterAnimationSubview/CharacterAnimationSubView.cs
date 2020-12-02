﻿
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


namespace OTG.CombatSM.EditorTools
{
    public class CharacterAnimationSubView : CharacterSubViewBase
    {
      
        private VisualElement m_animPreviewCont;
        private Editor m_characterEditor;
        private IMGUIContainer m_charIMGUI;

        #region base Class Implementation
        public CharacterAnimationSubView(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) 
        {
            m_animPreviewCont = ContainerElement.Q<VisualElement>("animation-preview");
        }

        protected override void HandleCharacterSelection()
        {
        
            m_charIMGUI = new IMGUIContainer(() =>
            {
           
                if(m_characterEditor == null)
                {
                  
                    m_characterEditor = Editor.CreateEditor(m_charViewData.SelectedCharacter.gameObject);
                }
                    


                m_characterEditor.OnPreviewGUI(GUILayoutUtility.GetRect(300, 300), null);
                
            });
            m_charIMGUI.onGUIHandler += IMGUIHandler;
            m_animPreviewCont.Add(m_charIMGUI);
        }

        protected override void HandleOnHierarchyChanged()
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
            m_charIMGUI.onGUIHandler -= IMGUIHandler;
            m_characterEditor = null;
            m_charIMGUI = null;
        }

        protected override void Refresh()
        {
           
        }

        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterAnimationSubview/CharacterAnimationSubViewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterAnimationSubview/CharacterAnimationSubViewTemplate.uxml";
            ContainerStyleName = "character-animation-subview-main";
        }
        #endregion

        private void IMGUIHandler()
        {
            return;
            Debug.Log("Im GUI handler");
        }
    }

}