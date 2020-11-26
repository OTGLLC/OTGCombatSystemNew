
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

            CharacterStateNode n = CreateStateNode(startingState);

           Port inPort= GenerateStateNodePort(n, Direction.Input);
            inPort.portName = "Input";
            n.inputContainer.Add(inPort);
           

            AddElement(n);

            foreach (KeyValuePair<OTGCombatState,StateNodeTransition> pair in startingState.StateTransitions)
            {
                Port outPort = GenerateStateNodePort(n, Direction.Output);
                outPort.portName = "Next State";
                n.outputContainer.Add(outPort);
                n.RefreshExpandedState();
                n.RefreshPorts();

                CharacterStateNode next = CreateStateNode(pair.Value.Transition);
                Port inPort2 = GenerateStateNodePort(n, Direction.Input);
                inPort2.portName = "Input";
                next.inputContainer.Add(inPort2);
                Edge e = new Edge();
                
                outPort.ConnectTo(inPort2);

                e.input = inPort2;
                e.output = outPort;
                AddElement(e);
                next.RefreshExpandedState();
                next.RefreshPorts();
                
                AddElement(next);

            }

            
        }
        private CharacterStateNode CreateStateNode(StateNode _dataNode)
        {
            return  new CharacterStateNode(_dataNode);
        }
        private Port GenerateStateNodePort(CharacterStateNode n, Direction _portDirection)
        {
           return n.InstantiatePort(Orientation.Horizontal, _portDirection, Port.Capacity.Single, typeof(OTGCombatState));
        }
        private void SetNodePosition(CharacterStateNode n)
        {

        }
        #endregion
    }

}
