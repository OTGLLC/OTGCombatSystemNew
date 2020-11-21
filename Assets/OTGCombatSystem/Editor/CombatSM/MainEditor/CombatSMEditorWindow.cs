
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
            
            CreateCombatantList();
        }
        private void OnProjectChange()
        {
            //if (m_combatantViewData != null)
            //{
            //    m_combatantViewData.PopulateActions();
            //    m_combatantViewData.PopulateTransitions();
                
            //}
            //AllViewsHandleProjectUpdate();   
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

       










        private void CreateCombatantList()
        {
           
            //OTGCombatSMC[]  combatantsInScene = FindObjectsOfType<OTGCombatSMC>();


            //m_combatantList = rootVisualElement.Query<ListView>("combatant-list").First();
            //m_combatantList.makeItem = () => new Label();
          
            //m_combatantList.bindItem = (element, i) => (element as Label).text = combatantsInScene[i].name;
            //m_combatantList.itemsSource = combatantsInScene;
            //m_combatantList.itemHeight = 16;
            //m_combatantList.selectionType = SelectionType.Single;

            //m_combatantList.onSelectionChange += (enumerable) =>
            //{
            //    foreach (Object candidate in enumerable)
            //    {
            //        OTGCombatSMC combatant = candidate as OTGCombatSMC;

            //        //CompileDataForCombatantViews(combatant);
            //        //CreateStateList();
            //    }
            //};

        }
        private void CreateStateList()
        {
            //m_stateList = rootVisualElement.Query<ListView>("combat-state-list").First();
            //m_stateList.makeItem = () => new Label();
            //m_stateList.bindItem = (element, i) => (element as Label).text = m_combatantViewData.AvailableStates[i].name;

            //m_stateList.itemsSource = m_combatantViewData.AvailableStates;
            //m_stateList.itemHeight = 16;
            //m_stateList.selectionType = SelectionType.Single;

            //m_stateList.onSelectionChange += (enumerable) =>
            //{
            //    foreach (Object stateCandidate in enumerable)
            //    {
            //        OTGCombatState state = stateCandidate as OTGCombatState;
            //        m_combatantViewData.SetSelectedCombatState(state);

            //        SetSelectedCombatStateOnViews();
            //    }
            //};
        }
       
        
       // private void ChangeView(CombatantBaseView _newView)
       // {
       //     rootVisualElement.Q<VisualElement>("right-details").Remove(m_currentView.ContainerElement);
       //     m_currentView = _newView;
       //     rootVisualElement.Q<VisualElement>("right-details").Add(m_currentView.ContainerElement);
       // }
       // private void SetCurrentView(CombatantBaseView _selectedView)
       // {
       //     m_currentView = _selectedView;
          
       // }
       // private void CompileDataForCombatantViews(OTGCombatSMC _selectedCombatant)
       // {
       //     m_combatantViewData.SetViewData(_selectedCombatant,m_animationView);
       // }
       // private void SetSelectedCombatStateOnViews()
       // {
       //     m_animationView.OnSelectionMade(m_combatantViewData);
       //     m_combatantStateView.OnSelectionMade(m_combatantViewData);
       // }
       // private void BindAllButtons()
       // {
       //     BindButton("anim-details-button", () => {
       //         ChangeView(m_animationView);
       //     });
       //     BindButton("state-details-button", () =>
       //     {
       //         ChangeView(m_combatantStateView);
       //     });
       //     BindButton("character-details-button", () =>
       //     {
       //         ChangeView(m_combatantView);
       //     });
       // }
       // private void BindButton(string _buttonName,System.Action _clickAction)
       // {
       //     rootVisualElement.Q<Button>(_buttonName).clickable.clicked += _clickAction;
       // }
       // private void RegisterAllViews()
       // {
       //     m_allViews = new List<CombatantBaseView>();
       //     m_animationView = new CombatantAnimationView();
       //     m_combatantStateView = new CombatantStateView();
       //     m_combatantView = new CombatantView();

       //     m_allViews.Add(m_animationView);
       //     m_allViews.Add(m_combatantStateView);
       //     m_allViews.Add(m_combatantView);
       // }
       //private void UnregisterAllViews()
       // {
       //     m_allViews.Clear();
       //     m_allViews = null;
       //     m_animationView = null;
       //     m_combatantStateView = null;
       //     m_combatantView = null;
       // }
        private void AllViewsHandleProjectUpdate()
        {
            
            //for(int i = 0; i < m_allViews.Count; i++)
            //{
            //    m_allViews[i].OnProjectUpdated();
            //}
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
