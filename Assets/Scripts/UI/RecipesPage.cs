using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RecipesPage : BasePage
{
    private List<VisualElement> _drinksElements;
    private RecipePage _recipePage;
    private List<Drink> _drinks = new();
    
    protected override void SetupPage()
    {
        _drinksElements = Root.Query(className: "selected_drink").ToList();

        foreach (VisualElement drinkElement in _drinksElements)
        {
            Drink drink = _drinks.Find(drink1 => drink1.Name.Replace(" ", "").ToLower().Equals(drinkElement.name.ToLower()));
            drinkElement.RegisterCallback<ClickEvent>(evt => ShowRecipePage(drink));
        }
    }

    private void ShowRecipePage(Drink drink)
    {
        Debug.Log(drink == null);
        _recipePage.ChangePageContent(drink);
        ChangePageTo(_recipePage);
    }

    public RecipesPage(VisualElement root, Header header, string name, RecipePage recipePage, List<Drink> drinks) : base(root, header, name)
    {
        _drinks = drinks;
        _recipePage = recipePage;
        SetupPage();
    }
}
