
using System.Collections.Generic;
using UnityEngine;
using OTG.CombatSM.Core;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


namespace OTG.CombatSM.EditorTools
{
    public class CharacterAnimationSubView : CharacterSubViewBase
    {
        #region Fields
        private ListView m_animationsListView;
        private VisualElement m_animDataBox;
        private PropertyField m_animDataPropfield;
        private SerializedProperty m_selectedCombatAnim;
        private AnimationClip m_selectedClip;
        private ListView m_availableAnimationEventList;
        #endregion

        private List<SerializedProperty> m_animationList;


        #region base Class Implementation
        public CharacterAnimationSubView(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) 
        {
            m_animationList = new List<SerializedProperty>();


            FindDataBoxes();
        }

        protected override void HandleCharacterSelection()
        {
            RetrieveAnimationsFromCombatStateTree();
            PopulateAnimationListView();
            CreateAnimationListView();
 

        }

        protected override void HandleOnHierarchyChanged()
        {
           
        }

        protected override void HandleOnProjectUpdate()
        {
            
        }

        protected override void HandleOnViewFocused()
        {
            CreateAnimationListView();
            m_animationsListView.onSelectionChange += OnAnimationSelected;
            CreateAnimationEventListView();
            PopulateAnimationEventListView();
        }

        protected override void HandleViewLostFocus()
        {
            m_animationsListView.onSelectionChange -= OnAnimationSelected;
            CleanupAnimationListView();

            m_selectedCombatAnim = null;
          
            m_animationList.Clear();

            CleanupAnimationEventListView();
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

        #region Utility
        private void FindDataBoxes()
        {
            m_animDataBox = ContainerElement.Q("animation-data-work-area");
        }
        private void CreateAnimationListView()
        {
            m_animationsListView = ContainerElement.Q<ListView>("animation-list-view");
        }
        private void CleanupAnimationListView()
        {
            m_animationsListView.Clear();
            m_animationsListView = null;
        }
        private void PopulateAnimationListView()
        {
            m_animationsListView.Clear();
            OTGEditorUtility.PopulateListViewSerializedProp(ref m_animationsListView, ref m_containerElement, m_animationList, "animation-list-view","m_animClip",true);
        }
        private void CreateAnimationEventListView()
        {
            m_availableAnimationEventList = ContainerElement.Q<ListView>("animation-event-list");
        }
        private void CleanupAnimationEventListView()
        {
            m_availableAnimationEventList.Clear();
            m_availableAnimationEventList = null;
        }
        private void PopulateAnimationEventListView()
        {
            OTGEditorUtility.PopulateListViewScriptableObject<OTGAnimationEvent>(ref m_availableAnimationEventList, ref m_containerElement, OTGEditorUtility.AvailableAnimationEvents,"animation-event-list");
        }
        #endregion
        private void RetrieveAnimationsFromCombatStateTree()
        {
            m_animationList.Clear();
            StateNode root = m_charViewData.StateTree.RootNode;
            PopulateAnimationSerializedObject(root);
            
        }
        private void PopulateAnimationSerializedObject(StateNode _node)
        {

            if (_node.CombatAnimation != null && !_node.IsRepeatNode)
            {

                m_animationList.Add(_node.CombatAnimation);
            }
                

            if (_node.StateTransitions.Count == 0)
                return;

            foreach (KeyValuePair<OTGCombatState,StateNodeTransition> pair in _node.StateTransitions)
            {
                PopulateAnimationSerializedObject(pair.Value.Transition);
            }
        }
        private void OnAnimationSelected(IEnumerable<object> _obj)
        {
            foreach(object candidate in _obj)
            {
                SerializedProperty prop = (SerializedProperty)candidate;
                if(prop != null)
                {
                    if (m_selectedCombatAnim != null)
                        UnBindData(ref m_animDataBox, ref m_animDataPropfield);

                    m_selectedCombatAnim = prop;

                    m_selectedClip = m_selectedCombatAnim.FindPropertyRelative("m_animClip").objectReferenceValue as AnimationClip;

                    BindData(ref m_animDataBox, ref m_animDataPropfield, m_selectedCombatAnim.serializedObject, m_selectedCombatAnim);
                }
            }
        }
        private void UnBindData(ref VisualElement _target, ref PropertyField _targetField)
        {
            if (_targetField == null || _target == null)
                return;

            _targetField.Unbind();
            _target.Remove(_targetField);
        }
        private void BindData(ref VisualElement _target, ref PropertyField _targetField, SerializedObject _owner, SerializedProperty _propTarget)
        {
            _targetField = new PropertyField(_propTarget);
            _targetField.Bind(_owner);
            _target.Add(_targetField);
        }

    }

}
