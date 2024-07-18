using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Main
{
    public class Main : MonoBehaviour
    {
        public static Main Instance;
        
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private VisualTreeAsset _collectionTemplate;
        [SerializeField] private VisualTreeAsset _shoppingListTemplate;
        private VisualElement Root => _uiDocument.rootVisualElement;

        private Header _header;
        private Menu _menu;
        private SideMenuPage _sideMenuPage;
        private HomePage _homePage;
        private AboutUsPage _aboutUsPage;
        private CollectionsPage _collectionsPage;
        private LessonsPage _lessonsPage;
        private LessonPage _lessonPage;
        private RecipePage _recipePage;
        private RecipesPage _recipesPage;
        private ShoppingListPage _shoppingListPage;
        private ToolsPage _toolsPage;
        private ToolsOtherPage _toolsOtherPage;
        private WebsitesPage _websitesPage;
        private YoutubePage _youtubePage;
        private SettingsPage _settingsPage;

        private VisualElement _sideMenuView;
        private VisualElement _headerView;
        private VisualElement _footerView;
        private VisualElement _homeView;
        private VisualElement _aboutUsView;
        private VisualElement _collectionsView;
        private VisualElement _lessonsView;
        private VisualElement _lessonView;
        private VisualElement _recipeView;
        private VisualElement _recipesView;
        private VisualElement _shoppingListView;
        private VisualElement _toolsView;
        private VisualElement _toolsOtherView;
        private VisualElement _websitesView;
        private VisualElement _youtubeView;
        private VisualElement _settingsView;
        
        private VisualElement swipeArea;
        private Vector2 startPoint;
        private Vector2 endPoint;
        private bool isSwiping;
        
        public List<Lesson> Lessons1 { get; private set; }
        public List<Lesson> Lessons2 { get; private set; }

        public List<Drink> InitialDrinks = new();
        public List<Drink> DrinksInOunce = new();
        public List<Drink> DrinksInMl = new();
        public List<Drink> Drinks = new();
        public bool IsInMl = true;

        public event Action OnSwipeLeft;
        public event Action OnSwipeRight;

        private void Awake()
        {
            Instance = this;
        }

        private void InitLevels()
        {
            Lessons1 = new List<Lesson>()
    {
        new Lesson(
            "Basic Mixing Techniques",
            "Introduction: Start with an interactive tutorial introducing the fundamental principles of mixing ingredients in cocktails. Incorporate visuals and animations to illustrate the process.",
            "Practice Mode: Offer a hands-on practice mode where users can simulate mixing cocktails using virtual ingredients and equipment. Provide step-by-step guidance and feedback on their technique.",
            "Quiz: Include a quiz section where users can test their knowledge of basic mixing techniques and receive instant feedback on their answers.",
            "Images/Lessons/1"
        ),
        new Lesson(
            "Ingredient Exploration",
            "Ingredient Database: Provide a comprehensive database of cocktail ingredients with detailed descriptions, flavor profiles, and recommended pairings. Allow users to explore different types of alcohol, mixers, and garnishes.",
            "Tasting Notes: Allow users to record their tasting notes and ratings for various ingredients. Provide recommendations based on their preferences and suggest cocktail recipes to try.",
            "Ingredient Matcher: Implement a feature that suggests ingredient combinations based on user preferences and flavor profiles. Encourage users to experiment with new flavors and create their own custom cocktails.",
            "Images/Lessons/5"
        ),
        new Lesson(
            "Pouring and Shaking Techniques",
            "Tutorial Videos: Curate a series of tutorial videos demonstrating proper pouring and shaking techniques. Break down each step of the process and highlight common mistakes to avoid.",
            "Virtual Bartender: Introduce a virtual bartender feature that guides users through the process of pouring and shaking cocktails in real-time. Provide feedback on their technique and offer tips for improvement.",
            "Progress Tracker: Track users' progress as they practice pouring and shaking cocktails. Award badges or achievements for mastering each technique and completing practice sessions.",
            "Images/Lessons/6"
        ),
        new Lesson(
            "Garnishing and Presentation",
            "Tutorial on Garnishes: Offer a tutorial section focusing on different garnishing techniques, such as citrus twists, herb sprigs, and decorative fruit slices. Include step-by-step instructions and video demonstrations.",
            "Practice Mode: Provide users with a virtual garnish station where they can practice their garnishing skills using a variety of ingredients and tools. Offer feedback on their technique and creativity.",
            "Before-and-After Showcase: Allow users to showcase their garnishing skills by uploading photos of their cocktails before and after garnishing. Encourage them to share their creations with the app community.",
            "Images/Lessons/7"
        ),
        new Lesson(
            "Cocktail Etiquette and Responsible Drinking",
            "Etiquette Guidelines: Present a series of guidelines and tips on cocktail etiquette, covering topics such as responsible drinking, serving etiquette, and social behavior in bar settings.",
            "Interactive Scenarios: Create interactive scenarios where users can role-play common social situations involving alcohol, such as refusing a drink or handling a drunk friend. Provide feedback on their responses.",
            "Resource Hub: Include a resource hub with links to additional information on responsible drinking, alcohol-related laws and regulations, and support resources for those struggling with alcohol misuse.",
            "Images/Lessons/1"
        )
    };
            Lessons2 = new List<Lesson>()
                {
                            new Lesson(
                "Advanced Mixing Techniques",
                "Interactive Workshops: Host interactive workshops where users can learn advanced mixing techniques through immersive simulations and challenges. Offer personalized feedback based on their performance.",
                "Recipe Builder: Provide a recipe builder tool that allows users to experiment with advanced mixing techniques and create their own custom cocktails. Offer suggestions for ingredient substitutions and variations.",
                "Community Challenges: Organize community challenges where users can submit their original cocktail recipes incorporating advanced techniques. Encourage peer feedback and voting to determine the winners.",
                "Images/Lessons/1"
            ),
            new Lesson(
                "Flavor Pairing and Balance",
                "Flavor Profile Analyzer: Develop a feature that analyzes users' flavor preferences and suggests cocktail recipes tailored to their taste preferences. Provide recommendations for achieving balanced flavor profiles.",
                "Ingredient Combinations: Showcase curated ingredient combinations and flavor pairings to inspire users' creativity. Include tips and tricks for achieving harmony in cocktails.",
                "Taste Testing Events: Host virtual taste testing events where users can sample different flavor combinations and provide feedback. Collect data on popular flavor profiles and trends among users.",
                "Images/Lessons/4"
            ),
            new Lesson(
                "Cocktail History and Trends",
                "Interactive Timeline: Create an interactive timeline highlighting key milestones in the history of cocktails. Include multimedia content such as videos, photos, and articles to provide context.",
                "Trend Spotting: Curate a section dedicated to current cocktail trends and innovations. Feature interviews with industry experts and highlight emerging techniques and ingredients.",
                "User-Generated Content: Encourage users to contribute their own stories and experiences related to cocktail history and trends. Provide a platform for users to share recipes, photos, and anecdotes with the community.",
                "Images/Lessons/2"
            ),
            new Lesson(
                "Craftsmanship and Presentation",
                "Advanced Garnishing Techniques: Introduce users to more advanced garnishing techniques, such as edible flowers, molecular gastronomy elements, and elaborate garnish sculptures. Provide detailed tutorials and demonstrations.",
                "Virtual Cocktail Competition: Organize a virtual cocktail competition where users can showcase their craftsmanship and presentation skills by submitting photos or videos of their most creatively garnished cocktails. Allow community voting to determine the winners.",
                "Feedback from Experts: Partner with professional bartenders and mixologists to offer personalized feedback and critiques on users' garnishing and presentation techniques. Host live Q&A sessions and workshops with industry experts.",
                "Images/Lessons/3"
            ),
            new Lesson(
                "Menu Development and Cocktail Creation",
                "Menu Planning Tools: Provide users with tools for designing cocktail menus, including templates, layout options, and customizable sections for different drink categories.",
                "Recipe Development Challenges: Challenge users to create their own original cocktail recipes based on specific themes or ingredients. Offer prizes or recognition for the most innovative creations.",
                "Business Insights: Offer insights into the business aspects of running a bar or restaurant, including inventory management, pricing strategies, and customer engagement tactics. Provide case studies and success stories from industry professionals.",
                "Images/Lessons/4"
            )
        };
        }

        public void GetRecipesInMls(List<Drink> drinks)
        {
            foreach (Drink drink in drinks)
            {
                foreach (string point in drink.Points.ToList())
                {
                    drink.Points[drink.Points.IndexOf(point)] = ConvertOuncesToMilliliters(point);
                }
            }
        }
        
        private string ConvertOuncesToMilliliters(string input)
        {
            string pattern = @"(\d+\.?\d*)\s*ounces|\b(\d+\.?\d*)\s*ounce\b";
            double OuncesToMilliliters(double ounces) => ounces * 29.5735;
            string result = Regex.Replace(input, pattern, match =>
            {
                double value;
                if (double.TryParse(match.Groups[1].Value, out value) || double.TryParse(match.Groups[2].Value, out value))
                {
                    double mlValue = OuncesToMilliliters(value);
                    string mlText = ((int)mlValue).ToString();
                    return mlText + (match.Groups[0].Value.Contains("ounces") ? " mls" : " ml");
                }
                return match.Value;
            });

            return result;
        }
        
        private void InitDrinks()
        {
            DrinksInOunce = new List<Drink>
            {
                new Drink("Margarita", new List<string> { "2 ounces tequila", "1 ounce lime juice", "1/2 ounce orange liqueur (optional)", "Salt for rim (optional)" }, "", "Images/Images/Margarita", new List<string>()),
                new Drink("Old Fashioned", new List<string> { "2 ounces bourbon or rye whiskey", "1/2 ounce sugar syrup", "2 dashes Angostura bitters", "Orange peel for garnish (optional)" }, "", "Images/Images/Old Fashioned", new List<string>()),
                new Drink("Daiquiri", new List<string> { "2 ounces white rum", "1 ounce lime juice", "1/2 ounce simple syrup" }, "", "Images/Images/Daiquiri", new List<string>()),
                new Drink("Mojito", new List<string> { "2 ounces white rum", "1 ounce lime juice", "1/2 ounce simple syrup", "6 mint leaves", "Soda water", "Lime wedge for garnish" }, "", "Images/Images/Mojito", new List<string>()),
                new Drink("Negroni", new List<string> { "1 ounce gin", "1 ounce Campari", "1 ounce sweet vermouth", "Orange peel for garnish (optional)" }, "", "Images/Images/Negroni", new List<string>()),
                new Drink("Espresso Martini", new List<string> { "1.5 ounces vodka", "1 ounce espresso", "1/2 ounce coffee liqueur", "3 coffee beans for garnish" }, "", "Images/Images/Espresso Martini", new List<string>()),
                new Drink("Aperol Spritz", new List<string> { "3 ounces Aperol", "2 ounces prosecco", "1 ounce soda water", "Orange slice for garnish" }, "", "Images/Images/Aperol Spritz", new List<string>()),
                new Drink("French 75", new List<string> { "2 ounces gin", "1/2 ounce lemon juice", "1/2 ounce simple syrup", "Champagne", "Lemon peel for garnish" }, "", "Images/Images/French 75", new List<string>()),
                new Drink("Penicillin", new List<string> { "2 ounces blended Scotch whisky", "1/2 ounce lemon juice", "1/2 ounce honey syrup", "1/4 ounce ginger syrup", "Smoked peat garnish (optional)" }, "Images/Images/Penicillin", "", new List<string>()),
                new Drink("Aviation", new List<string> { "2 ounces gin", "1/2 ounce lemon juice", "1/4 ounce maraschino liqueur", "1/4 ounce crème de violette", "Cherry for garnish" }, "", "Images/Images/Aviation", new List<string>()),
                new Drink("Pina Colada", new List<string> { "2 ounces light rum", "1 ounce coconut cream", "1 ounce pineapple juice", "Pineapple wedge and cherry for garnish" }, "", "Images/Images/Pina Colada", new List<string>()),
                new Drink("Chi Chi", new List<string> { "1 ounce vodka", "1 ounce maraschino liqueur", "1 ounce pineapple juice", "Milk", "Cherry for garnish" }, "", "Images/Images/Chi Chi", new List<string>()),
                new Drink("Mai Tai", new List<string> { "2 ounces light rum", "1 ounce orgeat liqueur", "1 ounce almond syrup", "1 ounce lime juice", "Pineapple wedge and cherry for garnish" }, "", "Images/Images/Mai Tai", new List<string>()),
                new Drink("Zombie", new List<string> { "2 ounces light rum", "1 ounce dark rum", "1 ounce rum liqueur", "1 ounce pineapple juice", "1 ounce orange juice", "1 ounce lime juice", "1/2 ounce grapefruit juice", "Pineapple wedge and cherry for garnish" }, "", "Images/Images/Zombie", new List<string>()),
                new Drink("Cosmopolitan", new List<string> { "1 1/2 ounces vodka", "1/2 ounce Cointreau", "1/2 ounce cranberry juice", "1/2 ounce lime juice", "Lemon twist for garnish" }, "", "Images/Images/Cosmopolitan", new List<string>()),
                new Drink("Moscow Mule", new List<string> { "2 ounces vodka", "1/2 ounce lime juice", "4 ounces ginger beer", "Lime wedge for garnish" }, "", "Images/Images/Moscow Mule", new List<string>()),
                new Drink("Greyhound", new List<string> { "2 ounces vodka", "4 ounces grapefruit juice", "Lemon twist for garnish" }, "", "Images/Images/Greyhound", new List<string>()),
                new Drink("Vodka Martini", new List<string> { "2 ounces vodka", "1/2 ounce dry vermouth", "Olive for garnish" }, "", "Images/Images/Vodka Martini", new List<string>()),
                new Drink("Irish Coffee", new List<string> { "2 ounces Irish whiskey", "1/2 ounce sugar syrup", "Hot coffee", "Whipped cream for garnish" }, "", "Images/Images/Irish Coffee", new List<string>()),
                new Drink("French Connection", new List<string> { "1/2 ounce cognac", "1/2 ounce Amaretto", "Orange twist for garnish" }, "", "Images/Images/French Connection", new List<string>()),
                new Drink("B-52", new List<string> { "1/3 ounce Kahlúa", "1/3 ounce Baileys Irish Cream", "1/3 ounce Grand Marnier", "Coffee bean for garnish" }, "", "Images/Images/B-52", new List<string>()),
            };

            DrinksInMl = new List<Drink>
            {
                new Drink("Margarita", new List<string> { "2 ounces tequila", "1 ounce lime juice", "1/2 ounce orange liqueur (optional)", "Salt for rim (optional)" }, "", "Images/Images/Margarita", new List<string>()),
                new Drink("Old Fashioned", new List<string> { "2 ounces bourbon or rye whiskey", "1/2 ounce sugar syrup", "2 dashes Angostura bitters", "Orange peel for garnish (optional)" }, "", "Images/Images/Old Fashioned", new List<string>()),
                new Drink("Daiquiri", new List<string> { "2 ounces white rum", "1 ounce lime juice", "1/2 ounce simple syrup" }, "", "Images/Images/Daiquiri", new List<string>()),
                new Drink("Mojito", new List<string> { "2 ounces white rum", "1 ounce lime juice", "1/2 ounce simple syrup", "6 mint leaves", "Soda water", "Lime wedge for garnish" }, "", "Images/Images/Mojito", new List<string>()),
                new Drink("Negroni", new List<string> { "1 ounce gin", "1 ounce Campari", "1 ounce sweet vermouth", "Orange peel for garnish (optional)" }, "", "Images/Images/Negroni", new List<string>()),
                new Drink("Espresso Martini", new List<string> { "1.5 ounces vodka", "1 ounce espresso", "1/2 ounce coffee liqueur", "3 coffee beans for garnish" }, "", "Images/Images/Espresso Martini", new List<string>()),
                new Drink("Aperol Spritz", new List<string> { "3 ounces Aperol", "2 ounces prosecco", "1 ounce soda water", "Orange slice for garnish" }, "", "Images/Images/Aperol Spritz", new List<string>()),
                new Drink("French 75", new List<string> { "2 ounces gin", "1/2 ounce lemon juice", "1/2 ounce simple syrup", "Champagne", "Lemon peel for garnish" }, "", "Images/Images/French 75", new List<string>()),
                new Drink("Penicillin", new List<string> { "2 ounces blended Scotch whisky", "1/2 ounce lemon juice", "1/2 ounce honey syrup", "1/4 ounce ginger syrup", "Smoked peat garnish (optional)" }, "Images/Images/Penicillin", "", new List<string>()),
                new Drink("Aviation", new List<string> { "2 ounces gin", "1/2 ounce lemon juice", "1/4 ounce maraschino liqueur", "1/4 ounce crème de violette", "Cherry for garnish" }, "", "Images/Images/Aviation", new List<string>()),
                new Drink("Pina Colada", new List<string> { "2 ounces light rum", "1 ounce coconut cream", "1 ounce pineapple juice", "Pineapple wedge and cherry for garnish" }, "", "Images/Images/Pina Colada", new List<string>()),
                new Drink("Chi Chi", new List<string> { "1 ounce vodka", "1 ounce maraschino liqueur", "1 ounce pineapple juice", "Milk", "Cherry for garnish" }, "", "Images/Images/Chi Chi", new List<string>()),
                new Drink("Mai Tai", new List<string> { "2 ounces light rum", "1 ounce orgeat liqueur", "1 ounce almond syrup", "1 ounce lime juice", "Pineapple wedge and cherry for garnish" }, "", "Images/Images/Mai Tai", new List<string>()),
                new Drink("Zombie", new List<string> { "2 ounces light rum", "1 ounce dark rum", "1 ounce rum liqueur", "1 ounce pineapple juice", "1 ounce orange juice", "1 ounce lime juice", "1/2 ounce grapefruit juice", "Pineapple wedge and cherry for garnish" }, "", "Images/Images/Zombie", new List<string>()),
                new Drink("Cosmopolitan", new List<string> { "1 1/2 ounces vodka", "1/2 ounce Cointreau", "1/2 ounce cranberry juice", "1/2 ounce lime juice", "Lemon twist for garnish" }, "", "Images/Images/Cosmopolitan", new List<string>()),
                new Drink("Moscow Mule", new List<string> { "2 ounces vodka", "1/2 ounce lime juice", "4 ounces ginger beer", "Lime wedge for garnish" }, "", "Images/Images/Moscow Mule", new List<string>()),
                new Drink("Greyhound", new List<string> { "2 ounces vodka", "4 ounces grapefruit juice", "Lemon twist for garnish" }, "", "Images/Images/Greyhound", new List<string>()),
                new Drink("Vodka Martini", new List<string> { "2 ounces vodka", "1/2 ounce dry vermouth", "Olive for garnish" }, "", "Images/Images/Vodka Martini", new List<string>()),
                new Drink("Irish Coffee", new List<string> { "2 ounces Irish whiskey", "1/2 ounce sugar syrup", "Hot coffee", "Whipped cream for garnish" }, "", "Images/Images/Irish Coffee", new List<string>()),
                new Drink("French Connection", new List<string> { "1/2 ounce cognac", "1/2 ounce Amaretto", "Orange twist for garnish" }, "", "Images/Images/French Connection", new List<string>()),
                new Drink("B-52", new List<string> { "1/3 ounce Kahlúa", "1/3 ounce Baileys Irish Cream", "1/3 ounce Grand Marnier", "Coffee bean for garnish" }, "", "Images/Images/B-52", new List<string>()),
            };
            
            GetRecipesInMls(DrinksInMl);

            IsInMl = ConvertToBool(PlayerPrefs.GetInt("IsInMl", 0));
            if (IsInMl)
            {
                Debug.Log('d');
                Drinks = DrinksInMl;
            }

            else
            {
                Drinks = DrinksInOunce;
            }
        }

        private bool ConvertToBool(int value)
        {
            return value != 0;
        }

        private void Start()
        {
            InitLevels();
            InitDrinks();
            InitViews();
            InitPages();

            _homeView.Q("SwipeArea").RegisterCallback<PointerDownEvent>(OnPointerDown);
            _homeView.Q("SwipeArea").RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _homeView.Q("SwipeArea").RegisterCallback<PointerUpEvent>(OnPointerUp);

            OnSwipeRight += _homePage.SetRandomDrink;
            OnSwipeLeft += _homePage.SetRandomDrink;
            _recipePage.OnReturn += () => _recipePage.ChangePageTo(_recipesPage);
            _settingsPage.OnReturn += () => _settingsPage.ChangePageTo(_homePage);
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
            _lessonView = Root.Q("Lesson");
            _recipeView = Root.Q("Recipe");
            _recipesView = Root.Q("Recipes");
            _shoppingListView = Root.Q("ShoppingList");
            _toolsOtherView = Root.Q("ToolsOther");
            _websitesView = Root.Q("Websites");
            _youtubeView = Root.Q("Youtube");
            _toolsView = Root.Q("ToolsPage");
            _websitesView = Root.Q("WebsitesPage");
            _youtubeView = Root.Q("YoutubePage");
            _aboutUsView = Root.Q("AboutUsPage");
            _settingsView = Root.Q("SettingsPage");
        }

        private void InitPages()
        {
            _header = new Header(_headerView, _header, "Home");
            _lessonPage = new LessonPage(_lessonView, _header, "Lessons");
            _lessonsPage = new LessonsPage(_lessonsView, _header, "Lessons", _lessonPage, Lessons1, Lessons2);
            _shoppingListPage = new ShoppingListPage(_shoppingListView, _header, "Shopping List", _shoppingListTemplate);
            _recipePage = new RecipePage(_recipeView, _header, "Recipe", _shoppingListPage, this);
            _recipesPage = new RecipesPage(_recipesView, _header, "Recipes", _recipePage);
            _collectionsPage = new CollectionsPage(_collectionsView, _header, "Collections", _collectionTemplate, _recipePage);
            _homePage = new HomePage(_homeView, _header, "Home", _recipePage, _collectionsPage);
            _toolsPage = new ToolsPage(_toolsView, _header, "Tools & Other");
            _websitesPage = new WebsitesPage(_websitesView, _header, "Tools & Other");
            _youtubePage = new YoutubePage(_youtubeView, _header, "Tools & Other");
            _aboutUsPage = new AboutUsPage(_aboutUsView, _header, "Tools & Other");
            _settingsPage = new SettingsPage(_settingsView, _header, "Settings");

            _toolsPage.OnReturn += () => _toolsPage.ChangePageTo(_toolsOtherPage);
            _websitesPage.OnReturn += () => _websitesPage.ChangePageTo(_toolsOtherPage);
            _youtubePage.OnReturn += () => _youtubePage.ChangePageTo(_toolsOtherPage);
            _aboutUsPage.OnReturn += () => _aboutUsPage.ChangePageTo(_toolsOtherPage);
            _collectionsPage.OnShopping += () => _collectionsPage.ChangePageTo(_shoppingListPage);
            _shoppingListPage.OnReturn += () => _shoppingListPage.ChangePageTo(_homePage);

            _toolsOtherPage = new ToolsOtherPage(_toolsOtherView, _header, "Tools & Other", _toolsPage, _websitesPage, _youtubePage, _aboutUsPage);
            _sideMenuPage = new SideMenuPage(_sideMenuView, _header, "", _homePage, _shoppingListPage, _aboutUsPage, _toolsOtherPage);
            _menu = new Menu(_footerView, _header, "", _homePage, _lessonsPage, _lessonPage, _recipePage, _recipesPage, _collectionsPage, _toolsOtherPage);
            _header.OnCart += () => _menu.ChangePageTo(null, _shoppingListPage, _toolsOtherPage, _collectionsPage, _recipePage, _recipesPage, _settingsPage);
            _header.OnSettings += () => _menu.ChangePageTo(null, _settingsPage, _toolsOtherPage, _collectionsPage,
                _recipePage, _recipesPage, _shoppingListPage, _websitesPage, _youtubePage, _aboutUsPage);

            _settingsPage.OnUnitChanged += ChangeDrinks;
        }

        private void ChangeDrinks()
        {
            IsInMl = !ConvertToBool(PlayerPrefs.GetInt("IsInMl", 0));

            Drinks = IsInMl ? DrinksInMl : DrinksInOunce;
            _homePage.SetDrink();
            _recipePage.ChangePageContent();
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