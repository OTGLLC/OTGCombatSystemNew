

using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class NewCharacterCreationFactory
    {
        #region Fields
        private GameObject m_characterGameObject;
        private SerializedObject m_characterSMCObj;
        #endregion

        public void CreateCharacterWithData(NewCharacterCreationData _data, EditorConfig _config)
        {
            CreateCharacterDataFolders(_data,_config);
            CreateCharacterGameObject(_data);
            AttachCombatSMC();
            CreateAndAttachHandlerDataGroup(_data,_config);
            CreateInitialState(_data, _config);
            ApplyCharacterType(_data);
            ApplyCharacterModel(_data);
            FocusOnAddedCharacter();
        }

        #region Utility
        private void CreateCharacterGameObject(NewCharacterCreationData _data)
        {
            m_characterGameObject = new GameObject(_data.CharacterName);
            m_characterGameObject.transform.position = Vector3.zero;
            
            
        }
        private void AttachCombatSMC()
        {
            m_characterGameObject.AddComponent<OTGCombatSMC>();
            m_characterSMCObj = new SerializedObject(m_characterGameObject.GetComponent<OTGCombatSMC>());
        }


        private void FocusOnAddedCharacter()
        {
            Selection.activeObject = m_characterGameObject;
            SceneView.FrameLastActiveSceneViewWithLock();
        }
        private void CreateCharacterDataFolders(NewCharacterCreationData _data, EditorConfig _config)
        {
            string rootCharfolder = OTGEditorUtility.GetCharacterRootFolder(_data.CharacterName, _config.CharacterPathRoot);
           

            AssetDatabase.CreateFolder(_config.CharacterPathRoot, _data.CharacterName);
            AssetDatabase.CreateFolder(rootCharfolder,"Configurations");
            AssetDatabase.CreateFolder(rootCharfolder, "Prefabs");
            AssetDatabase.CreateFolder(rootCharfolder,"States");

        }
        private void CreateAndAttachHandlerDataGroup(NewCharacterCreationData _data, EditorConfig _config)
        {
            HandlerDataGroup dataGrp = ScriptableObject.CreateInstance<HandlerDataGroup>();
            dataGrp.name = _data.CharacterName + "_HanderDataGroup";

            string path = OTGEditorUtility.GetCharacterConfigurationsFolder(_data.CharacterName, _config.CharacterPathRoot);
                        AssetDatabase.CreateAsset(dataGrp, path + dataGrp.name + ".asset");

            m_characterSMCObj.FindProperty("m_handlerDataGroup").objectReferenceValue = dataGrp;
            m_characterSMCObj.ApplyModifiedProperties();
        }
        private void CreateInitialState(NewCharacterCreationData _data, EditorConfig _config)
        {
            OTGCombatState initialState = ScriptableObject.CreateInstance<OTGCombatState>();
            initialState.name = OTGEditorUtility.GetCombatStateName(_data.CharacterName, "Inititial");

            string stateFolder = OTGEditorUtility.GetCharacterStateFolder(_data.CharacterName, _config.CharacterPathRoot);
            string initialStateGUID = AssetDatabase.CreateFolder(stateFolder, "InitialState");
            string initialStatePath = AssetDatabase.GUIDToAssetPath(initialStateGUID);
            AssetDatabase.CreateAsset(initialState, initialStatePath + "/" +initialState.name + ".asset");

            m_characterSMCObj.FindProperty("m_startingState").objectReferenceValue = initialState;
            m_characterSMCObj.ApplyModifiedProperties();

        }
        private void ApplyCharacterType(NewCharacterCreationData _data)
        {
            m_characterSMCObj.FindProperty("m_combatantType").enumValueIndex = (int)_data.CharacterType;
            m_characterSMCObj.ApplyModifiedProperties();
        }
        private void ApplyCharacterModel(NewCharacterCreationData _data)
        {
            GameObject characterModel = (GameObject)PrefabUtility.InstantiatePrefab(_data.CharacterObject);
             
            characterModel.transform.SetParent(m_characterGameObject.transform);
        }
        #endregion

    }

}
