

using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class NewCharacterCreationFactory
    {
        #region Fields
        private GameObject m_characterGameObject;
        #endregion

        public void CreateCharacterWithData(NewCharacterCreationData _data, EditorConfig _config)
        {
            CreateCharacterDataFolders(_data,_config);
            CreateCharacterGameObject(_data);
            AttachCombatSMC();

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
        }


        private void FocusOnAddedCharacter()
        {
            Selection.activeObject = m_characterGameObject;
            SceneView.FrameLastActiveSceneViewWithLock();
        }
        private void CreateCharacterDataFolders(NewCharacterCreationData _data, EditorConfig _config)
        {
            string rootCharfolder = _config.CharacterPathRoot + "/"+_data.CharacterName;
           

            AssetDatabase.CreateFolder(_config.CharacterPathRoot, _data.CharacterName);
            AssetDatabase.CreateFolder(rootCharfolder,"Configurations");
            AssetDatabase.CreateFolder(rootCharfolder, _data.CharacterName+"_States");
        }
        #endregion

    }

}
