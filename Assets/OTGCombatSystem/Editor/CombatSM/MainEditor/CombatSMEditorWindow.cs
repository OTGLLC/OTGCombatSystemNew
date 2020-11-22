
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using OTG.CombatSM.Core;
using System.Collections.Generic;

namespace OTG.CombatSM.EditorTools
{
    public class CombatSMEditorWindow : EditorWindow
    {
        #region Views
        private CombatSMBaseView m_currentView;
        private CharacterView m_characterView;
        private OTGEventView m_eventView;
        private OptionsView m_optionView;
        #endregion

        #region Fields
        private EditorConfig m_editorConfig;
       
        #endregion


        [MenuItem("OTG Tools/Combatant Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<CombatSMEditorWindow>();
            window.titleContent = new GUIContent("Combatant Editor");
            window.minSize = new Vector2(800, 800);
        }

        #region Unity API
        private void OnEnable()
        {
            InitializeLayout();
            InitializeStyleSheet();
            InitializeMenuBar();
            CreateViews();
            SubscribeButtonsToCallbacks();
           
            
        }

        private void OnGUI()
        {
            UpdateHeightOftheMainContainer();
            UpdateViewHeight();
        }
        private void OnHierarchyChange()
        {
            m_currentView.OnOnHierarchyChanged();
        }
        private void OnProjectChange()
        {
            m_currentView.OnProjectUpdated();
        }
        private void OnDisable()
        {
            UnsubscribeButtonCallbacks();

            CleanupData();
        }
        #endregion

        #region Utility
        private void InitializeLayout()
        {
            VisualTreeAsset layout = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/OTGCombatSystem/Editor/CombatSM/MainEditor/CombatSMEditorWindow.uxml");
            TemplateContainer treeAsset = layout.CloneTree();
            rootVisualElement.Add(treeAsset);
            
        }
        private void InitializeStyleSheet()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/MainEditor/CombatSMEditorWindowStyles.uss");
            rootVisualElement.styleSheets.Add(styleSheet);
        }
        private void InitializeMenuBar()
        {
            Image img = rootVisualElement.Query<Image>("main-logo");
            img.image = AssetDatabase.LoadAssetAtPath<Texture>("Assets/OTGCombatSystem/Editor/EditorResources/OTG_Black_Transparent.png");
           
        }
        private void UpdateHeightOftheMainContainer()
        {
            rootVisualElement.Q<VisualElement>("main-container").style.height = new
            StyleLength(position.height);
        }
        private void UpdateViewHeight()
        {
            if (m_currentView == null)
                return;

            m_currentView.UpdateViewHeight(position.height);
        }
        private void CleanupData()
        {
           
        }
       
        #endregion

        #region Views
        private void CreateViews()
        {
            m_optionView = new OptionsView(ref m_editorConfig);
            m_characterView = new CharacterView(m_editorConfig);
            m_eventView = new OTGEventView();
            
            SwitchViews(m_characterView);
          
        }
        private void SwitchViews(CombatSMBaseView _newView)
        {
            if (m_currentView != null)
            {
                m_currentView.OnViewLostFocus();
                rootVisualElement.Q<VisualElement>("main-work-view").Remove(m_currentView.ContainerElement);
            }
                
            m_currentView = _newView;

            rootVisualElement.Q<VisualElement>("main-work-view").Add(m_currentView.ContainerElement);
            m_currentView.OnViewFocused();
            
        }
        #endregion

        #region Menu Bar Button Callbacks
        private void SubscribeButtonsToCallbacks()
        {
            Button optionsBtn = rootVisualElement.Query<Button>("main-button-options");
            optionsBtn.clickable.clicked += OnOptionsButtonClicked;

            Button charViewBtn = rootVisualElement.Query<Button>("main-button-character");
            charViewBtn.clickable.clicked += OnCharacterViewButtonClicked;

            Button eventViewerBtn = rootVisualElement.Query<Button>("main-button-events");
            eventViewerBtn.clickable.clicked += OnEventViewButtonClicked;
        }
        private void UnsubscribeButtonCallbacks()
        {
            Button optionsBtn = rootVisualElement.Query<Button>("main-button-options");
            optionsBtn.clickable.clicked -= OnOptionsButtonClicked;

            Button charViewBtn = rootVisualElement.Query<Button>("main-button-character");
            charViewBtn.clickable.clicked -= OnCharacterViewButtonClicked;

            Button eventViewerBtn = rootVisualElement.Query<Button>("main-button-events");
            eventViewerBtn.clickable.clicked -= OnEventViewButtonClicked;
        }
        private void OnCharacterViewButtonClicked()
        {
            if (m_currentView == m_characterView)
                return;

            SwitchViews(m_characterView);
            Debug.Log("Char view clicked");
            
        }
        private void OnEventViewButtonClicked()
        {
            if (m_currentView == m_eventView)
                return;

            SwitchViews(m_eventView);
            Debug.Log("Event view clicked");
        }
        private void OnOptionsButtonClicked()
        {
            if (m_currentView == m_optionView)
                return;

            SwitchViews(m_optionView);
        }
        #endregion
    }

}
