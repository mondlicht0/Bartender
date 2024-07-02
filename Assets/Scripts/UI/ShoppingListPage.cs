using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShoppingListPage : BasePage
{
    private Button _backArrow;
    private VisualTreeAsset _template;
    private ScrollView _list;
    public event Action OnReturn;
    
    public ShoppingListPage(VisualElement root, Header header, string name, VisualTreeAsset template) : base(root, header, name)
    {
        _template = template;
        SetupPage();
    }
    
    protected override void SetupPage()
    {
        _backArrow = Root.Q<Button>();
        _list = Root.Q<ScrollView>();
        _backArrow.clicked += () => OnReturn?.Invoke();
    }

    public void AddToShoppingList(Drink drink)
    {
        VisualElement newDrink = _template.CloneTree();
        List<Label> ingredients = newDrink.Q("Ingredients").Query<Label>().ToList();
        Label title = newDrink.Q<Label>("Title");

        foreach (Label ingredient in ingredients)
        {
            if (ingredients.IndexOf(ingredient) > drink.Points.Count - 1)
            {
                continue;
            }
            
            ingredient.text = drink.Points[ingredients.IndexOf(ingredient)];
        }

        title.text = drink.Name.ToUpper();
        _list.Add(newDrink);
    }
}
