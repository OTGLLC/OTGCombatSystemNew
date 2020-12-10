
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
            
            m_selectedNode = selectable as CharacterStateNode;

            if (m_selectedNode == null)
                return;

            m_subView.OnStateSelected(m_selectedNode);
            base.AddToSelection(selectable);

        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatablePorts = new List<Port>();
            ports.ForEach((port) =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatablePorts.Add(port);
            });

            return compatablePorts;
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
            StateNode startingState = m_charViewData.StateTree.RootNode;

            CharacterStateNode n = GenerateNode(startingState);
           
            GenerateChildrenNodes(n);


            
        }
        private CharacterStateNode GenerateNode(StateNode _nodeData)
        {
            CharacterStateNode n = new CharacterStateNode(_nodeData);
            AddElement(n);

            Rect parentPosition = new Rect((n.NodeData.Level * 200) + 150, (n.NodeData.Order * 150) + 150, 150, 150);
            n.SetPosition(parentPosition);

            return n;
        }
        private void GenerateChildrenNodes(CharacterStateNode _startingNode)
        {
            foreach (KeyValuePair<OTGCombatState, StateNodeTransition> pair in _startingNode.NodeData.StateTransitions)
            {
               

                Port outPort = _startingNode.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(OTGCombatState));
                string portName = pair.Value.Transition.OwnerState.name.Split('_')[1];
                outPort.portName = portName;
                _startingNode.outputContainer.Add(outPort);
                _startingNode.RefreshExpandedState();
                _startingNode.RefreshPorts();
                Edge e = new Edge();

                if(pair.Value.ShouldReturnToAnExistingState)
                {
                   
                    outPort.portColor = Color.red;
                    //CharacterStateNode n = GenerateNode(pair.Value.Transition);
                    //outPort.ConnectTo(n.InputPort);
                    //e.input = n.InputPort;
                    //e.output = outPort;
                    //n.SetPosition(new Rect((n.NodeData.Level * 150) + 150, (n.NodeData.Order * 150) + 150, 150, 150));
                    //AddElement(e);
                }
                else
                {
                    CharacterStateNode n = GenerateNode(pair.Value.Transition);
                    outPort.ConnectTo(n.InputPort);
                    e.input = n.InputPort;
                    e.output = outPort;
                    AddElement(e);
                    GenerateChildrenNodes(n);
                }
            }
        }
        #endregion
    }

}
