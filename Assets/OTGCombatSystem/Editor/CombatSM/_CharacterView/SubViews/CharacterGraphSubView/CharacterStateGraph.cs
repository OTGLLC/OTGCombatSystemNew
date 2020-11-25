
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

        private CharcaterStateGraphData m_graphData;
        private CharacterViewData m_charViewData;
        private CharacterGraphSubview m_subView;
        #endregion

        
        #region Public API
        public CharacterStateGraph(CharacterViewData _charViewData, CharacterGraphSubview _subView)
        {
            m_charViewData = _charViewData;
            m_subView = _subView;
            m_graphData = new CharcaterStateGraphData();

            AddManipulators();
            ConstructGridBackground();

        }
        public void OnCharacterSelected()
        {
            
            m_graphData.PopulateExistingStateData(m_charViewData.SelectedCharacter);
            CreateStartingNodeGraph();
        }
        public void OnGraphHidden()
        {
            m_selectedNode = null;
            Clear();
            m_graphData.Cleanup();
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
            GenerateNodes(m_graphData.StateDataNodeRoot, 0);

        }
        
        #endregion

        private void GenerateNodes(StateDataNode _stateNode, int _level, Port _outPort = null)
        {
            CharacterStateNode n = GenerateNode(_stateNode);
            AddElement(n);

            Port inPort = GeneratePort(n, Direction.Input);
            n.inputContainer.Add(inPort);
            //if (_outPort != null)
            //{
            //    _outPort.ConnectTo(inPort);
            //}



            foreach(KeyValuePair<string,StateDataNode> pair in _stateNode.NextStates)
            {
                Port portOut = GeneratePort(n, Direction.Output);
                n.outputContainer.Add(portOut);

                GenerateNodes(pair.Value, _level + 1, portOut);
            }

            n.RefreshPorts();
        }

        private CharacterStateNode GenerateNode(StateDataNode _data)
        {
            var node = new CharacterStateNode(_data.StateObj)
            {
                title = _data.StateObj.targetObject.name,
                
                EntryPoint = true,
            };

          
            node.SetPosition(new Rect(100, 200, 100, 150));
            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }
        //private CharacterStateNode GenerateEntryPointNode()
        //{
        //    var node = new CharacterStateNode()
        //    {
        //        title = "START",
        //        GUID = Guid.NewGuid().ToString(),
        //        StateName = "EntryState",
        //        EntryPoint = true
        //    };

        //    var port = GeneratePort(node, Direction.Output);
        //    port.name = "NEXT";
        //    var port2 = GeneratePort(node, Direction.Output);
        //    port.name = "NEXT2";

        //    var portIn = GeneratePort(node, Direction.Input);

        //    node.outputContainer.Add(port);
        //    node.outputContainer.Add(port2);
        //    node.inputContainer.Add(portIn);
        //    port.ConnectTo(portIn);
          
        //    node.SetPosition(new Rect(100, 200, 100, 150));

        //    node.RefreshExpandedState();
        //    node.RefreshPorts();

        //    return node;
        //}

        private Port GeneratePort(CharacterStateNode _targetNode, Direction _portDirection, Port.Capacity _capacity = Port.Capacity.Single)
        {
            return (_targetNode.InstantiatePort(Orientation.Horizontal, _portDirection, _capacity, typeof(float)));
        }
    }

}
