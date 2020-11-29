
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace OTG.CombatSM.EditorTools
{
    public class OptionsView : CombatSMBaseView
    {
        

        #region Fields
        private OptionsViewData m_viewData;
      
        #endregion

        #region abstract class implemenetations

        public OptionsView(ref EditorConfig _config):base()
        {
            m_viewData = new OptionsViewData(ref _config);

            
            
        }
        protected override void Refresh()
        {

        }
        protected override void SetStrings()
        {
            m_stylePath = CombatSMStrings.Styles.OptionView;
            m_templatePath = CombatSMStrings.Templates.OptionView;
            ContainerStyleName = CombatSMStrings.StyleContainerName.OptionView;
        }
        protected override void HandleOnProjectUpdate()
        {
            
        }
        protected override void HandleOnViewFocused()
        {
            SubscribeAllCallbacks();
            BindPathTextField();
        }
        protected override void HandleViewLostFocus()
        {
            UnSubscribeAllCallbacks();
            UnBindPathTextField();
        }
        protected override void HandleOnHierarchyChanged()
        {

        }
        #endregion

        #region Utility


        #endregion

        #region Callbacks
        private void SubscribeAllCallbacks()
        {
            OTGEditorUtility.SubscribeToolbarButtonCallback(ContainerElement, "open-actions-file-picker", OnActionFolderButton);
            OTGEditorUtility.SubscribeToolbarButtonCallback(ContainerElement, "open-transitions-file-picker", OnTransitionFolderButton);
            OTGEditorUtility.SubscribeToolbarButtonCallback(ContainerElement, "open-character-file-picker", OnCharacterDataFolderButton);
        }
        private void UnSubscribeAllCallbacks()
        {
            OTGEditorUtility.UnSubscribeToolbarButtonCallback(ContainerElement, "open-actions-file-picker", OnActionFolderButton);
            OTGEditorUtility.UnSubscribeToolbarButtonCallback(ContainerElement, "open-transitions-file-picker", OnTransitionFolderButton);
            OTGEditorUtility.UnSubscribeToolbarButtonCallback(ContainerElement, "open-character-file-picker", OnCharacterDataFolderButton);
        }
        private void BindPathTextField()
        {
            ContainerElement.Q<TextField>("action-path-field-area").BindProperty(m_viewData.ActionsPathProp);
            ContainerElement.Q<TextField>("transitions-path-field-area").BindProperty(m_viewData.TransitionsPathProperty);
            ContainerElement.Q<TextField>("character-path-field-area").BindProperty(m_viewData.CharacterDataPathProperty);

        }
        private void UnBindPathTextField()
        {
            ContainerElement.Q<TextField>("action-path-field-area").Unbind();
            ContainerElement.Q<TextField>("transitions-path-field-area").Unbind();
            ContainerElement.Q<TextField>("character-path-field-area").Unbind();

        }
        private void OnActionFolderButton()
        {
            string path = EditorUtility.OpenFolderPanel("Select Actions Root Folder", "", "");
            m_viewData.SetActionsPathStringValue(path);

        }
        private void OnTransitionFolderButton()
        {
            string path = EditorUtility.OpenFolderPanel("Select Transitions Root Folder", "", "");
            m_viewData.SetTransitionsPathStringValue(path);
        }
        private void OnCharacterDataFolderButton()
        {
            string path = EditorUtility.OpenFolderPanel("Select Character Data Root Folder", "", "");
            m_viewData.SetCharacterPathStringValue(path);
        }
        #endregion
    }
}

