using UnityEngine.UIElements;

public class Header : BasePage
{
    private VisualElement _menu;
    private VisualElement _sideMenu;
    private VisualElement _settings;
    private VisualElement _cart;
    private VisualElement _menuSide;
    private Label _headerLabel;
    private Label _homeSide;
    private Label _settingsSide;
    private Label _shoppingSide;
    private Label _toolsSide;
    private Label _aboutSide;
    private Label _changeSide;

    public Header(VisualElement root, Header header, string name) : base(root, header, name)
    {
        SetupPage();
    }
    

    protected override void SetupPage()
    {
        _sideMenu = Root.parent.Q("SideMenu");
        _menu = Root.Q("HeaderMenu");
        _settings = Root.Q("HeaderSettings");
        _cart = Root.Q("HeaderMenu");
        _headerLabel = Root.Q<Label>("HeaderLabel");
        _menu.RegisterCallback<ClickEvent>(ShowSideMenu);
    }

    private void ShowSideMenu(ClickEvent evt)
    {
        _sideMenu.RemoveFromClassList("left-transfom");
    }
    
    private void HideSideMenu(ClickEvent evt)
    {
        _sideMenu.AddToClassList("left-transfom");
    }

    public void ChangeHeaderTitle(string title)
    {
        _headerLabel.text = title;
    }
}