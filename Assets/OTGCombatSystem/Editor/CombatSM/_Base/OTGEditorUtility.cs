
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public static class OTGEditorUtility
    {
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
            return _rootFolder + "/" + _characterName+"/"+_characterName+"_States/";
        }
        public static string GetCharacterConfigurationsFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "Configurations/";


        }
        public static string GetCharacterPrefabsFolder(string _characterName, string _rootFolder)
        {
            return _rootFolder + "/" + _characterName + "/" + "Prefabs/";


        }
    }

}
