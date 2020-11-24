
using System.Collections.Generic;
using OTG.CombatSM.Concrete;
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterStateSubview : CharacterSubViewBase
    {
        #region Fields
        private List<OTGCombatAction> m_allActions;
        private List<OTGCombatAction> m_instaniatedActions;
        #endregion

        #region abstract implementatiosn
        public CharacterStateSubview(CharacterViewData _charViewData, EditorConfig _editorConfig) : base(_charViewData, _editorConfig) 
        {
            RegisterActions();
            FindAllActions();
        }
        protected override void HandleCharacterSelection()
        {
            throw new System.NotImplementedException();
        }
        protected override void HandleOnProjectUpdate()
        {
            throw new System.NotImplementedException();
        }
        protected override void HandleOnViewFocused()
        {

        }
        protected override void HandleViewLostFocus()
        {
            throw new System.NotImplementedException();
        }
        protected override void HandleOnHierarchyChanged()
        {
            throw new System.NotImplementedException();
        }
        protected override void SetStrings()
        {
            m_stylePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterStateSubview/CharacterStateSubviewStyle.uss";
            m_templatePath = "Assets/OTGCombatSystem/Editor/CombatSM/_CharacterView/SubViews/CharacterStateSubview/CharacterStateSubviewTemplate.uxml";
            ContainerStyleName = "character-state-subview-main";
        }
        #endregion

        #region Utility
        private void RegisterActions()
        {
            m_allActions = new List<OTGCombatAction>();
            m_allActions.Add(ScriptableObject.CreateInstance<CalculateHorizontalMovement>());
            m_allActions.Add(ScriptableObject.CreateInstance<CalculateVerticalMovenet>());
        }
        private void FindAllActions()
        {
            m_instaniatedActions = new List<OTGCombatAction>();
            //Object[] foundActions = AssetDatabase.LoadAllAssetsAtPath(m_editorConfig.CombatActionsPath);

            string[] actionGuids = AssetDatabase.FindAssets("t:OTGCombatAction");

            m_instaniatedActions.Clear();
            for (int i = 0; i < actionGuids.Length; i++)
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(actionGuids[i]);
                m_instaniatedActions.Add(AssetDatabase.LoadAssetAtPath<OTGCombatAction>(assetPath));
            }

            for(int i = 0; i < m_instaniatedActions.Count; i++)
            {
                for(int j = 0; j < m_allActions.Count; j++)
                {
                    OTGCombatAction actionInstance = m_instaniatedActions[i];
                    OTGCombatAction spawnAction = m_allActions[j];

                    if(actionInstance.GetType() == spawnAction.GetType())
                    {
                        m_allActions.Remove(spawnAction);
                    }
                }
            }

            for(int k = 0; k < m_allActions.Count; k++)
            {
                OTGCombatAction act = m_allActions[k];
                AssetDatabase.CreateAsset(act, m_editorConfig.CombatActionsPath + "/" + act.GetType().ToString() + ".asset");
            }
        }
        #endregion


    }

}

