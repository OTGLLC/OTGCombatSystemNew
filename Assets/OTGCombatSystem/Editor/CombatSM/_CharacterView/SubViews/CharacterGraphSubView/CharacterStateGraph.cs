
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateGraph : GraphView
    {


        #region Fields
        private CharcaterStateGraphData m_graphData;
        #endregion


        #region Public API
        public CharacterStateGraph()
        {
            m_graphData = new CharcaterStateGraphData();
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            
            //AddElement(GenerateEntryPointNode());
            //Clear();
        }
        public void OnCharacterSelected(OTGCombatSMC _selectedCharacter)
        {
            m_graphData.PopulateExistingStateData(_selectedCharacter);
        }
        public void OnGraphHidden()
        {
            m_graphData.Cleanup();
        }
        #endregion

        private CharacterStateNode GenerateEntryPointNode()
        {
            var node = new CharacterStateNode()
            {
                title = "START",
                GUID = Guid.NewGuid().ToString(),
                StateName = "EntryState",
                EntryPoint = true
            };

            var port = GeneratePort(node, Direction.Output);
            port.name = "NEXT";
            var port2 = GeneratePort(node, Direction.Output);
            port.name = "NEXT2";

            var portIn = GeneratePort(node, Direction.Input);

            node.outputContainer.Add(port);
            node.outputContainer.Add(port2);
            node.inputContainer.Add(portIn);
            port.ConnectTo(portIn);
          
            node.SetPosition(new Rect(100, 200, 100, 150));

            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }

        private Port GeneratePort(CharacterStateNode _targetNode, Direction _portDirection, Port.Capacity _capacity = Port.Capacity.Single)
        {
            return (_targetNode.InstantiatePort(Orientation.Horizontal, _portDirection, _capacity, typeof(float)));
        }
    }

}
