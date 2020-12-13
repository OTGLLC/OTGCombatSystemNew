
using OTG.CombatSM.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using System;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateNode : Node
    {
        
        public string GUID;

        public string StateName;

        public bool EntryPoint = false;

        public SerializedObject OwningSerializedObject { get; private set; }
        public Port InputPort { get; private set; }
        public StateNode NodeData { get; private set; }

        #region Public API
        public CharacterStateNode(StateNode n)
        {
            NodeData = n;
            GUID = Guid.NewGuid().ToString();
            OwningSerializedObject = n.OwnerStateObject;
            title = n.OwnerState.name;
            
           
            InitializeStyleSheet();
            CreateInputPort();
        }
        
        #endregion

        #region Utility
      private void InitializeStyleSheet()
        {
            
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterGraphSubView/CharacterStateNodeStyle.uss"));
        }
        private void CreateInputPort()
        {
            InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(OTGCombatState));
            InputPort.portName = "Input";
            this.inputContainer.Add(InputPort);
            RefreshExpandedState();
            RefreshPorts();
        }
        #endregion

        #region Callbacks
        
        #endregion


    }

}
