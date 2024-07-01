using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Main
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        private VisualElement Root => _uiDocument.rootVisualElement;

        private Header _header;
        private Menu _menu;
        private SideMenuPage _sideMenuPage;
        private HomePage _homePage;
        private AboutUsPage _aboutUsPage;
        private CollectionsPage _collectionsPage;
        private LessonsPage _lessonsPage;
        private RecipePage _recipePage;
        private RecipesPage _recipesPage;
        private ShoppingListPage _shoppingListPage;
        private ToolsPage _toolsPage;
        private ToolsOtherPage _toolsOtherPage;
        private WebsitesPage _websitesPage;
        private YoutubePage _youtubePage;

        private VisualElement _sideMenuView;
        private VisualElement _headerView;
        private VisualElement _footerView;
        private VisualElement _homeView;
        private VisualElement _aboutUsView;
        private VisualElement _collectionsView;
        private VisualElement _lessonsView;
        private VisualElement _recipeView;
        private VisualElement _shoppingListView;
        private VisualElement _toolsView;
        private VisualElement _toolsOtherView;
        private VisualElement _websitesView;
        private VisualElement _youtubeView;
        
        private VisualElement swipeArea;
        private Vector2 startPoint;
        private Vector2 endPoint;
        private bool isSwiping;
        
        public event Action OnSwipeLeft;
        public event Action OnSwipeRight;

        private void Start()
        {
            InitViews();
            InitPages();
            
            _homeView.Q("SwipeArea").RegisterCallback<PointerDownEvent>(OnPointerDown);
            _homeView.Q("SwipeArea").RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _homeView.Q("SwipeArea").RegisterCallback<PointerUpEvent>(OnPointerUp);

            OnSwipeRight += _homePage.SetRandomDrink;
            OnSwipeLeft += _homePage.SetRandomDrink;
        }

        private void InitViews()
        {
            _sideMenuView = Root.Q("SideMenu");
            _headerView = Root.Q("Header");
            _footerView = Root.Q("Menu");
            _homeView = Root.Q("Home");
            _aboutUsView = Root.Q("AboutUs");
            _collectionsView = Root.Q("Collections");
            _lessonsView = Root.Q("Lessons");
            _recipeView = Root.Q("Recipe");
            _shoppingListView = Root.Q("ShoppingList");
            _toolsOtherView = Root.Q("ToolsOther");
            _websitesView = Root.Q("Websites");
            _youtubeView = Root.Q("Youtube");
            _toolsView = Root.Q("ToolsPage");
            _websitesView = Root.Q("WebsitesPage");
            _youtubeView = Root.Q("YoutubePage");
            _aboutUsView = Root.Q("AboutUsPage");
        }

        private void InitPages()
        {
            _header = new Header(_headerView, _header, "Home");
            _homePage = new HomePage(_homeView, _header, "Home");
            _collectionsPage = new CollectionsPage(_collectionsView, _header, "Collections");
            _lessonsPage = new LessonsPage(_lessonsView, _header, "Lessons");
            _recipePage = new RecipePage(_recipeView, _header, "Recipe");
            _recipesPage = new RecipesPage(_recipeView, _header, "Recipes");
            _shoppingListPage = new ShoppingListPage(_shoppingListView, _header, "Shopping List");
            _toolsPage = new ToolsPage(_toolsView, _header, "Tools & Other");
            _websitesPage = new WebsitesPage(_websitesView, _header, "Tools & Other");
            _youtubePage = new YoutubePage(_youtubeView, _header, "Tools & Other");
            _aboutUsPage = new AboutUsPage(_aboutUsView, _header, "Tools & Other");

            _toolsPage.OnReturn += () => _toolsPage.ChangePageTo(_toolsOtherPage);
            _websitesPage.OnReturn += () => _websitesPage.ChangePageTo(_toolsOtherPage);
            _youtubePage.OnReturn += () => _youtubePage.ChangePageTo(_toolsOtherPage);
            _aboutUsPage.OnReturn += () => _aboutUsPage.ChangePageTo(_toolsOtherPage);
            
            _toolsOtherPage = new ToolsOtherPage(_toolsOtherView, _header, "Tools & Other", _toolsPage, _websitesPage, _youtubePage, _aboutUsPage);
            _sideMenuPage = new SideMenuPage(_sideMenuView, _header, "", _homePage, _shoppingListPage, _aboutUsPage, _toolsOtherPage);
            _menu = new Menu(_footerView, _header, "", _homePage, _lessonsPage, _recipesPage, _collectionsPage, _toolsOtherPage);
        }
        
        void OnEnable()
        {
            var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            swipeArea = rootVisualElement.Q<VisualElement>("SwipeArea");
        }

        void OnPointerDown(PointerDownEvent evt)
        {
            isSwiping = true;
            startPoint = evt.position;
        }

        void OnPointerMove(PointerMoveEvent evt)
        {
            if (isSwiping)
            {
                endPoint = evt.position;
            }
        }

        void OnPointerUp(PointerUpEvent evt)
        {
            if (isSwiping)
            {
                isSwiping = false;
                endPoint = evt.position;

                DetectSwipe();
            }
        }

        void DetectSwipe()
        {
            Vector2 swipeDelta = endPoint - startPoint;
            if (swipeDelta.magnitude > 50)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                        OnSwipeRight?.Invoke();
                    else
                        OnSwipeLeft?.Invoke();
                }
            }
        }
    }
}