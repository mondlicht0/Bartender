using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage : BasePage
{
    private List<string> _regionList = new();
    private List<string> _themeList = new();
    private List<string> _unitList = new();

    private Label _regionLabel;
    private Button _nextRegion;
    private Button _backRegion;
    private int _currentRegionIndex;

    private Label _themeLabel;
    private Button _nextTheme;
    private Button _backTheme;
    private int _currentThemeIndex;
    
    private Label _unitLabel;
    private Button _nextUnit;
    private Button _backUnit;
    private int _currentUnitIndex;
    
    private Button _backArrow;
    public event Action OnReturn;
    public event Action OnUnitChanged;
    
    public SettingsPage(VisualElement root, Header header, string name) : base(root, header, name)
    {
        SetupPage();
    }

    protected override void SetupPage()
    {
        _nextRegion = Root.Q<Button>("NextRegion");
        _backRegion = Root.Q<Button>("BackRegion");
        _nextTheme = Root.Q<Button>("NextTheme");
        _backTheme = Root.Q<Button>("BackTheme");
        _regionLabel = Root.Q<Label>("RegionLabel");
        _themeLabel = Root.Q<Label>("ThemeLabel");
        _unitLabel = Root.Q<Label>("UnitLabel");
        _backUnit = Root.Q<Button>("BackUnit");
        _nextUnit = Root.Q<Button>("NextUnit");
        
        _regionList.Add("Russia");
        _regionList.Add("UK");
        _regionList.Add("Brazil");
        _regionList.Add("Spain");
        _regionList.Add("Germany");
        
        _themeList.Add("Dark");
        _themeList.Add("White");
        
        _unitList.Add("Millilitres (ml)");
        _unitList.Add("Ounces (oz)");
        
        _backArrow = Root.Q<Button>("BackArrow");
        _backArrow.clicked += () => OnReturn?.Invoke();

        _nextRegion.clicked += NextRegion;
        _backRegion.clicked += BackRegion;

        _nextUnit.clicked += NextUnit;
        _backUnit.clicked += BackUnit;
        
        string region = PlayerPrefs.GetString("Region");

        if (region == string.Empty)
        {
            region = "Russia";
        }

        _regionLabel.text = region;
        _currentRegionIndex = _regionList.IndexOf(region);

        CheckButtonsRegion();
        
        if (PlayerPrefs.GetInt("IsInMl") == 0)
        {
            NextUnit();
        }
    }

    private void CheckButtonsRegion()
    {
        _nextRegion.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));
        _backRegion.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));

        if (_currentRegionIndex == 0)
        {
            _backRegion.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }

        if (_currentRegionIndex == _regionList.Count - 1)
        {
            _nextRegion.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }
    }
    
    private void CheckButtonsUnit()
    {
        _nextUnit.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));
        _backUnit.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));

        if (_currentRegionIndex == 0)
        {
            _backUnit.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }

        if (_currentRegionIndex == _regionList.Count - 1)
        {
            _nextUnit.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }
    }

    private void NextRegion()
    {
        _currentRegionIndex++;
        if (_currentRegionIndex >= _regionList.Count)
        {
            _currentRegionIndex = 0;
        }
        
        _regionLabel.text = _regionList[_currentRegionIndex];

        CheckButtonsRegion();
        
        PlayerPrefs.SetString("Region", _regionList[_currentRegionIndex]);
    }

    private void BackRegion()
    {
        _currentRegionIndex--;
        if (_currentRegionIndex < 0)
        {
            _currentRegionIndex = _regionList.Count - 1;
        }

        _regionLabel.text = _regionList[_currentRegionIndex];

        CheckButtonsRegion();
        
        PlayerPrefs.SetString("Region", _regionList[_currentRegionIndex]);
    }
    
    private void NextUnit()
    {
        _currentUnitIndex++;
        if (_currentUnitIndex >= _unitList.Count)
        {
            _currentUnitIndex = 0;
        }
        
        _unitLabel.text = _unitList[_currentUnitIndex];

        CheckButtonsUnit();
        
        PlayerPrefs.SetInt("IsInMl", _currentUnitIndex);
        OnUnitChanged?.Invoke();
    }

    private void BackUnit()
    {
        _currentUnitIndex--;
        if (_currentUnitIndex < 0)
        {
            _currentUnitIndex = _unitList.Count - 1;
        }

        _unitLabel.text = _unitList[_currentUnitIndex];

        CheckButtonsUnit();
        
        PlayerPrefs.SetInt("IsInMl", _currentUnitIndex);
        OnUnitChanged?.Invoke();
    }
}
