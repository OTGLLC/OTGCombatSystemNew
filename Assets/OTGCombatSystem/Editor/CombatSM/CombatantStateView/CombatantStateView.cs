

using OTG.CombatSM.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CombatantStateView : CombatantBaseView
    {


        #region Fields
        private CombatantStateViewData m_viewData;


        private bool m_GotMouseDown;
        private OTGCombatAction m_selectedAction;
        private Object[] m_actionsDragged = new Object[1];
        #endregion

        #region Public API
        public CombatantStateView() : base()
        {
            m_viewData = new CombatantStateViewData();
            RegisterEnumFieldCallbacks();

            
           
        }
        #endregion
        
        #region Base Class Implementation

        protected override void SetStrings()
        {
            m_templatePath = CombatSMStrings.Templates.StateView;
            m_stylePath = CombatSMStrings.Styles.StateView;
            ContainerStyleName = CombatSMStrings.StyleContainerName.StateView;
        }

        protected override void HandleSelection(CombatantViewData _data)
        {
            PopulateDetails();
        }
        protected override void HandleOnProjectUpdate()
        {
            CreateActionsListView();
        }

        #endregion

        #region Utility
        private void PopulateDetails()
        {
            ContainerElement.Q<VisualElement>("state-details-area").Clear();
            PopulateReorderableList(m_viewData.SelectedCombatState, m_viewData.CurrentStateEnterActions, "On Enter Actions");
            PopulateReorderableList(m_viewData.SelectedCombatState, m_viewData.CurrentStateUpdateActions, "On Update Actions");
            PopulateReorderableList(m_viewData.SelectedCombatState, m_viewData.CurrentStateAnimMoveActions, "On Animator Move Actions");
            PopulateReorderableList(m_viewData.SelectedCombatState, m_viewData.CurrentStateOnExitActions, "On Exit Actions");
            PopulateTransitionList(m_viewData.SelectedCombatState, m_viewData.CurrentStateTransitions, "Transitions");

            CreateActionsListView();
        }
        private void PopulateReorderableList(SerializedObject _owner, SerializedProperty _listItems, string _listName)
        {
            OTGReorderableListViewElement roList = new OTGReorderableListViewElement(_owner, _listItems, _listName);
            roList.Bind(_owner);
            ContainerElement.Q<VisualElement>("state-details-area").Add(roList);
        }
        private void PopulateTransitionList(SerializedObject _owner, SerializedProperty _listItems, string _listName)
        {
           OTGStateTransitionListView transList = new OTGStateTransitionListView(_owner, _listItems, _listName);
            transList.Bind(_owner);
            ContainerElement.Q<VisualElement>("state-details-area").Add(transList);
        }
        private void RegisterEnumFieldCallbacks()
        {
            ContainerElement.Q<EnumField>("action-filter").RegisterCallback<ChangeEvent<System.Enum>>(evt => { FilterLists(evt.newValue); });
            ContainerElement.Q<EnumField>("transition-filter").RegisterCallback<ChangeEvent<System.Enum>>(evt => { FilterLists(evt.newValue); });
        }
        private void FilterLists(System.Enum _incEnum)
        {

            if(_incEnum.GetType()==typeof(E_ActionType))
            {
                
            }
            if(_incEnum.GetType()==typeof(E_TransitionDecisionType))
            {
                
            }
        }
        private void CreateActionsListView()
        {
            ListView targetListView = ContainerElement.Query<ListView>("actions-list").First();
            targetListView.Clear();
            targetListView.makeItem = () => new Label();

            targetListView.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
            targetListView.RegisterCallback<MouseMoveEvent>(OnMouseMoveEvent);
            targetListView.RegisterCallback<MouseUpEvent>(OnMouseUpEvent);


            targetListView.bindItem = (element, i) => (element as Label).text = m_viewData.AllActionsAvailable[i].name;
            targetListView.itemsSource = m_viewData.AllActionsAvailable;
            targetListView.itemHeight = 16;
            targetListView.selectionType = SelectionType.Single;

            
            targetListView.onSelectionChange += (enumerable) =>
            {
                foreach(Object actionCandidate in enumerable)
                {
                    
                    if(Event.current.type == EventType.MouseDown)
                    {
                        m_selectedAction = actionCandidate as OTGCombatAction;
                        m_actionsDragged[0] = m_selectedAction;
                    }
                    if (Event.current.type == EventType.MouseUp)
                    {
                        m_selectedAction = null;
                    }
                }
                 

            };
        }

        private void OnMouseDownEvent(MouseDownEvent e)
        {
            m_GotMouseDown = true;
        }
        private void OnMouseMoveEvent(MouseMoveEvent e)
        {
            if(m_GotMouseDown && e.pressedButtons == 1)
            {

                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = m_actionsDragged;
                DragAndDrop.StartDrag("ActionDrag");
            }
        }
        private void OnMouseUpEvent(MouseUpEvent e)
        {
            m_GotMouseDown = false;
        }
        #endregion

       
        
    }

}
