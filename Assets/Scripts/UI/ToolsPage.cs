using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolsPage : BasePage
{
    private VisualElement _backArrow;
    public event Action OnReturn; 
    protected override void SetupPage()
    {
        _backArrow = Root.Q<Button>();
        _backArrow.RegisterCallback<ClickEvent>(evt => OnReturn?.Invoke());
    }

    public ToolsPage(VisualElement root, Header header, string name) : base(root, header, name)
    {
        SetupPage();
    }
}
