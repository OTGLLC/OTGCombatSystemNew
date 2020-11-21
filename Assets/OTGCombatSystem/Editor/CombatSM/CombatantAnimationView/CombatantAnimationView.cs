
using OTG.CombatSM.Core;
using UnityEditorInternal;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.AssetImporters;

namespace OTG.CombatSM.EditorTools
{
   
    public class CombatantAnimationView : CombatantBaseView
    {
       
        public CombatantAnimationView():base()
        {

        }
        #region Utility
        protected override void SetStrings()
        {
            m_templatePath = CombatSMStrings.Templates.AnimView;
            m_stylePath = CombatSMStrings.Styles.AnimView;
            ContainerStyleName = CombatSMStrings.StyleContainerName.AnimView;
        }
        protected override void HandleOnProjectUpdate()
        {
            
        }
        protected override void HandleSelection(CombatantViewData _data)
        {
            PopulateAnimationClipData(_data);
            ModelImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(_data.SelectedAnimationClip)) as ModelImporter;
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(importer.assetPath);
        }
        private void PopulateAnimationClipData(CombatantViewData _data)
        {
            //ContainerElement.Q<VisualElement>("anim-data-area").Clear();
            //PropertyField prop = new PropertyField(_data.AnimationClipData);
            //prop.Bind(_data.SObj_InitialState);
            //ContainerElement.Q<VisualElement>("anim-data-area").Add(prop);
        }
        #endregion
    }

}
