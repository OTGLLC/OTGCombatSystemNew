
using UnityEngine;
using UnityEditor;

namespace OTG.CombatSM.EditorTools
{
    public class OptionsViewData
    {
        #region Constants
        private const string m_editorConfigFile = "Assets/OTGCombatSystem/Editor/CombatSM/Configs/Editor Config.asset";
        #endregion

        #region Properties
        public EditorConfig ConfigAsset { get; set; }
        public SerializedObject EditorOptionsObject { get; private set; }
        public SerializedProperty ActionsPathProp { get; private set; }
        public SerializedProperty TransitionsPathProperty { get; private set; }
        public SerializedProperty CharacterDataPathProperty { get; private set; }
        #endregion

        #region Public API
        public OptionsViewData(ref EditorConfig _editorConfig)
        {
            
            InitializeEditorConfig(ref _editorConfig);
            CreateEditorOptionsObject();
            CreateProperties();
        }
        public void SetActionsPathStringValue(string _val)
        {
            SetPropertyStringValue(ActionsPathProp, _val);
            EditorOptionsObject.ApplyModifiedProperties();
        }
        public void SetTransitionsPathStringValue(string _val)
        {
            SetPropertyStringValue(TransitionsPathProperty, _val);
            EditorOptionsObject.ApplyModifiedProperties();
        }
        public void SetCharacterPathStringValue(string _val)
        {
            SetPropertyStringValue(CharacterDataPathProperty, _val);
            EditorOptionsObject.ApplyModifiedProperties();
        }
        #endregion

        #region Utility
        private void CreateEditorOptionsObject()
        {
            EditorOptionsObject = new SerializedObject(ConfigAsset);
        }
        private void CreateProperties()
        {
            ActionsPathProp = EditorOptionsObject.FindProperty("m_combatActionsPath");
            TransitionsPathProperty = EditorOptionsObject.FindProperty("m_combatTransitionsPath");
            CharacterDataPathProperty = EditorOptionsObject.FindProperty("m_characterPAthRoot");
        }
        private void InitializeEditorConfig(ref EditorConfig _config)
        {

            ConfigAsset = AssetDatabase.LoadAssetAtPath<EditorConfig>(m_editorConfigFile);
            if (ConfigAsset == null)
            {
                ConfigAsset = ScriptableObject.CreateInstance<EditorConfig>();
                AssetDatabase.CreateAsset(ConfigAsset, m_editorConfigFile);
                _config = ConfigAsset;
            }
            else
            {
                _config = ConfigAsset;
            }
        }
        private void SetPropertyStringValue(SerializedProperty _prop,string _val)
        {
            _prop.stringValue = _val;
        }
        #endregion
    }

}
