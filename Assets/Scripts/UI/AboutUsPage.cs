using System;
using UnityEngine.UIElements;

public class AboutUsPage : BasePage
{
    private VisualElement _backArrow;
    public event Action OnReturn; 
    protected override void SetupPage()
    {
        _backArrow = Root.Q<Button>();
        _backArrow.RegisterCallback<ClickEvent>(evt => OnReturn?.Invoke());
    }

    public AboutUsPage(VisualElement root, Header header, string name) : base(root, header, name)
    {
        SetupPage();
    }
}
