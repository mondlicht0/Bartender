using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BasePage
{
    public string Name;
    protected Header Header;
    public VisualElement Root;
    public event Action OnToggle;
        
    protected BasePage(VisualElement root, Header header, string name)
    {
        Name = name;
        Header = header;
        Root = root;
    }

    protected abstract void SetupPage();

    public void InvokeToggle()
    {
        OnToggle?.Invoke();
    }
    
    public void ChangePageTo(BasePage targetPage, params BasePage[] otherPages)
    {
        Debug.Log("Change" + targetPage.Name);
        if (this is not HomePage)
        {
            Root.AddToClassList("left-transfom");
        }
        
        targetPage.Root.RemoveFromClassList("left-transfom");
        targetPage.OnToggle?.Invoke();

        foreach (var page in otherPages)
        {
            page.Root.AddToClassList("left-transfom");
        }
        
        Header.ChangeHeaderTitle(Name);
    }
}
