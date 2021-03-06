﻿

using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;
using System;

namespace OTG.CombatSM.EditorTools
{
    public class NewCharacterCreationFactory
    {
        #region Fields
        private GameObject m_characterGameObject;
        private OTGHitColliderController m_hitColliderObj;
        private OTGTargetingController m_targetingObj;
        private SerializedObject m_characterSMCObj;
        #endregion

        public void CreateCharacterWithData(NewCharacterCreationData _data, EditorConfig _config)
        {
            CreateCharacterDataFolders(_data,_config);
            CreateCharacterGameObject(_data);
            CreateCharacterSavedGraphFile(_data, _config);
            AttachCombatSMC();
            CreateAndAttachHandlerDataGroup(_data,_config);
            LinkGlobalCombatConfig(_data, _config);
            CreateInitialState(_data, _config);
            ApplyCharacterType(_data);
            ApplyCharacterModel(_data);
            AdjustCharacterControllerCapsule();
            AddHitBoxCollider();
            AddTargetingBox(_data,_config);
            AddSFXControllers();

            SetLayers(_data,_config);
            FocusOnAddedCharacter();


            Cleanup();
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
        private void CreateCharacterSavedGraphFile(NewCharacterCreationData _data,EditorConfig _config)
        {
            CharacterSavedGraph savedGraph = ScriptableObject.CreateInstance<CharacterSavedGraph>();
            string path = _config.CharacterSavedGraphsPath + "/" + _data.CharacterName.ToString() + ".asset";
            AssetDatabase.CreateAsset(savedGraph, path);
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
        private void LinkGlobalCombatConfig(NewCharacterCreationData _data, EditorConfig _config)
        {
            

            m_characterSMCObj.FindProperty("m_globalConfig").objectReferenceValue = _config.GlobalCombatConfig;
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

            Animator anim = characterModel.GetComponent<Animator>();
            if (anim != null)
                GameObject.DestroyImmediate(anim);

            characterModel.transform.SetParent(m_characterGameObject.transform);
            characterModel.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        private void AdjustCharacterControllerCapsule()
        {
            m_characterGameObject.GetComponent<CharacterController>().center = new Vector3(0, 1, 0);
        }
        private void AddHitBoxCollider()
        {
            GameObject collider = new GameObject();
            m_hitColliderObj = collider.AddComponent<OTGHitColliderController>();
            
            collider.GetComponent<BoxCollider>().isTrigger = true;
            collider.name = "HitBoxCollider";
            collider.transform.parent = m_characterGameObject.transform;
            collider.transform.position = new Vector3(0, 1, 0);
        }
        private void AddTargetingBox(NewCharacterCreationData _data, EditorConfig _config)
        {
            GameObject targeting = new GameObject();
            m_targetingObj = targeting.AddComponent<OTGTargetingController>();
            targeting.GetComponent<BoxCollider>().isTrigger = true;
            targeting.name = "TargetingController";
            targeting.transform.parent = m_characterGameObject.transform;
            targeting.transform.position = new Vector3(1, 1, 0);

            SerializedObject obj = new SerializedObject(m_targetingObj);
            SerializedProperty prop = obj.FindProperty("m_validTargets");
            if(_data.CharacterType == e_CombatantType.Player)
            {
                prop.intValue = _config.GlobalCombatConfig.EnemyPushBox;
            }
            if(_data.CharacterType == e_CombatantType.Enemy)
            {
                prop.intValue = _config.GlobalCombatConfig.PlayerPushBox;
            }
            obj.ApplyModifiedProperties();
        }
        private void AddSFXControllers()
        {
            foreach(E_SoundFXType type in Enum.GetValues(typeof(E_SoundFXType)))
            {
                GameObject obj = new GameObject();
                obj.name = "OTGSFXController." + type.ToString();

                OTGSoundFXController ctrl = obj.AddComponent<OTGSoundFXController>();
                ctrl.GetComponent<AudioSource>().playOnAwake = false;
               
                SerializedObject sObj = new SerializedObject(ctrl);
                sObj.FindProperty("m_sfxType").intValue = (int)type;
                sObj.ApplyModifiedProperties();

                obj.transform.parent = m_characterGameObject.transform;
                obj.transform.position = Vector3.zero;
            }
        }
        private void SetLayers(NewCharacterCreationData _data, EditorConfig _config)
        {
            if(_data.CharacterType == e_CombatantType.Player)
            {
                m_characterGameObject.layer = Mathf.RoundToInt(Mathf.Log(_config.GlobalCombatConfig.PlayerPushBox.value,2));
                m_hitColliderObj.gameObject.layer = Mathf.RoundToInt(Mathf.Log(_config.GlobalCombatConfig.PlayerHitBox.value,2));
            }
            else if(_data.CharacterType == e_CombatantType.Enemy)
            {

                m_characterGameObject.layer = Mathf.RoundToInt(Mathf.Log(_config.GlobalCombatConfig.EnemyPushBox.value,2));
                m_hitColliderObj.gameObject.layer = Mathf.RoundToInt(Mathf.Log(_config.GlobalCombatConfig.EnemyHitBox.value,2));
            }
        }
        private void Cleanup()
        {
            m_characterGameObject = null;
            m_hitColliderObj = null;
            m_characterSMCObj = null;
        }
        #endregion

    }

}
