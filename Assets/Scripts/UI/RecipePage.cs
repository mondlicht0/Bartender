using System;
using System.Collections.Generic;
using UI.Main;
using UnityEngine;
using UnityEngine.UIElements;

public class RecipePage : BasePage
{
    private VisualElement _image;
    private List<Label> _drinkPoints = new();
    private List<Label> _drinkInstructions = new();
    private VisualElement _addShopping;
    private VisualElement _shoppingImage;
    private Label _shoppingLabel;
    private VisualElement _backArrow;
    private List<VisualElement> _stars;
    private Drink _currentDrink;
    private ShoppingListPage _shoppingListPage;
    private Main _mainPage;

    public event Action OnReturn;
    
    public RecipePage(VisualElement root, Header header, string name, ShoppingListPage shoppingListPage, Main mainPage) : base(root, header, name)
    {
        _mainPage = mainPage;
        _shoppingListPage = shoppingListPage;
        SetupPage();
    }
    
    protected override void SetupPage()
    {
        _drinkPoints = Root.Q("IngredientsList").Query<Label>().ToList();
        _image = Root.Q("RecipeImage");
        _addShopping = Root.Q("AddShopping");
        _shoppingImage = Root.Q("ShoppingImage");
        _backArrow = Root.Q<Button>();
        _shoppingLabel = Root.Q<Label>();
        _stars = Root.Query<VisualElement>("Stars").ToList();
        
        _shoppingImage.RegisterCallback<ClickEvent>(evt => AddToShoppingList());
        _shoppingLabel.RegisterCallback<ClickEvent>(evt => AddToShoppingList());
        _backArrow.RegisterCallback<ClickEvent>(evt => OnReturn?.Invoke());

        InitStars();
    }

    private void InitStars()
    {
        Debug.Log(_stars.Count);
        foreach (VisualElement star in _stars)
        {
            star.Q("InactiveStar").RegisterCallback<ClickEvent>(evt => Stars(_stars.IndexOf(star), star.Q("InactiveStar")));
            star.Q("ActiveStar").RegisterCallback<ClickEvent>(evt => Stars(_stars.IndexOf(star), star.Q("ActiveStar")));
        }
    }
    
    private void Stars(int i, VisualElement star)
    {
        int rating = 0;
        for (int j = 0; j < 5; j++)
        {
            _stars[i].Q("ActiveStar").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            //inactiveStar.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }
        
        for (int j = 0; j < 5; j++)
        {
            if (star.name == "InactiveStar")
            {
                Debug.Log("InActive0");
                rating = j;
                if (j == i + 1)
                {
                    break;
                }
                Debug.Log("InActive");
                _stars[j].Q("ActiveStar").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                _stars[j].Q("InactiveStar").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }

            else
            {
                rating = 0;
                _stars[j].Q("ActiveStar").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                _stars[j].Q("InactiveStar").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }
        }
        
        PlayerPrefs.SetInt(_currentDrink.Name, rating);
        _currentDrink.Rating = rating - 1;
    }

    private void Stars(int i)
    {
        for (int j = 0; j <= i; j++)
        {
            Debug.Log(i);
            VisualElement activeStar = _stars[j].Q("ActiveStar");
            VisualElement inactiveStar = _stars[j].Q("InactiveStar");

            StyleEnum<DisplayStyle> active = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            StyleEnum<DisplayStyle> inactive = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

            if (activeStar.style.display == active)
            {
                activeStar.style.display = inactive;
                inactiveStar.style.display = active;
            }

            else
            {
                activeStar.style.display = active;
                inactiveStar.style.display = inactive;
            }
        }

        PlayerPrefs.SetInt(_currentDrink.Name, i);
        _currentDrink.Rating = i;
    }

    private void AddToShoppingList()
    {
        _shoppingListPage.AddToShoppingList(_currentDrink);
    }
    
    public void ChangePageContent()
    {
        Drink drink = _currentDrink;
        Debug.Log(drink == null);
        List<string> points = drink.Points;

        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            if (pointsList.IndexOf(point) > points.Count - 1)
            {
                continue;
            }
            
            point.text = points[pointsList.IndexOf(point)];
        }

        Texture2D image = Resources.Load<Texture2D>(drink.RecipeImageSource);
        _image.style.backgroundImage = new StyleBackground(image);
        _currentDrink = drink;
        foreach (VisualElement star in _stars)
        {
            VisualElement active = star.Q("ActiveStar");
            VisualElement inactive = star.Q("InactiveStar");
            active.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            inactive.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }
        
        Stars(PlayerPrefs.GetInt(drink.Name, 0));
    }
    
    public void ChangePageContent(Drink drink)
    {
        Debug.Log(drink == null);
        List<string> points = drink.Points;

        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            if (pointsList.IndexOf(point) > points.Count - 1)
            {
                continue;
            }
            
            point.text = points[pointsList.IndexOf(point)];
        }

        Texture2D image = Resources.Load<Texture2D>(drink.RecipeImageSource);
        _image.style.backgroundImage = new StyleBackground(image);
        _currentDrink = drink;
        foreach (VisualElement star in _stars)
        {
            VisualElement active = star.Q("ActiveStar");
            VisualElement inactive = star.Q("InactiveStar");
            active.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            inactive.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }
        
        Stars(PlayerPrefs.GetInt(drink.Name, 0));
    }
}
