
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;

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
        private ListView m_availabeStatesListView;
        private ListView m_animationListView;
        private CharacterStateNode m_selectedNode;
        private TextField m_animFilterField;
        
        #endregion

        #region Public API
        public void OnStateSelected(CharacterStateNode _selectedNode)
        {
            m_selectedNode = _selectedNode;
            PopulateStateDetailsView(_selectedNode.OwningSerializedObject);
            
        }
        #endregion

        #region abstract implementatiosn
        public CharacterGraphSubview(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) 
        {
            m_animFilterField = m_containerElement.Q<TextField>("animation-filter");
           
        }
        protected override void HandleCharacterSelection()
        {
            CleanupGraph();
            CreateNewGraph();

            PopulateAvailableStates();
            AddCallbacksToListView(ref m_availabeStatesListView);
            m_availabeStatesListView.onSelectionChange += OnActionListItemSelected;
            m_stateGraph.OnCharacterSelected();
        }
        protected override void HandleOnProjectUpdate()
        {
         
        }
        protected override void HandleOnViewFocused()
        {
            CleanupGraph();
            CreateNewGraph();
            OTGEditorUtility.PopulateListViewScriptableObject<OTGCombatAction>(ref m_actionListView,ref m_containerElement, OTGEditorUtility.ActionsInstantiated ,"action-list-area");
            OTGEditorUtility.PopulateListViewScriptableObject<OTGTransitionDecision>(ref m_transitionListView,ref m_containerElement, OTGEditorUtility.TransitionsInstantiated, "transition-list-area");
            OTGEditorUtility.PopulateListView<string>(ref m_animationListView, ref m_containerElement, OTGEditorUtility.AvailableAnimationClips, "animation-list-area",true);
            AddCallbacksToListView(ref m_actionListView);
            AddCallbacksToListView(ref m_transitionListView);
            AddCallbacksToListView(ref m_animationListView);
            m_actionListView.onSelectionChange += OnActionListItemSelected;
            m_transitionListView.onSelectionChange += OnActionListItemSelected;
            m_animationListView.onSelectionChange += OnAnimationListItemSelected;
            SubscribeToButtonCallbacks();

            m_animFilterField.RegisterValueChangedCallback(OnTextChanged);
        }
        
      
        protected override void HandleViewLostFocus()
        {
            RemoveCallbacksFromListView(ref m_actionListView);
            RemoveCallbacksFromListView(ref m_transitionListView);
            RemoveCallbacksFromListView(ref m_availabeStatesListView);
            RemoveCallbacksFromListView(ref m_animationListView);

            m_actionListView.onSelectionChange -= OnActionListItemSelected;
            m_transitionListView.onSelectionChange -= OnActionListItemSelected;
            m_availabeStatesListView.onSelectionChange -= OnActionListItemSelected;
            m_animationListView.onSelectionChange -= OnAnimationListItemSelected;
            ContainerElement.Q<VisualElement>("state-details-area").Clear();
            CleanupGraph();
            UnSubscribeFromButtonCallBacks();

            m_animFilterField.UnregisterValueChangedCallback(OnTextChanged);
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
        private void AddCallbacksToListView(ref ListView _targetList)
        {
            if (_targetList == null)
                return;

            _targetList.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
            _targetList.RegisterCallback<MouseMoveEvent>(OnMouseMoveEvent);
            _targetList.RegisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }
        private void RemoveCallbacksFromListView(ref ListView _targetList)
        {
            if (_targetList == null)
                return;

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

            DisplayAnimationPropertyField(_targetState);
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_onEnterActions"), "On Enter Actions");
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_onUpdateActions"), "On Update Actions");
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_animUpdateActions"), "On Animator Move Actions");
            PopulateReorderableList(_targetState, _targetState.FindProperty("m_onExitActions"), "On Exit Actions");
            PopulateTransitionList(_targetState, _targetState.FindProperty("m_stateTransitions"), "Transitions");
        }
        private void DisplayAnimationPropertyField(SerializedObject _targetState)
        {
            PropertyField animProp = new PropertyField(_targetState.FindProperty("m_combatAnim").FindPropertyRelative("m_animClip"));
            animProp.Bind(_targetState);
            ContainerElement.Q<VisualElement>("state-details-area").Add(animProp);
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
        private void PopulateAvailableStates()
        {
            OTGEditorUtility.FindCharacterStates(m_charViewData.SelectedCharacter.name, m_editorConfig);
            OTGEditorUtility.PopulateListViewScriptableObject<OTGCombatState>(ref m_availabeStatesListView,ref m_containerElement, OTGEditorUtility.AvailableCharacterStates, "state-list-area");
        }
        
       
        #endregion

        #region Callbacks
        private void OnRefreshActions()
        {
            OTGEditorUtility.RegisterActions();
            OTGEditorUtility.FindAllActions(m_editorConfig);
            OTGEditorUtility.PopulateListViewScriptableObject<OTGCombatAction>(ref m_actionListView,ref m_containerElement, OTGEditorUtility.ActionsInstantiated, "action-list-area");
        }
        private void OnRefreshTransitions()
        {
            OTGEditorUtility.RegisterTransitions();
            OTGEditorUtility.FindAllTransitions(m_editorConfig);
            OTGEditorUtility.PopulateListViewScriptableObject<OTGTransitionDecision>(ref m_transitionListView,ref m_containerElement, OTGEditorUtility.TransitionsInstantiated, "transition-list-area");
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
        private void OnAnimationListItemSelected(IEnumerable<object> _obj)
        {
            foreach (string actionCandidate in _obj)
            {
                
                if (Event.current.type == EventType.MouseDown)
                {
                    AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(actionCandidate.ToString());
                    Selection.activeObject = clip;
                    m_draggedItems[0] = clip;
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
            string textBoxValue = ContainerElement.Q<TextField>("new-state-name-textfield").text;
            string folder =    OTGEditorUtility.GetCharacterStateFolder(m_charViewData.SelectedCharacter.name, m_editorConfig.CharacterPathRoot);
            string stateName = OTGEditorUtility.GetCombatStateName(m_charViewData.SelectedCharacter.name, textBoxValue);
            OTGCombatState newState = ScriptableObject.CreateInstance<OTGCombatState>();
            newState.name = stateName;
            AssetDatabase.CreateAsset(newState, folder + "/" + stateName + ".asset");

            PopulateAvailableStates();
            m_stateGraph.OnNewStateButtonPressed();
        }
        private void OnTextChanged(ChangeEvent<string> changeEv)
        {
            var filteredItems = OTGEditorUtility.AvailableAnimationClips.Where(x => x.ToLower().Contains(changeEv.newValue.ToLower())).ToList();
            OTGEditorUtility.AvailableAnimationClipsFilteredList.Clear();
            OTGEditorUtility.AvailableAnimationClipsFilteredList.AddRange(filteredItems);

            OTGEditorUtility.PopulateListView<string>(ref m_animationListView, ref m_containerElement, OTGEditorUtility.AvailableAnimationClipsFilteredList, "animation-list-area", true);
        }
        #endregion
    }

}

