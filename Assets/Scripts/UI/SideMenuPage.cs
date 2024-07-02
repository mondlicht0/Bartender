using UnityEngine.UIElements;

public class SideMenuPage : BasePage
{
    private VisualElement _sideMenu;
    private VisualElement _menuSide;
    private Label _headerLabel;
    private Label _homeSide;
    private Label _settingsSide;
    private Label _shoppingSide;
    private Label _toolsSide;
    private Label _aboutSide;
    private Label _changeSide;
    
    private HomePage _homePage;
    private AboutUsPage _aboutUsPage;
    private ToolsOtherPage _otherPage;
    private ShoppingListPage _shoppingListPage;
    private Header _header;
    
    public SideMenuPage(VisualElement root, Header header, string name, HomePage homePage, ShoppingListPage shoppingListPage, AboutUsPage aboutUs, ToolsOtherPage otherPage) : base(root, header, name)
    {
        _header = header;
        _homePage = homePage;
        _shoppingListPage = shoppingListPage;
        _otherPage = otherPage;
        _aboutUsPage = aboutUs;
        SetupPage();
    }

    protected override void SetupPage()
    {
        _sideMenu = Root.Q("SideMenu");
        _menuSide = _sideMenu.Q("HeaderMenu");
        
        _menuSide.RegisterCallback<ClickEvent>(HideSideMenu);
        _homeSide = _sideMenu.Q<Label>("Home");
        _settingsSide = _sideMenu.Q<Label>("Settings");
        _shoppingSide = _sideMenu.Q<Label>("ShoppingList");
        _toolsSide = _sideMenu.Q<Label>("ToolsResources");
        _aboutSide = _sideMenu.Q<Label>("AboutUs");
        _changeSide = _sideMenu.Q<Label>("ChangeTheme");
        
        _homeSide.RegisterCallback<ClickEvent>(evt => ChangePageTo(_homePage, _shoppingListPage, _otherPage, _aboutUsPage));
        _homeSide.RegisterCallback<ClickEvent>(HideSideMenu);
        _shoppingSide.RegisterCallback<ClickEvent>(evt => ChangePageTo(_shoppingListPage,_otherPage, _aboutUsPage));
        _shoppingSide.RegisterCallback<ClickEvent>(HideSideMenu);
        _toolsSide.RegisterCallback<ClickEvent>(evt => ChangePageTo(_otherPage, _shoppingListPage, _aboutUsPage));
        _toolsSide.RegisterCallback<ClickEvent>(HideSideMenu);
        _aboutSide.RegisterCallback<ClickEvent>(evt => ChangePageTo(_aboutUsPage, _shoppingListPage, _otherPage));
        _aboutSide.RegisterCallback<ClickEvent>(HideSideMenu);
    }
    private void HideSideMenu(ClickEvent evt)
    {
        _sideMenu.AddToClassList("left-transfom");
    }
}