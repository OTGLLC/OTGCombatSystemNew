
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


public abstract class CombatSMBaseView : VisualElement
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
    public CombatSMBaseView()
    {
        SetStrings();

        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_templatePath);
        StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_stylePath);

        ContainerElement = new VisualElement();
        visualTree.CloneTree(ContainerElement);
        ContainerElement.styleSheets.Add(style);
    }
    public void OnProjectUpdated()
    {
        HandleOnProjectUpdate();
    }
    public void OnOnHierarchyChanged()
    {
        HandleOnHierarchyChanged();
    }
    public void OnViewFocused()
    {
        HandleOnViewFocused();
    }
    public void OnViewLostFocus()
    {
        HandleViewLostFocus();
    }
    public void OnViewDestroyed()
    {

    }
    public virtual void UpdateViewHeight(float _height)
    {
        ContainerElement.Q<VisualElement>(ContainerStyleName).style.height = new StyleLength(_height);
    }
    #endregion

    #region abstract methods
    protected abstract void SetStrings();
    protected abstract void HandleOnProjectUpdate();
    protected abstract void HandleOnViewFocused();
    protected abstract void HandleViewLostFocus();
    protected abstract void HandleOnHierarchyChanged();
    #endregion
}
