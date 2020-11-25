
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterGraphSubview : CharacterSubViewBase
    {

        #region Fields
        private bool m_GotMouseDown;
        private Object[] m_draggedItems = new Object[1];
        private CharacterStateGraph m_stateGraph;
        private ListView m_actionListView;
        private ListView m_transitionListView;
        private CharacterStateNode m_selectedNode;
        #endregion

        #region Public API
        public void OnStateSelected(CharacterStateNode _selectedNode)
        {
            m_selectedNode = _selectedNode;
            PopulateStateDetailsView(_selectedNode.OwningSerializedObject);
        }
        #endregion

        #region abstract implementatiosn
        public CharacterGraphSubview(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) { }
        protected override void HandleCharacterSelection()
        {
            CleanupGraph();
            CreateNewGraph();

            m_stateGraph.OnCharacterSelected();
        }
        protected override void HandleOnProjectUpdate()
        {
         
        }
        protected override void HandleOnViewFocused()
        {
            CleanupGraph();
            CreateNewGraph();
            PopulateListView<OTGCombatAction>(ref m_actionListView, OTGEditorUtility.ActionsInstantiated ,"action-list-area");
            PopulateListView<OTGTransitionDecision>(ref m_transitionListView, OTGEditorUtility.TransitionsInstantiated, "transition-list-area");
            AddCallbacksToListView(ref m_actionListView);
            AddCallbacksToListView(ref m_transitionListView);
            m_actionListView.onSelectionChange += OnActionListItemSelected;
            m_transitionListView.onSelectionChange += OnActionListItemSelected;
            SubscribeToButtonCallbacks();
        }

      
        protected override void HandleViewLostFocus()
        {
            RemoveCallbacksFromListView(ref m_actionListView);
            RemoveCallbacksFromListView(ref m_transitionListView);

            m_actionListView.onSelectionChange -= OnActionListItemSelected;
            m_transitionListView.onSelectionChange -= OnActionListItemSelected;

            ContainerElement.Q<VisualElement>("state-details-area").Clear();
            CleanupGraph();
            UnSubscribeFromButtonCallBacks();
        }
        protected override void HandleOnHierarchyChanged()
        {
            
        }
        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterGraphSubviewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterGraphSubviewTemplate.uxml";
            ContainerStyleName = "character-graph-subview-main";
        }
        #endregion

        #region Utility
        private void CreateNewGraph()
        {
            m_stateGraph = new CharacterStateGraph(m_charViewData,this)
            {
                name = "State Graph"
            };

            m_stateGraph.StretchToParentSize();
            ContainerElement.Q<VisualElement>("graph-area").Add(m_stateGraph);
        }
        private void CleanupGraph()
        {
            if (m_stateGraph == null)
                return;
            m_selectedNode = null;
            m_stateGraph.OnGraphHidden();
            ContainerElement.Q<VisualElement>("graph-area").Remove(m_stateGraph);
            m_stateGraph = null;
        }
        private void PopulateListView<T>(ref ListView _targetListView, List<T> _items ,string _listAreaName) where T : ScriptableObject
        {
            _targetListView = ContainerElement.Query<ListView>(_listAreaName).First();

            _targetListView.Clear();
            _targetListView.makeItem = () => new Label();


            _targetListView.bindItem = (element, i) => (element as Label).text = _items[i].name;
            _targetListView.itemsSource = _items;
            _targetListView.itemHeight = 16;
            _targetListView.selectionType = SelectionType.Single;
        }
        private void AddCallbacksToListView(ref ListView _targetList)
        {
            _targetList.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
            _targetList.RegisterCallback<MouseMoveEvent>(OnMouseMoveEvent);
            _targetList.RegisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }
        private void RemoveCallbacksFromListView(ref ListView _targetList)
        {
            _targetList.UnregisterCallback<MouseDownEvent>(OnMouseDownEvent);
            _targetList.UnregisterCallback<MouseMoveEvent>(OnMouseMoveEvent);
            _targetList.UnregisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }
        private void SubscribeToButtonCallbacks()
        {

            ContainerElement.Q<Button>("refresh-actions-button").clickable.clicked += OnRefreshActions;
            ContainerElement.Q<Button>("refresh-transitions-button").clickable.clicked += OnRefreshTransitions;
            ContainerElement.Q<Button>("new-state-button").clickable.clicked += OnNewStateClicked;

        }
        private void UnSubscribeFromButtonCallBacks()
        {
            ContainerElement.Q<Button>("refresh-actions-button").clickable.clicked -= OnRefreshActions;
            ContainerElement.Q<Button>("refresh-transitions-button").clickable.clicked -= OnRefreshTransitions;
            ContainerElement.Q<Button>("new-state-button").clickable.clicked -= OnNewStateClicked;
        }
        private void PopulateStateDetailsView(SerializedObject _targetState)
        {
            //"state-details-area"
           ContainerElement.Q<VisualElement>("state-details-area").Clear();

            PopulateReorderableList(_targetState, _targetState.FindProperty("m_onEnterActions"), "On Enter Actions");
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_onUpdateActions"), "On Update Actions");
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_animUpdateActions"), "On Animator Move Actions");
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_onExitActions"), "On Exit Actions");
            PopulateTransitionList(_targetState, _targetState.FindProperty("m_stateTransitions"), "Transitions");
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
        #endregion

        #region Callbacks
        private void OnRefreshActions()
        {
            OTGEditorUtility.RegisterActions();
            OTGEditorUtility.FindAllActions(m_editorConfig);
            PopulateListView<OTGCombatAction>(ref m_actionListView, OTGEditorUtility.ActionsInstantiated, "action-list-area");
        }
        private void OnRefreshTransitions()
        {
            OTGEditorUtility.RegisterTransitions();
            OTGEditorUtility.FindAllTransitions(m_editorConfig);
            PopulateListView<OTGTransitionDecision>(ref m_transitionListView, OTGEditorUtility.TransitionsInstantiated, "transition-list-area");
        }

        private void OnActionListItemSelected(IEnumerable<object> _obj)
        {
            foreach (Object actionCandidate in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {

                    m_draggedItems[0] = actionCandidate;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_draggedItems[0] = null;
                }
            }

        }

        private void OnMouseDownEvent(MouseDownEvent e)
        {
            m_GotMouseDown = true;
        }
        private void OnMouseMoveEvent(MouseMoveEvent e)
        {
            if (m_GotMouseDown && e.pressedButtons == 1)
            {

                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = m_draggedItems;
                DragAndDrop.StartDrag("ActionDrag");
            }
        }
        private void OnMouseUpEvent(MouseUpEvent e)
        {
            m_GotMouseDown = false;
        }
        private void OnNewStateClicked()
        {
            m_stateGraph.OnNewStateButtonPressed();
        }
        #endregion
    }

}

