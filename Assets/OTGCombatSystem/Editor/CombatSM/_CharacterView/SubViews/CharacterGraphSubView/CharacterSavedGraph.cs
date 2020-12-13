using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.EditorTools
{
    [CreateAssetMenu]
    public class CharacterSavedGraph : ScriptableObject
    {
        private Dictionary<string, Vector2> m_nodePositionLookup;


        [HideInInspector]
        [SerializeField] private List<CharacterStateNodePositionData> m_nodePositionData;

        private void OnEnable()
        {
            m_nodePositionLookup = new Dictionary<string, Vector2>();
            for(int i = 0; i < m_nodePositionData.Count; i++)
            {
                if(!m_nodePositionLookup.ContainsKey(m_nodePositionData[i].NodeName))
                {
                    m_nodePositionLookup.Add(m_nodePositionData[i].NodeName, m_nodePositionData[i].NodePosition);
                }
            }
        }

        public Vector2 GetNodePosition(StateNode _nodeData)
        {
            Vector2 position = new Vector2();
            
            if(!m_nodePositionLookup.TryGetValue(_nodeData.OwnerState.name, out position))
            {
                Vector2 pos = GetRawNodePosition(_nodeData);
                m_nodePositionLookup.Add(_nodeData.OwnerState.name, pos);
            }


            return position;
        }

        private Vector2 GetRawNodePosition(StateNode _nodeData)
        {
            Vector2 position = new Vector2((_nodeData.Level * 200) + 150, (_nodeData.Order * 150));
            return position;
        }

    }
    [System.Serializable]
    public struct CharacterStateNodePositionData
    {
        [SerializeField] private string m_nodeName;
        [SerializeField] private Vector2 m_nodePosition;

        public string NodeName { get { return m_nodeName; } }
        public Vector2 NodePosition { get { return m_nodePosition; } }
    }

}