
using OTG.CombatSM.Core;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterView : CombatSMBaseView
    {
        #region Fields
        private CharacterViewData m_viewData;
        private ListView m_charListView;
        private EditorConfig m_config;
        #endregion

        #region SubViews
        private CharacterSubViewBase m_currentSubView;
        private CharacterDetailsSubView m_charDetailsSubView;
        private NewCharacterSubView m_newCharacterSubView;
        private CharacterStateSubview m_characterStateSubView;
        private CharacterGraphSubview m_characterGraphSubView;
        private CharacterAnimationSubView m_charAnimationSubview;
        #endregion

        #region Utility
        private void BuildCharacterToolBar()
        {
            ContainerElement.Q<ToolbarButton>("new-char-button").clickable.clicked += OnNewCharacterClicked;
        }
        private void SetSubviewToolBarCallbacks()
        {
            ContainerElement.Q<ToolbarButton>("details-sub-view-button").clickable.clicked += SwitchToDetailsSubView;
            ContainerElement.Q<ToolbarButton>("state-sub-view-button").clickable.clicked += SwitchToStateSubview;
            ContainerElement.Q<ToolbarButton>("graph-sub-view-button").clickable.clicked += SwitchToGraphsubView;
            ContainerElement.Q<ToolbarButton>("animation-sub-view-button").clickable.clicked += SwitchToAnimationSubView;
        }
        private void CleanupCharacterToolBar()
        {
            ContainerElement.Q<ToolbarButton>("new-char-button").clickable.clicked -= OnNewCharacterClicked;
        }
        private void CleanupSubviewToolbarCallbacks()
        {
            ContainerElement.Q<ToolbarButton>("details-sub-view-button").clickable.clicked -= SwitchToDetailsSubView;
            ContainerElement.Q<ToolbarButton>("state-sub-view-button").clickable.clicked -= SwitchToStateSubview;
            ContainerElement.Q<ToolbarButton>("graph-sub-view-button").clickable.clicked -= SwitchToGraphsubView;
            ContainerElement.Q<ToolbarButton>("animation-sub-view-button").clickable.clicked -= SwitchToAnimationSubView;
        }
        private void CreateNewData(EditorConfig _editorConfig)
        {
            m_viewData = new CharacterViewData(_editorConfig);
        }
        private void GetAllCharactersInScene()
        {
            m_viewData.GetAllCharactersInScene();
        }
        private void GatherVisualElements()
        {
            m_charListView = ContainerElement.Query<ListView>("character-list").First();
        }
        private void ShowListView()
        {

            m_charListView.makeItem = () => new Label();

            m_charListView.bindItem = (element, i) => (element as Label).text = m_viewData.CharactersInScene[i].name;
            m_charListView.itemsSource = m_viewData.CharactersInScene;
            m_charListView.itemHeight = 16;
            m_charListView.selectionType = SelectionType.Single;

            m_charListView.onSelectionChange += (enumerable) =>
            {
                foreach (Object candidate in enumerable)
                {
                    OTGCombatSMC combatant = candidate as OTGCombatSMC;

                    m_viewData.SetSelectedCharacter(combatant);
                    m_currentSubView.OnCharacterSelected();
                }
            };
        }
        private void CreateViews(EditorConfig _editorConfig)
        {
            m_newCharacterSubView = new NewCharacterSubView(_editorConfig, m_viewData);
            m_charDetailsSubView = new CharacterDetailsSubView(m_viewData, _editorConfig);
            m_characterGraphSubView = new CharacterGraphSubview(m_viewData, _editorConfig);
            m_characterStateSubView = new CharacterStateSubview(m_viewData, _editorConfig);
            m_charAnimationSubview = new CharacterAnimationSubView(m_viewData, _editorConfig);
        }
        private void SwitchSubViews(CharacterSubViewBase _newView)
        {
            if(_newView == null)
            {
                Debug.LogWarning("You tried to switch to a vew that is null");
                return;
            }
            if (m_currentSubView == _newView)
                return;

            if(m_currentSubView != null)
            {
                m_currentSubView.OnViewLostFocus();
                ContainerElement.Q<VisualElement>("workflow-area").Remove(m_currentSubView.ContainerElement);
            }

            m_currentSubView = _newView;
            m_currentSubView.OnViewFocused();
            ContainerElement.Q<VisualElement>("workflow-area").Add(m_currentSubView.ContainerElement);
        }
        #endregion

        #region abstract class implementation
        public CharacterView(EditorConfig _editorConfig):base()
        {
            m_config = _editorConfig;
            CreateNewData(m_config);
            //GetAllCharactersInScene();
            GatherVisualElements();
            CreateViews(_editorConfig);
            SwitchSubViews(m_charDetailsSubView);
        }
        protected override void Refresh()
        {
            CreateNewData(m_config);
            //GetAllCharactersInScene();
            GatherVisualElements();
            CreateViews(m_config);
            SwitchSubViews(m_charDetailsSubView);
        }
        protected override void HandleOnProjectUpdate()
        {
            GetAllCharactersInScene();
            ShowListView();
        }
        protected override void HandleOnHierarchyChanged()
        {
            GetAllCharactersInScene();
            ShowListView();
        }
        protected override void SetStrings()
        {
            m_stylePath = CombatSMStrings.Styles.CharView;
            m_templatePath = CombatSMStrings.Templates.CharView;
            ContainerStyleName = CombatSMStrings.StyleContainerName.CharView;
        }
        protected override void HandleOnViewFocused()
        {
            BuildCharacterToolBar();
            SetSubviewToolBarCallbacks();
            ShowListView();
        }
        protected override void HandleViewLostFocus()
        {
            CleanupCharacterToolBar();
            CleanupSubviewToolbarCallbacks();
        }
        public override void UpdateViewHeight(float _height)
        {
            if (m_currentSubView != null)
                m_currentSubView.UpdateViewHeight(_height);
            base.UpdateViewHeight(_height);
        }
        #endregion

        #region Callbacks
        private void OnNewCharacterClicked()
        {
            if(m_currentSubView == m_newCharacterSubView)
            {
                m_newCharacterSubView.OnNewCharacterMakeRequest();
            }
            else
            {
                SwitchSubViews(m_newCharacterSubView);
            }
            
        }
        private void SwitchToDetailsSubView()
        {
            SwitchSubViews(m_charDetailsSubView);
        }
        private void SwitchToStateSubview()
        {
            SwitchSubViews(m_characterStateSubView);
        }
        private void SwitchToGraphsubView()
        {
            SwitchSubViews(m_characterGraphSubView);
        }
        private void SwitchToAnimationSubView()
        {
            SwitchSubViews(m_charAnimationSubview);
        }
        #endregion


    }

}
