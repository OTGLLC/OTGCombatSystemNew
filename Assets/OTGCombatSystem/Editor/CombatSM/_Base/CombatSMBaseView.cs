﻿
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


public abstract class CombatSMBaseView : VisualElement
{
    #region Properties
    public VisualElement ContainerElement { get { return m_containerElement; } }
    public string ContainerStyleName { get; protected set; }
    #endregion

    #region Fields
    protected string m_templatePath;
    protected string m_stylePath;
    protected VisualElement m_containerElement;
    #endregion


    #region Public API
    public CombatSMBaseView()
    {
        SetStrings();

        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_templatePath);
        StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_stylePath);

        m_containerElement = new VisualElement();
        visualTree.CloneTree(m_containerElement);
        m_containerElement.styleSheets.Add(style);
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
    public void OnRefreshButtonClicked()
    {

    }
    public virtual void UpdateViewHeight(float _height)
    {
        m_containerElement.Q<VisualElement>(ContainerStyleName).style.height = new StyleLength(_height);
    }
    #endregion

    #region abstract methods
    protected abstract void Refresh();
    protected abstract void SetStrings();
    protected abstract void HandleOnProjectUpdate();
    protected abstract void HandleOnViewFocused();
    protected abstract void HandleViewLostFocus();
    protected abstract void HandleOnHierarchyChanged();
    #endregion
}
