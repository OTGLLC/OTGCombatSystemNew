

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
            AssetDatabase.CreateFolder(rootCharfolder, _data.CharacterName+"_States");

        }
        private void CreateAndAttachHandlerDataGroup(NewCharacterCreationData _data, EditorConfig _config)
        {
            HandlerDataGroup dataGrp = ScriptableObject.CreateInstance<HandlerDataGroup>();
            dataGrp.name = _data.CharacterName + "_HanderDataGroup";

            string path = OTGEditorUtility.GetCharacterConfigurationsFolder(_data.CharacterName, _config.CharacterPathRoot) + dataGrp.name+".asset";

            AssetDatabase.CreateAsset(dataGrp, path);

            m_characterSMCObj.FindProperty("m_handlerDataGroup").objectReferenceValue = dataGrp;
            m_characterSMCObj.ApplyModifiedProperties();
        }
        private void CreateInitialState(NewCharacterCreationData _data, EditorConfig _config)
        {
            OTGCombatState initialState = ScriptableObject.CreateInstance<OTGCombatState>();
            
        }
        #endregion

    }

}
