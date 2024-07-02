using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectionsPage : BasePage
{
    private VisualTreeAsset  _collectionDrinkTemplate;
    private VisualElement _list;
    private VisualElement _shoppingButton;
    private RecipePage _recipePage;
    public event Action OnShopping;
    
    protected override void SetupPage()
    {
        Drink oldFashioned = new Drink("Old Fashioned", new List<string>(){"Label11", "Label22", "Label33", "level44"},
            "Images/Drinks/Daiquiri", "Images/Recipes/Daiquiri", new List<string>(){"Labl3", "Lbel3", "Lel3", "leel3"});
        _list = Root.Q("List");
        _shoppingButton = Root.Q("ShoppingButton");
        _shoppingButton.RegisterCallback<ClickEvent>(evt => OnShopping?.Invoke());
    }

    public void AddNewDrink(Drink drink)
    {
        VisualElement newDrink = _collectionDrinkTemplate.CloneTree();
        VisualElement collectionDrink = newDrink.Q("CollectionDrink");
        Label title = newDrink.Q<Label>("Title");
        Debug.Log(drink.ImageSource);
        collectionDrink.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>(drink.ImageSource));
        title.text = drink.Name.ToUpper();
        _list.Add(newDrink);
        newDrink.RegisterCallback<ClickEvent>(evt => GoToRecipePage(drink));
    }

    private void GoToRecipePage(Drink drink)
    {
        ChangePageTo(_recipePage);
        _recipePage.ChangePageContent(drink);
    }

    public CollectionsPage(VisualElement root, Header header, string name, VisualTreeAsset template, RecipePage recipePage) : base(root, header, name)
    {
        _recipePage = recipePage;
        _collectionDrinkTemplate = template;
        SetupPage();
    }
}
