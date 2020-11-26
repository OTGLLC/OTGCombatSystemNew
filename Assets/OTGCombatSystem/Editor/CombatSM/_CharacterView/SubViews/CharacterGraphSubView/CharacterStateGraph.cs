
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using OTG.CombatSM.Core;
using System.Collections.Generic;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateGraph : GraphView
    {


        #region Fields
        private CharacterStateNode m_selectedNode;
        private CharacterViewData m_charViewData;
        private CharacterGraphSubview m_subView;
        #endregion

        
        #region Public API
        public CharacterStateGraph(CharacterViewData _charViewData, CharacterGraphSubview _subView)
        {
            m_charViewData = _charViewData;
            m_subView = _subView;

            AddManipulators();
            ConstructGridBackground();
            CreateStartingNodeGraph();
        }
        public void OnCharacterSelected()
        {
            CreateStartingNodeGraph();
        }
        public void OnGraphHidden()
        {
            m_selectedNode = null;
            Clear();
           
        }
        public void OnNewStateButtonPressed()
        {

        }
        #endregion

        #region Base Class Implementation
        public override void AddToSelection(ISelectable selectable)
        {
            m_selectedNode = (CharacterStateNode)selectable;
            m_subView.OnStateSelected(m_selectedNode);
            base.AddToSelection(selectable);

        }
      
        #endregion

        #region Utility
        private void AddManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }
        private void ConstructGridBackground()
        {
            GridBackground grid = new GridBackground();
            grid.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterGraphGridStyle.uss"));

            Insert(0, grid);
            grid.StretchToParentSize();
        }
        private void CreateStartingNodeGraph()
        {
            

        }
        
        #endregion
    }

}
