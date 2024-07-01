using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<Drink> _drinks = new();

    private VisualElement _swiper;
    private List<VisualElement> _swipers;
    private int _currentSwiperIndex;

    private List<VisualElement> _selectedDrinks; 
    
    public HomePage(VisualElement root, Header header, string name) : base(root, header, name)
    {
        SetupPage();
    }

    private void InitDrinks()
    {
        Drink pinaColada = new Drink("Pina Colada", new List<string>(){"Label1", "Label2", "Label3", "level4"}, "Images/Drinks/PinaColada", new List<string>(){"Labl1", "Lbel1", "Lel1", "leel1"});
        Drink daiquiri = new Drink("Daiquiri", new List<string>(){"Label11", "Label22", "Label33", "level44"}, "Images/Drinks/Daiquiri", new List<string>(){"Labl2", "Lbel2", "Lel2", "leel2"});
        Drink oldFashioned = new Drink("Old Fashioned", new List<string>(){"Label11", "Label22", "Label33", "level44"}, "Images/Drinks/Daiquiri", new List<string>(){"Labl3", "Lbel3", "Lel3", "leel3"});
        
        _drinks.Add(pinaColada);
        _drinks.Add(daiquiri);
        _drinks.Add(oldFashioned);
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

        InitDrinks();
        InitSelected();
    }

    public void SetRandomDrink()
    {
        Drink drink = _drinks[Random.Range(0, _drinks.Count)];

        string name = drink.Name;
        string imageSource = drink.ImageSource;
        List<string> points = drink.Points;

        _drinkLabel.text = name.ToUpper();
        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            point.text = drink.Points[pointsList.IndexOf(point)];
        }

        Texture2D image = Resources.Load<Texture2D>(drink.ImageSource);
        Root.style.backgroundImage = new StyleBackground(image);
        ChangeSwiper();
    }

    private void InitSelected()
    {
        _selectedDrinks = Root.Query<VisualElement>(className: "selected_drink").ToList();

        foreach (VisualElement selected in _selectedDrinks)
        {
            Debug.Log(selected.name);
            selected.RegisterCallback<ClickEvent>(evt =>
            {
                SelectDrink(selected);
            });
        }
    }
    
    private void SelectDrink(VisualElement element)
    {
        string selectedDrinkName = element.name;
        Drink drink = null;
        foreach (Drink drink1 in _drinks)
        {
            if (drink1.Name.Replace(" ", "") == selectedDrinkName)
            {
                drink = drink1;
            }
        }

        string name = drink.Name;
        string imageSource = drink.ImageSource;
        List<string> points = drink.Points;

        _drinkLabel.text = name.ToUpper();
        List<Label> pointsList = _drinkPoints;

        foreach (Label point in pointsList)
        {
            point.text = drink.Points[pointsList.IndexOf(point)];
        }

        Texture2D image = Resources.Load<Texture2D>(drink.ImageSource);
        Root.style.backgroundImage = new StyleBackground(image);
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
    public int Rating = 0;
    public string Name;
    public List<string> Points;
    public string ImageSource;
    public List<string> Instructions;

    public Drink(string name, List<string> points, string imageSource, List<string> instructions)
    {
        Name = name;
        Points = points;
        ImageSource = imageSource;
        Instructions = instructions;
    }
}
