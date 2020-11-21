
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public abstract class CombatantBaseView : VisualElement
    {
        #region Properties
        public VisualElement ContainerElement { get; protected set; }
        public string ContainerStyleName { get; protected set; }
        #endregion

        #region Fields
        protected string m_templatePath;
        protected string m_stylePath;
        #endregion

        #region Public API
        
        public CombatantBaseView()
        {
            SetStrings();

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_templatePath);
            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_stylePath);

            ContainerElement = new VisualElement();
            visualTree.CloneTree(ContainerElement);
            ContainerElement.styleSheets.Add(style);
        }
        public void OnSelectionMade(CombatantViewData _data)
        {
            HandleSelection(_data);
        }
        public void OnProjectUpdated()
        {
            HandleOnProjectUpdate();
        }
        #endregion

        #region Utility
        protected abstract void SetStrings();
        protected abstract void HandleSelection(CombatantViewData _data);
        protected abstract void HandleOnProjectUpdate();
        #endregion
    }

}
