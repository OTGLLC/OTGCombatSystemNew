
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterViewData
    {
        #region Properties
        public List<OTGCombatSMC> CharactersInScene { get; private set; }
        public OTGCombatSMC SelectedCharacter { get; private set; }
        public SerializedObject SelectedCharacterSObject { get; private set; }
        public SerializedObject CharacterHandlerDataObj { get; private set; }
        public SerializedProperty MovementDataProp { get; private set; }
        public SerializedProperty AnimHandlerDataProp { get; private set; }
        public SerializedProperty InputHandlerDataProp { get; private set; }
        public SerializedProperty CollisionHandlerDataProp { get; private set; }
        public SerializedProperty CombatHandlerDataProp { get; private set; }
        #endregion

        #region Public API
        public CharacterViewData()
        {
            CharactersInScene = new List<OTGCombatSMC>();
        }
        public void GetAllCharactersInScene()
        {
            CharactersInScene.Clear();

            OTGCombatSMC[] chars = Object.FindObjectsOfType<OTGCombatSMC>();

            for(int i = 0; i < chars.Length; i++)
            {
                if (!CharactersInScene.Contains(chars[i]))
                    CharactersInScene.Add(chars[i]);
            }

            
        }
        public void SetSelectedCharacter(OTGCombatSMC _selection)
        {
            SelectedCharacter = _selection;
            SelectedCharacterSObject = new SerializedObject(SelectedCharacter);

            GetCharacterHandlerData();
            GetHandlerDataProperties();
        }

        #endregion
        #region Utility
        private void GetCharacterHandlerData()
        {
            CharacterHandlerDataObj = new SerializedObject(SelectedCharacterSObject.FindProperty("m_handlerDataGroup").objectReferenceValue);
        }
        private void GetHandlerDataProperties()
        {
            MovementDataProp = CharacterHandlerDataObj.FindProperty("m_moveHandlerData");
            AnimHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_animHandlerData");
            InputHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_inputHandlerData");
            CollisionHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_collisionHandlerData");
            CombatHandlerDataProp = CharacterHandlerDataObj.FindProperty("m_combatHandlerData");
        }
        #endregion
    }

}
