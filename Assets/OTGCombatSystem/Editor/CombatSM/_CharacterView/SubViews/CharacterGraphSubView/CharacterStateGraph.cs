
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateGraph : GraphView
    {
        public CharacterStateGraph()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            AddElement(GenerateEntryPointNode());
        }

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

            node.outputContainer.Add(port);
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
