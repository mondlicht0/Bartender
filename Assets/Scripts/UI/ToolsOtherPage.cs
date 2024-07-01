using UnityEngine;
using UnityEngine.UIElements;

public class ToolsOtherPage : BasePage
{
    private VisualElement _tools;
    private VisualElement _websites;
    private VisualElement _youtube;
    private VisualElement _aboutUs;

    private ToolsPage _toolsPage;
    private WebsitesPage _websitesPage;
    private YoutubePage _youtubePage;
    private AboutUsPage _aboutUsPage;
    
    protected override void SetupPage()
    {
        _tools = Root.Q("Tools");
        _websites = Root.Q("Websites");
        _youtube = Root.Q("Youtube");
        _aboutUs = Root.Q("AboutUs");
        
        _tools.RegisterCallback<ClickEvent>(evt => ChangePageTo(_toolsPage));
        _websites.RegisterCallback<ClickEvent>(evt => ChangePageTo(_websitesPage));
        _youtube.RegisterCallback<ClickEvent>(evt => ChangePageTo(_youtubePage));
        _aboutUs.RegisterCallback<ClickEvent>(evt => ChangePageTo(_aboutUsPage));
    }

    public ToolsOtherPage(VisualElement root, Header header, string name, ToolsPage toolsPage, WebsitesPage websitesPage, YoutubePage youtubePage, AboutUsPage aboutUsPage) : base(root, header, name)
    {
        _toolsPage = toolsPage;
        _websitesPage = websitesPage;
        _youtubePage = youtubePage;
        _aboutUsPage = aboutUsPage;
        SetupPage();
    }
}
