
using System;
using System.Collections.Generic;
using OTG.CombatSM.Core;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


namespace OTG.CombatSM.EditorTools
{
    public class NewCharacterSubView : CharacterSubViewBase
    {
       

        #region Slides
        private NewCharacterSlideBase m_currentSlide;
        #endregion

        #region Fields
        private NewCharacterCreationData m_creationData;
        private ToolbarBreadcrumbs m_breadcrums;
        private Stack<BreadcrumbData> m_currentBreadcrumbs;
        private Stack<BreadcrumbData> m_availableBreadcrumbs;
        private NewCharacterCreationFactory m_creationFactory;
        #endregion

        #region Public API
        public void OnNewCharacterMakeRequest()
        {

        }
        #endregion

        #region abstract implementatiosn
        public NewCharacterSubView(EditorConfig _config, CharacterViewData _viewData) : base(_viewData, _config)
        {
            m_creationData = new NewCharacterCreationData();
            m_creationFactory = new NewCharacterCreationFactory();
            CreateBreadcrumbs();
            PopulateNextBreadcrumb();
        }
        protected override void Refresh()
        {
            
        }
        public override void UpdateViewHeight(float _height)
        {
            
            base.UpdateViewHeight(_height);
            UpdateCurrentSlideHeight(_height);
        }
        protected override void HandleCharacterSelection()
        {
           
        }
        protected override void HandleOnProjectUpdate()
        {
           
        }
        protected override void HandleOnViewFocused()
        {
            ChangeSlide(m_currentBreadcrumbs.Peek().Slide);
            BindNextButton();
           
        }
        protected override void HandleViewLostFocus()
        {

            UnbindNextButton();
        }
        protected override void HandleOnHierarchyChanged()
        {

        }
        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/NewCharacterSubViewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/NewCharacterSubView/NewCharacterSubViewTemplate.uxml";
            ContainerStyleName = "new-character-subview-main";
        }
        #endregion

        #region Setup Utility
        private void UpdateCurrentSlideHeight(float _height)
        {
            if (m_currentSlide == null)
                return;

            m_currentSlide.UpdateViewHeight(_height);
        }
        private void BindNextButton()
        {
            ContainerElement.Q<Button>("new-character-next-button").clickable.clicked += OnNextButtonClicked;
        }
        private void UnbindNextButton()
        {
            ContainerElement.Q<Button>("new-character-next-button").clickable.clicked -= OnNextButtonClicked;
        }
        private void CreateBreadcrumbs()
        {
            
            m_breadcrums = new ToolbarBreadcrumbs();
            m_availableBreadcrumbs = new Stack<BreadcrumbData>();
            m_availableBreadcrumbs.Push(new BreadcrumbData() { Label = "Character Summary",Order=3 ,Callback = null, Slide = new CharacterSummarySlide(m_creationData) });
            m_availableBreadcrumbs.Push(new BreadcrumbData() { Label = "Character Model", Order = 2,Callback = () => { while (m_breadcrums.childCount > 2) { SubstractBreadcrumb(); } }, Slide = new CharacterModelSlide(m_creationData) });
            m_availableBreadcrumbs.Push(new BreadcrumbData() { Label = "Character Name", Order =1 , Callback = () => { while (m_breadcrums.childCount > 1) { SubstractBreadcrumb(); } }, Slide = new CharacterNameSlide(m_creationData) });

            m_currentBreadcrumbs = new Stack<BreadcrumbData>();

           

            ContainerElement.Q<Toolbar>("new-character-toolbar").Add(m_breadcrums);
        }
        private void ChangeNextButtonText(string _newText)
        {
            ContainerElement.Q<Button>("new-character-next-button").text = _newText;
            
        }
        private void ChangeSlide(NewCharacterSlideBase _newSlide)
        {
            if(m_currentSlide != null)
            {
                m_currentSlide.OnSlideInvisible();
                ContainerElement.Q<VisualElement>("new-character-workflow-slides").Remove(m_currentSlide.ContainerElement);
            }
            m_currentSlide = _newSlide;
            m_currentSlide.OnSlideVisible();
            ContainerElement.Q<VisualElement>("new-character-workflow-slides").Add(m_currentSlide.ContainerElement);

        }
        private void PopulateNextBreadcrumb()
        {
            if (m_availableBreadcrumbs.Count == 0)
                return;
            m_currentBreadcrumbs.Push(m_availableBreadcrumbs.Pop());

            if (m_availableBreadcrumbs.Count == 0)
                ChangeNextButtonText("Create Character and post to heierarchy");

            BreadcrumbData currentBC = m_currentBreadcrumbs.Peek();
            m_breadcrums.PushItem(currentBC.Label, currentBC.Callback);
        }
        private void SubstractBreadcrumb()
        {
            if (m_currentBreadcrumbs.Count == 1)
                return;
            ChangeNextButtonText("Next");
            m_availableBreadcrumbs.Push(m_currentBreadcrumbs.Pop());
            BreadcrumbData bcData = m_currentBreadcrumbs.Peek();
            ChangeSlide(bcData.Slide);
            m_breadcrums.PopItem();
        }
        private void SubtractAllBreadcrumbs()
        {
            while (m_currentBreadcrumbs.Count > 1)
                SubstractBreadcrumb();
        }
        #endregion

        #region Callbacks
        private void OnNextButtonClicked()
        {
           if(m_availableBreadcrumbs.Count == 0)
            {
                m_creationFactory.CreateCharacterWithData(m_creationData, m_editorConfig);
                SubtractAllBreadcrumbs();
                return;
            }
            PopulateNextBreadcrumb();
            ChangeSlide(m_currentBreadcrumbs.Peek().Slide);
           
        }
      
        #endregion
    }
    public class NewCharacterCreationData
    {
        public string CharacterName;
        public e_CombatantType CharacterType;
        public UnityEngine.Object CharacterObject;
    }
    public class BreadcrumbData
    {
        public string Label;
        public int Order;
        public Action Callback;
        public NewCharacterSlideBase Slide;
    }
}
