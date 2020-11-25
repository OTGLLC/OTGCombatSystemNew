
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using OTG.CombatSM.Core;
using OTG.CombatSM.Concrete;

namespace OTG.CombatSM.EditorTools
{
    public static class OTGEditorUtility
    {
        public static List<OTGCombatAction> ActionsAvailable { get; private set; } = new List<OTGCombatAction>();
        public static List<OTGCombatAction> ActionsInstantiated { get; private set; } = new List<OTGCombatAction>();

        public static List<OTGTransitionDecision> TransitionsAvailable { get; private set; } = new List<OTGTransitionDecision>();
        public static List<OTGTransitionDecision> TransitionsInstantiated { get; private set; } = new List<OTGTransitionDecision>();
        public static void SubscribeToolbarButtonCallback(VisualElement _container, string _buttonName,  Action _callback)
        {
            _container.Q<ToolbarButton>(_buttonName).clickable.clicked += _callback;
        }
        public static void UnSubscribeToolbarButtonCallback(VisualElement _container, string _buttonName,  Action _callback)
        {
            _container.Q<ToolbarButton>(_buttonName).clickable.clicked -= _callback;
        }
        public static string GetCharacterRootFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName;
        }
        public static string GetCharacterStateFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName+"/"+"States";
        }
        public static string GetCharacterConfigurationsFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "Configurations/";

        }
        public static string GetCharacterPrefabsFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "Prefabs";


        }
        public static string GetCombatStateName(string _characterName, string _stateType)
        {
            string nameFormat = "{0}_{1}_CombatState";
            return string.Format(nameFormat, _characterName, _stateType);
        }


        public static void RegisterActions()
        {
            ActionsAvailable = new List<OTGCombatAction>();
            ActionsAvailable.Add(ScriptableObject.CreateInstance<CalculateHorizontalMovement>());
            ActionsAvailable.Add(ScriptableObject.CreateInstance<CalculateVerticalMovenet>());
            ActionsAvailable.Add(ScriptableObject.CreateInstance<PerformMovement>());
        }
        public static void RegisterTransitions()
        {
            TransitionsAvailable.Clear();
            TransitionsAvailable.Add(ScriptableObject.CreateInstance<PassThrough>());
            TransitionsAvailable.Add(ScriptableObject.CreateInstance<IsGrounded>());
        }
        public static void FindAllActions(EditorConfig _editorConfig)
        {
            string[] actionGuids = AssetDatabase.FindAssets("t:OTGCombatAction");

            ActionsInstantiated.Clear();
            for (int i = 0; i < actionGuids.Length; i++)
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(actionGuids[i]);
                ActionsInstantiated.Add(AssetDatabase.LoadAssetAtPath<OTGCombatAction>(assetPath));
            }

            for (int i = 0; i < ActionsInstantiated.Count; i++)
            {
                for (int j = 0; j < ActionsAvailable.Count; j++)
                {
                    OTGCombatAction actionInstance = ActionsInstantiated[i];
                    OTGCombatAction spawnAction = ActionsAvailable[j];

                    if (actionInstance.GetType() == spawnAction.GetType())
                    {
                        ActionsAvailable.Remove(spawnAction);
                    }
                }
            }

            for (int k = 0; k < ActionsAvailable.Count; k++)
            {
                OTGCombatAction act = ActionsAvailable[k];
                string[] splitFileName = act.GetType().ToString().Split('.');
                AssetDatabase.CreateAsset(act, _editorConfig.CombatActionsPath + "/" + splitFileName[splitFileName.Length - 1] + ".asset");
            }
        }
        public static void FindAllTransitions(EditorConfig _editorConfig)
        {
            string[] transitionGuids = AssetDatabase.FindAssets("t:OTGTransitionDecision");

            TransitionsInstantiated.Clear();
            for (int i = 0; i < transitionGuids.Length; i++)
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(transitionGuids[i]);
                TransitionsInstantiated.Add(AssetDatabase.LoadAssetAtPath<OTGTransitionDecision>(assetPath));
            }

            for (int i = 0; i < TransitionsInstantiated.Count; i++)
            {
                for (int j = 0; j < TransitionsAvailable.Count; j++)
                {
                    OTGTransitionDecision spawnInstance = TransitionsInstantiated[i];
                    OTGTransitionDecision spawnTransition = TransitionsAvailable[j];

                    if (spawnInstance.GetType() == spawnTransition.GetType())
                    {
                        TransitionsAvailable.Remove(spawnTransition);
                    }
                }
            }

            for (int k = 0; k < TransitionsAvailable.Count; k++)
            {
                OTGTransitionDecision act = TransitionsAvailable[k];
                string[] splitFileName = act.GetType().ToString().Split('.');
                AssetDatabase.CreateAsset(act, _editorConfig.CombatTransitionsPath + "/" + splitFileName[splitFileName.Length-1] + ".asset");
            }
        }
    }
 }
