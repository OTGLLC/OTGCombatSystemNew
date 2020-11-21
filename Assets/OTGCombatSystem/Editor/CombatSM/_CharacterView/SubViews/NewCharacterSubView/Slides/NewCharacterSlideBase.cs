
using UnityEditor;
using UnityEngine.UIElements;


namespace OTG.CombatSM.EditorTools
{
    public abstract class NewCharacterSlideBase : VisualElement
    {
        #region Properties
        public VisualElement ContainerElement { get; protected set; }
        public string ContainerStyleName { get; protected set; }
        #endregion

        #region Fields
        protected string m_templatePath;
        protected string m_stylePath;
        protected NewCharacterCreationData m_creationData;
        #endregion

        #region Public API
        public NewCharacterSlideBase(NewCharacterCreationData _creationData)
        {
            m_creationData = _creationData;
            SetStrings();

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_templatePath);
            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_stylePath);

            ContainerElement = new VisualElement();
            visualTree.CloneTree(ContainerElement);
            ContainerElement.styleSheets.Add(style);
        }
        public void OnSlideVisible()
        {
            HandleSlideVisible();
        }
        public void OnSlideInvisible()
        {
            HandleSlideInvisible();
        }
        public void UpdateViewHeight(float _height)
        {
            ContainerElement.Q<VisualElement>(ContainerStyleName).style.height = new StyleLength(_height);
        }
        #endregion

        #region Utility
        protected abstract void SetStrings();
        protected abstract void HandleSlideVisible();
        protected abstract void HandleSlideInvisible();
      
        #endregion
    }

}
