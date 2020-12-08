
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

        #region Controls
        private ToolbarButton m_copyPasteStateButton;
        private ToolbarButton m_newStateButton;
        private EnumField m_stateTemplateEnumField;
        #endregion

        #region Fields

        private CharacterStateGraph m_stateGraph;
        private ListView m_actionListView;
        private ListView m_transitionListView;
        private ListView m_availabeStatesListView;
        private ListView m_animationListView;
        private CharacterStateNode m_selectedNode;
        private TextField m_animFilterField;
        private StateDataCache m_copiedStateCache;
        #endregion

        #region Public API
        public void OnStateSelected(CharacterStateNode _selectedNode)
        {
            if(m_copiedStateCache != null)
            {
                m_copiedStateCache.Cleanup();
                m_copiedStateCache = null;

            }

            m_selectedNode = _selectedNode;
            PopulateStateDetailsView(_selectedNode.OwningSerializedObject);
            DisplayCopyPasteButton();
        }
        #endregion

        #region abstract implementatiosn
        public CharacterGraphSubview(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) 
        {
            m_animFilterField = m_containerElement.Q<TextField>("animation-filter");
           
        }
        protected override void Refresh()
        {

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
            LinkControls();
            HideCopyPasteButton();
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
           
            m_animationListView.onSelectionChange -= OnAnimationListItemSelected;

            if(m_availabeStatesListView != null)
                m_availabeStatesListView.onSelectionChange -= OnActionListItemSelected;
            ContainerElement.Q<VisualElement>("state-details-area").Clear();
            CleanupGraph();
            UnSubscribeFromButtonCallBacks();
            CleanupControls();

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
        private void LinkControls()
        {
            m_newStateButton = ContainerElement.Q<ToolbarButton>("new-state-button");
            m_copyPasteStateButton = ContainerElement.Q<ToolbarButton>("copy-state-button");
            m_stateTemplateEnumField = ContainerElement.Q<EnumField>("new-state-template");
        }
        private void CleanupControls()
        {
            m_newStateButton = null;
            m_copyPasteStateButton = null;
            m_stateTemplateEnumField = null;
        }
        private void HideCopyPasteButton()
        {
            m_copyPasteStateButton.style.visibility = Visibility.Hidden;
        }
        private void DisplayCopyPasteButton()
        {
            if (m_selectedNode == null)
                return;
            m_copyPasteStateButton.style.visibility = Visibility.Visible;
            m_copyPasteStateButton.clickable.clicked -= OnCopyState;
            m_copyPasteStateButton.clickable.clicked -= OnPasteState;
            if (m_copiedStateCache == null)
            {
                m_copyPasteStateButton.text = "Copy State";
                m_copyPasteStateButton.clickable.clicked += OnCopyState;
               
            }
            else
            {

                m_copyPasteStateButton.text = "Paste State";
                m_copyPasteStateButton.clickable.clicked += OnPasteState;
            }

        }
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
        
        private void SubscribeToButtonCallbacks()
        {

            ContainerElement.Q<Button>("refresh-actions-button").clickable.clicked += OnRefreshActions;
            ContainerElement.Q<Button>("refresh-transitions-button").clickable.clicked += OnRefreshTransitions;
           m_newStateButton.clickable.clicked += OnNewStateClicked;
            m_stateTemplateEnumField.RegisterCallback<ChangeEvent<System.Enum>>(evt => { SetTemplateData(evt.newValue); });

        }
        private void UnSubscribeFromButtonCallBacks()
        {
            ContainerElement.Q<Button>("refresh-actions-button").clickable.clicked -= OnRefreshActions;
            ContainerElement.Q<Button>("refresh-transitions-button").clickable.clicked -= OnRefreshTransitions;
            m_newStateButton.clickable.clicked -= OnNewStateClicked;
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
        
       private void CreateNewState(string _stateName)
        {
            string folder = OTGEditorUtility.GetCharacterStateFolder(m_charViewData.SelectedCharacter.name, m_editorConfig.CharacterPathRoot);
            string stateName = OTGEditorUtility.GetCombatStateName(m_charViewData.SelectedCharacter.name, _stateName);
            OTGCombatState newState = ScriptableObject.CreateInstance<OTGCombatState>();
            newState.name = stateName;

            if (m_copiedStateCache != null)
            {
                PopulateStateWithStartingActionsAndTransitions(ref newState, m_copiedStateCache);
            }

            AssetDatabase.CreateAsset(newState, folder + "/" + stateName + ".asset");

            PopulateAvailableStates();
            m_stateGraph.OnNewStateButtonPressed();

        }
        private void PopulateStateWithStartingActionsAndTransitions(ref OTGCombatState _state, StateDataCache _data)
        {
            _data.PopulateState(ref _state);
        }
        #endregion

        #region Callbacks
        private void OnRefreshActions()
        {
            OTGEditorUtility.RefreshProject(m_editorConfig);
            OTGEditorUtility.PopulateListViewScriptableObject<OTGCombatAction>(ref m_actionListView,ref m_containerElement, OTGEditorUtility.ActionsInstantiated, "action-list-area");
        }
        private void OnRefreshTransitions()
        {
            OTGEditorUtility.RefreshProject(m_editorConfig);
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
        
        private void OnNewStateClicked()
        {
            string textBoxValue = ContainerElement.Q<TextField>("new-state-name-textfield").text;
            if (string.IsNullOrEmpty(textBoxValue))
                return;

            CreateNewState(textBoxValue);
           
            
        }
        private void OnTextChanged(ChangeEvent<string> changeEv)
        {
            var filteredItems = OTGEditorUtility.AvailableAnimationClips.Where(x => x.ToLower().Contains(changeEv.newValue.ToLower())).ToList();
            OTGEditorUtility.AvailableAnimationClipsFilteredList.Clear();
            OTGEditorUtility.AvailableAnimationClipsFilteredList.AddRange(filteredItems);

            OTGEditorUtility.PopulateListView<string>(ref m_animationListView, ref m_containerElement, OTGEditorUtility.AvailableAnimationClipsFilteredList, "animation-list-area", true);
        }
        private void OnCopyState()
        {
            m_copiedStateCache = new StateDataCache();
            m_copiedStateCache.CopyNode(m_selectedNode);
            DisplayCopyPasteButton();
        }
        private void OnPasteState()
        {
            string textBoxValue = ContainerElement.Q<TextField>("new-state-name-textfield").text;
            if (string.IsNullOrEmpty(textBoxValue))
                return;

            CreateNewState(textBoxValue);

            m_copiedStateCache.Cleanup();
            m_copiedStateCache = null;
            DisplayCopyPasteButton();
        }
        private void SetTemplateData(System.Enum _incEnum)
        {
            

            if (_incEnum.GetType() == typeof(E_NewCombatStateTemplate))
            {
                E_NewCombatStateTemplate template = (E_NewCombatStateTemplate)_incEnum;
                m_copiedStateCache?.Cleanup();
                m_copiedStateCache = new StateDataCache();

                OTGEditorUtility.PopulateStateByTemplate(template, ref m_copiedStateCache, m_editorConfig);
                ContainerElement.Q<TextField>("new-state-name-textfield").value = template.ToString() + "template selected";
            }
        }
        #endregion
    }
    
    public class StateDataCache
    {
        private OTGCombatAction[] m_onEnterActions;
        private OTGCombatAction[] m_onUpdateActions;
        private OTGCombatAction[] m_onAnimatorMoveActions;
        private OTGCombatAction[] m_onExitActions;


        public StateDataCache()
        {
            
        }

        public void CreateTemplate(SerializedObject _obj)
        {
            GetPropertyValues(ref m_onEnterActions, _obj, "m_onEnterActions");
            GetPropertyValues(ref m_onUpdateActions, _obj, "m_onUpdateActions");
            GetPropertyValues(ref m_onAnimatorMoveActions, _obj, "m_animUpdateActions");
            GetPropertyValues(ref m_onExitActions, _obj, "m_onExitActions");

        }
        public void CopyNode(CharacterStateNode _node)
        {
            GetPropertyValues(ref m_onEnterActions, _node.OwningSerializedObject, "m_onEnterActions");
            GetPropertyValues(ref m_onUpdateActions, _node.OwningSerializedObject, "m_onUpdateActions");
            GetPropertyValues(ref m_onAnimatorMoveActions, _node.OwningSerializedObject, "m_animUpdateActions");
            GetPropertyValues(ref m_onExitActions, _node.OwningSerializedObject, "m_onExitActions");

        }
        public void PopulateState(ref OTGCombatState _state)
        {
            SerializedObject obj = new SerializedObject(_state);
            SetPropertyValues(obj, "m_onEnterActions", m_onEnterActions);
            SetPropertyValues(obj, "m_onUpdateActions", m_onUpdateActions);
            SetPropertyValues(obj, "m_animUpdateActions", m_onAnimatorMoveActions);
            SetPropertyValues(obj, "m_onExitActions", m_onExitActions);

        }
        public void Cleanup()
        {
            
        }

        private void GetPropertyValues(ref OTGCombatAction[] _target, SerializedObject _source, string _propName)
        {
            var props = _source.FindProperty(_propName);
            int size = props.arraySize;
            _target = new OTGCombatAction[size];

            for(int i = 0; i < size; i++)
            {
                _target[i] = props.GetArrayElementAtIndex(i).objectReferenceValue as OTGCombatAction;
            }
        }
        private void SetPropertyValues(SerializedObject _source, string _propName, OTGCombatAction[] _items )
        {
            var props = _source.FindProperty(_propName);
            
            for(int i = 0; i < _items.Length; i++)
            {
                props.InsertArrayElementAtIndex(i);
                props.GetArrayElementAtIndex(i).objectReferenceValue = (OTGCombatAction)_items[i];
            }
            _source.ApplyModifiedProperties();
        }
        private void PopulateProprtyValuesDirectly(ref OTGCombatAction[] _target, List<OTGCombatAction> _source)
        {
            _target = new OTGCombatAction[_source.Count];
            _target = _source.ToArray();

        }
        
    }

}

