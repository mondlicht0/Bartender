using System;
using System.Collections.Generic;
using System.Linq;
using UI.Main;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class HomePage : BasePage
{
    private List<VisualElement> _selectedDrink = new();
    private Button _seeRecipeButton;
    private Label _drinkLabel;
    private GroupBox _drinkRecipe;
    private List<Label> _drinkPoints;
    private Drink _currentDrink;
    private int _currentDrinkIndex;

    private RecipePage _recipePage;
    private VisualElement _swiper;
    private List<VisualElement> _swipers;
    private int _currentSwiperIndex;

    private List<VisualElement> _selectedDrinks;
    private List<VisualElement> _favorites = new();
    private CollectionsPage _collectionsPage;
    
    public HomePage(VisualElement root, Header header, string name, RecipePage recipePage, CollectionsPage collectionsPage) : base(root, header, name)
    {
        _collectionsPage = collectionsPage;
        _recipePage = recipePage;
        SetupPage();
    }

    private void InitDrinks()
    {
        _currentDrink = Main.Instance.Drinks[0];
        _currentDrinkIndex = 0;
        _recipePage.ChangePageContent(_currentDrink);

        _seeRecipeButton.clicked += SeeRecipe;
    }

    private void SeeRecipe()
    {
        _recipePage.ChangePageContent(_currentDrink); 
        ChangePageTo(_recipePage);
    }
    
    protected override void SetupPage()
    {
        _swiper = Root.Q("Swiper");
        _swipers = _swiper.Children().ToList();
        _drinkRecipe = Root.Q<GroupBox>("DrinkRecipe");
        _drinkPoints = _drinkRecipe.Query<Label>().ToList();
        _seeRecipeButton = Root.Q<Button>("SeeRecipeButton");
        _drinkLabel = Root.Q<Label>("DrinkName");
        _swipers[_currentSwiperIndex].AddToClassList("swiper_active");

        InitSelected();
        InitDrinks();
        InitFavorites();
        SetDrink();
    }

    public void SetRandomDrink()
    {
        Drink drink = Main.Instance.Drinks[Random.Range(0, Main.Instance.Drinks.Count)];
        _currentDrinkIndex = Random.Range(0, Main.Instance.Drinks.Count);

        string name = drink.Name;
        string imageSource = drink.ImageSource;
        List<string> points = drink.Points;

        _drinkLabel.text = name.ToUpper();
        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            try
            {
                point.text = drink.Points[pointsList.IndexOf(point)];
            }
            catch (Exception e)
            {
                break;
            }
        }

        Texture2D image = Resources.Load<Texture2D>(drink.ImageSource);
        Root.style.backgroundImage = new StyleBackground(image);
        ChangeSwiper();
        _currentDrink = drink;
    }
    
    public void SetDrink()
    {
        Drink drink = Main.Instance.Drinks[_currentDrinkIndex];
        string name = drink.Name;
        string imageSource = drink.ImageSource;
        List<string> points = drink.Points;

        _drinkLabel.text = name.ToUpper();
        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            try
            {
                point.text = drink.Points[pointsList.IndexOf(point)];
            }
            catch (Exception e)
            {
                break;
            }
        }

        Texture2D image = Resources.Load<Texture2D>(drink.ImageSource);
        Root.style.backgroundImage = new StyleBackground(image);
        _currentDrink = drink;
    }

    private void InitSelected()
    {
        _selectedDrinks = Root.Query<VisualElement>(className: "selected_drink").ToList();

        foreach (VisualElement selected in _selectedDrinks)
        {
            selected.RegisterCallback<ClickEvent>(evt =>
            {
                SelectDrink(selected);
            });
        }
    }

    private void InitFavorites()
    {
        _favorites = Root.Query<VisualElement>("Loves").ToList();
        foreach (VisualElement favorite in _favorites)
        {
            favorite.RegisterCallback<ClickEvent>(_ =>
            {
                AddDrinkToFavorite(favorite);
            });
        }
    }

    private void AddDrinkToFavorite(VisualElement favoriteButton)
    {
        string selectedDrinkName = favoriteButton.parent.parent.name;
        Drink drink = null;
        foreach (Drink drink1 in Main.Instance.Drinks)
        {
            if (drink1.Name.Replace(" ", "") == selectedDrinkName)
            {
                drink = drink1;
                drink1.IsInFavorite = !drink1.IsInFavorite;
            }
        }

        if (drink == null)
        {
            return;
        }

        favoriteButton.style.unityBackgroundImageTintColor = !drink.IsInFavorite ? new StyleColor(Color.white) : new StyleColor(Color.green);
        if (drink.IsInFavorite)
        {
            drink.IsInFavorite = true;
            _collectionsPage.AddNewDrink(drink);
        }

        else
        {
            drink.IsInFavorite = false;
            _collectionsPage.RemoveDrink(drink);
        }
    }
    
    private void SelectDrink(VisualElement element)
    {
        string selectedDrinkName = element.name;
        Drink drink = null;
        foreach (Drink drink1 in Main.Instance.Drinks)
        {
            if (drink1.Name.Replace(" ", "") == selectedDrinkName)
            {
                drink = drink1;
                _currentDrinkIndex = Main.Instance.Drinks.IndexOf(drink1);
            }
        }

        string name = drink.Name;
        string imageSource = drink.ImageSource;
        List<string> points = drink.Points;

        _drinkLabel.text = name.ToUpper();
        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            try
            {
                point.text = drink.Points[pointsList.IndexOf(point)];
            }
            catch (Exception e)
            {
                break;
            }
        }

        Texture2D image = Resources.Load<Texture2D>(drink.ImageSource);
        Root.style.backgroundImage = new StyleBackground(image);
        _currentDrink = drink;
    }

    private void ChangeSwiper()
    {
        _swipers[_currentSwiperIndex].RemoveFromClassList("swiper_active");
        _currentSwiperIndex++;
        if (_currentSwiperIndex > 3)
        {
            _currentSwiperIndex = 0;
        }
        _swiper[_currentSwiperIndex].AddToClassList("swiper_active");
    }
}

public class Drink
{
    public bool IsInFavorite;
    public bool IsInList;
    public int Rating = 0;
    public string Name;
    public List<string> Points;
    public string ImageSource;
    public string RecipeImageSource;
    public List<string> Instructions;

    public Drink(string name, List<string> points, string imageSource, string recipeImageSource, List<string> instructions)
    {
        Name = name;
        Points = points;
        ImageSource = recipeImageSource;
        RecipeImageSource = recipeImageSource;
        Instructions = instructions;
    }
}
