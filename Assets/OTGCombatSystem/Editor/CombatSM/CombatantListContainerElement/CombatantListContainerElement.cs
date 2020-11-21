using OTG.CombatSM.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CombatantListContainerElement : VisualElement
    {
        #region Properties
        public VisualElement ContainerElement { get; private set; }
        public string ContainerStyleName { get { return "combatant-list-region"; } }
        #endregion

        #region Fields
        private CombatSMEditorWindow m_owner;
        private OTGCombatSMC[] m_combatantsInScene;
        private ListView m_combatantList;
        #endregion

        #region Public API
        public CombatantListContainerElement(CombatSMEditorWindow _ownerWindow)
        {
            m_owner = _ownerWindow;

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/OTGCombatSystem/Editor/CombatSM/CombatantListContainerElement/CombatantListContainerElementTemplate.uxml");
            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/OTGCombatSystem/Editor/CombatSM/CombatantListContainerElement/CombatantListContainerElementStyle.uss");

            ContainerElement = new VisualElement();
            visualTree.CloneTree(ContainerElement);
            ContainerElement.styleSheets.Add(style);

           
        }
      
        #endregion


        #region Utility
        private void CreateCombatantList()
        {
            m_combatantsInScene = null;



            m_combatantList = ContainerElement.Query<ListView>("combatant-list").First();
            m_combatantList.makeItem = () => new Label();
            m_combatantList.bindItem = (element, i) => (element as Label).text = m_combatantsInScene[i].name;

            m_combatantList.itemsSource = m_combatantsInScene;
            m_combatantList.itemHeight = 16;
            m_combatantList.selectionType = SelectionType.Single;

        }
        #endregion

    }

}
