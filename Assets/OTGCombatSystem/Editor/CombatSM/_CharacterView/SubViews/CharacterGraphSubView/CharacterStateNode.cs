
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateNode : Node
    {
        public string GUID;

        public string StateName;

        public bool EntryPoint = false;
    }

}
