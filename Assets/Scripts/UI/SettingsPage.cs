using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage : BasePage
{
    private List<string> _regionList = new();
    private List<string> _themeList = new();

    private Label _regionLabel;
    private Button _nextRegion;
    private Button _backRegion;
    private int _currentRegionIndex;

    private Label _themeLabel;
    private Button _nextTheme;
    private Button _backTheme;
    private int _currentThemeIndex;
    
    private Button _backArrow;
    public event Action OnReturn;
    
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
        
        _regionList.Add("Russia");
        _regionList.Add("UK");
        _regionList.Add("Brazil");
        _regionList.Add("Spain");
        _regionList.Add("Germany");
        
        _themeList.Add("Dark");
        _themeList.Add("White");
        
        _backArrow = Root.Q<Button>("BackArrow");
        _backArrow.clicked += () => OnReturn?.Invoke();

        _nextRegion.clicked += NextRegion;
        _backRegion.clicked += BackRegion;
        
        string region = PlayerPrefs.GetString("Region");

        if (region == string.Empty)
        {
            region = "Russia";
        }

        _regionLabel.text = region;
        _currentRegionIndex = _regionList.IndexOf(region);

        CheckButtons();
    }

    private void CheckButtons()
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

    private void NextRegion()
    {
        _currentRegionIndex++;
        if (_currentRegionIndex >= _regionList.Count)
        {
            _currentRegionIndex = 0;
        }
        
        _regionLabel.text = _regionList[_currentRegionIndex];

        CheckButtons();
        
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

        CheckButtons();
        
        PlayerPrefs.SetString("Region", _regionList[_currentRegionIndex]);
    }

    private void NextTheme()
    {
        if (_currentThemeIndex >= _themeList.Count)
        {
            _currentThemeIndex = 0;
        }

        _regionLabel.text = _themeList[_currentThemeIndex];
        
        _nextTheme.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));
        _backTheme.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));

        if (_currentThemeIndex == 0)
        {
            _backTheme.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }

        if (_currentThemeIndex == _themeList.Count - 1)
        {
            _nextTheme.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }

        _currentThemeIndex++;
    }

    private void BackTheme()
    {
        if (_currentThemeIndex < 0)
        {
            _currentThemeIndex = _themeList.Count - 1;
        }

        _regionLabel.text = _themeList[_currentThemeIndex];
        
        _nextTheme.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));
        _backTheme.style.unityBackgroundImageTintColor =
            new StyleColor(new Color(0.2039216f, 0.9960784f, 0.007843138f));

        if (_currentThemeIndex == 0)
        {
            _backTheme.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }

        if (_currentThemeIndex == _themeList.Count - 1)
        {
            _nextTheme.style.unityBackgroundImageTintColor =
                new StyleColor(new Color(1f, 1f, 1f));
        }

        _currentThemeIndex--;
    }
}
