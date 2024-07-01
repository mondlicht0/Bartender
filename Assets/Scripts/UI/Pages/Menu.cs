using UnityEngine;
using UnityEngine.UIElements;

public class Menu
{
    protected VisualElement Root;
    private VisualElement _home;
    private VisualElement _lessons;
    private VisualElement _recipes;
    private VisualElement _collections;
    private VisualElement _other;

    private HomePage _homePage;
    private LessonsPage _lessonsPage;
    private RecipesPage _recipesPage;
    private CollectionsPage _collectionsPage;
    private ToolsOtherPage _otherPage;
    private Header _header;
    
    public Menu(VisualElement root, Header header, string name, HomePage homePage, LessonsPage lessonsPage, RecipesPage recipesPage, CollectionsPage collectionsPage, ToolsOtherPage otherPage)
    {
        Root = root;
        _header = header;
        _homePage = homePage;
        _lessonsPage = lessonsPage;
        _recipesPage = recipesPage;
        _collectionsPage = collectionsPage;
        _otherPage = otherPage;
        SetupPage();
    }

    protected void SetupPage()
    {
        _home = Root.Q("MenuHome");
        _lessons = Root.Q("MenuLessons");
        _recipes = Root.Q("MenuRecipes");
        _collections = Root.Q("MenuCollections");
        _other = Root.Q("MenuTools");
        
        Debug.Log(_home == null);
        
        _home.RegisterCallback<ClickEvent>(evt => ChangePageTo(_homePage, _lessonsPage, _recipesPage, _collectionsPage, _otherPage));
        _lessons.RegisterCallback<ClickEvent>(evt => ChangePageTo(_lessonsPage, _recipesPage, _collectionsPage, _otherPage));
        _recipes.RegisterCallback<ClickEvent>(evt => ChangePageTo(_recipesPage, _lessonsPage, _collectionsPage, _otherPage));
        _collections.RegisterCallback<ClickEvent>(evt => ChangePageTo(_collectionsPage, _recipesPage, _lessonsPage, _otherPage));
        _other.RegisterCallback<ClickEvent>(evt => ChangePageTo(_otherPage, _collectionsPage, _recipesPage, _lessonsPage));
    }
    
    public void ChangePageTo(BasePage targetPage, params BasePage[] otherPages)
    {
        Debug.Log("Change" + targetPage.Name);
        
        targetPage.Root.RemoveFromClassList("left-transfom");
        targetPage.InvokeToggle();

        foreach (var page in otherPages)
        {
            page.Root.AddToClassList("left-transfom");
        }
        
        _header.ChangeHeaderTitle(targetPage.Name);
    }
}