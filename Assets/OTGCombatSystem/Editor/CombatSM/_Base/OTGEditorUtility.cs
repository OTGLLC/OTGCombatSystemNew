﻿
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using OTG.CombatSM.Core;

using OTG.CombatSM.TwitchFighter;

namespace OTG.CombatSM.EditorTools
{
    public static class OTGEditorUtility
    {
        public enum E_CombatTemplate
        {
            TwitchFighter,
            SideScrollBeatemUpWithLanes,
            None
        }
        public static List<OTGCombatAction> ActionsAvailable { get; private set; } = new List<OTGCombatAction>();
        public static List<OTGCombatAction> ActionsInstantiated { get; private set; } = new List<OTGCombatAction>();
        public static List<OTGTransitionDecision> TransitionsAvailable { get; private set; } = new List<OTGTransitionDecision>();
        public static List<OTGTransitionDecision> TransitionsInstantiated { get; private set; } = new List<OTGTransitionDecision>();
        public static List<OTGCombatState> AvailableCharacterStates { get; private set; } = new List<OTGCombatState>();
        public static List<string> AvailableAnimationClips { get; private set; } = new List<string>();
        public static List<string> AvailableAnimationClipsFilteredList { get; private set; } = new List<string>();
        public static E_CombatTemplate CurrentCombatTemplate { get; private set; } = E_CombatTemplate.TwitchFighter;

        public static void SetCombatTemplate(E_CombatTemplate _template, EditorConfig _config)
        {
            CurrentCombatTemplate = _template;
            RegisterActions();
            RegisterTransitions();
            FindAllAnimationClips();
            FindAllActions(_config);
            FindAllTransitions(_config);
        }
        public static void RefreshProject(EditorConfig _config)
        {
            RegisterActions();
            RegisterTransitions();
            FindAllAnimationClips();
            FindAllActions(_config);
            FindAllTransitions(_config);
        }
        #region Folder and Data
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

        #endregion

        #region Template specific
        private static void RegisterActions()
        {
            ActionsAvailable = new List<OTGCombatAction>();

            switch(CurrentCombatTemplate)
            {
                case E_CombatTemplate.TwitchFighter:
                    RegisterTwitchFighterActions();
                    break;
                case E_CombatTemplate.SideScrollBeatemUpWithLanes:
                    break;
                        
            }
            


        }
        private static void RegisterTransitions()
        {
            TransitionsAvailable.Clear();
            
            switch (CurrentCombatTemplate)
            {
                case E_CombatTemplate.TwitchFighter:
                    RegisterTwitchFighterTransitions();
                    break;
                case E_CombatTemplate.SideScrollBeatemUpWithLanes:
                    break;

            }



        }
        private static void FindAllAnimationClips()
        {
            AvailableAnimationClips?.Clear();

            string[] guids = AssetDatabase.FindAssets("t:AnimationClip");

            for (int i = 0; i < guids.Length; i++)
            {
                AvailableAnimationClips.Add(AssetDatabase.GUIDToAssetPath(guids[i]));

            }

        }
        private static void FindAllActions(EditorConfig _editorConfig)
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
        private static void FindAllTransitions(EditorConfig _editorConfig)
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
                AssetDatabase.CreateAsset(act, _editorConfig.CombatTransitionsPath + "/" + splitFileName[splitFileName.Length - 1] + ".asset");
            }
        }
        #endregion

        #region Combat

        public static void FindCharacterStates(string _characterName, EditorConfig _config)
        {
            // Find all Texture2Ds that have 'co' in their filename, that are labelled with 'architecture' and are placed in 'MyAwesomeProps' folder
            AvailableCharacterStates = new List<OTGCombatState>();
            string searchString = _characterName + " t:OTGCombatState";
            string folderName = GetCharacterStateFolder(_characterName, _config.CharacterPathRoot);
            string[] guids = AssetDatabase.FindAssets(searchString, new[] { folderName });

            for(int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                AvailableCharacterStates.Add(AssetDatabase.LoadAssetAtPath<OTGCombatState>(path));
            }

        }
        
        public static void PopulateListView<T>(ref ListView _targetListView,ref VisualElement _ownerContainer , List<T> _items, string _listAreaName, bool tailOfPath = false)
        {
            _targetListView = _ownerContainer.Query<ListView>(_listAreaName).First();

            _targetListView.Clear();
            _targetListView.makeItem = () => new Label();

            _targetListView.bindItem = (element, i) =>
            {
                string labelText = _items[i].ToString();
                if(tailOfPath)
                {
                    string[] pathSplit = labelText.Split('/');
                    labelText = pathSplit[pathSplit.Length - 1];
                }
                (element as Label).text = labelText;
            };

            _targetListView.itemsSource = _items;
            _targetListView.itemHeight = 16;
            _targetListView.selectionType = SelectionType.Single;
        }
        public static void PopulateListViewScriptableObject<T>(ref ListView _targetListView, ref VisualElement _ownerContainer, List<T> _items, string _listAreaName) where T : ScriptableObject
        {
            _targetListView = _ownerContainer.Query<ListView>(_listAreaName).First();

            _targetListView.Clear();
            _targetListView.makeItem = () => new Label();


            _targetListView.bindItem = (element, i) => (element as Label).text = _items[i].name;
            _targetListView.itemsSource = _items;
            _targetListView.itemHeight = 16;
            _targetListView.selectionType = SelectionType.Single;
        }

        #endregion

        #region --Twitch Fighter---
        private static void RegisterTwitchFighterActions()
        {
            ActionsAvailable.Add(ScriptableObject.CreateInstance<SetDashDistance>());
            ActionsAvailable.Add(ScriptableObject.CreateInstance<CalculateHorizontalMovement>());
            ActionsAvailable.Add(ScriptableObject.CreateInstance<PerformMovement>());
        }
        private static void RegisterTwitchFighterTransitions()
        {
            TransitionsAvailable.Add(ScriptableObject.CreateInstance<HasReachedDesiredDashDistance>());
            
        }
        #endregion
    }


}
