using System;
using System.Collections.Generic;
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

    public event Action OnReturn;
    
    public RecipePage(VisualElement root, Header header, string name) : base(root, header, name)
    {
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
            star.RegisterCallback<ClickEvent>(evt => Stars(_stars.IndexOf(star), star));
        }
    }
    
    private void Stars(int i, VisualElement star)
    {
        foreach (VisualElement star1 in _stars)
        {
            VisualElement active = star1.Q("ActiveStar");
            active.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
        
        bool isEmptyStar = star.Q("ActiveStar").style.display.Equals(new StyleEnum<DisplayStyle>(DisplayStyle.None));
        for (int j = 0; j < 5; j++)
        {
            Debug.Log(i);
            VisualElement activeStar = _stars[j].Q("ActiveStar");
            VisualElement inactiveStar = _stars[j].Q("InactiveStar");

            StyleEnum<DisplayStyle> active = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            StyleEnum<DisplayStyle> inactive = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

            if (isEmptyStar)
            {
                Debug.Log("asf");
                activeStar.style.display = active;
                activeStar.style.display = inactive;
            }

            else
            {
                Debug.Log("asfaa");
                activeStar.style.display = inactive;
                inactiveStar.style.display = active;
                if (j == i)
                {
                    break;
                }
            }
        }

        _currentDrink.Rating = i;
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

        _currentDrink.Rating = i;
    }

    private void AddToShoppingList()
    {
        
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
        Stars(drink.Rating);
    }
}
