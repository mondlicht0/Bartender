using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LessonsPage : BasePage
{
    private Label _level1;
    private VisualElement _levels1;
    private VisualElement _lesson1_1;
    private VisualElement _lesson1_2;
    private VisualElement _lesson1_3;
    private VisualElement _lesson1_4;
    private VisualElement _lesson1_5;

    private Label _level2;
    private VisualElement _levels2;
    private VisualElement _lesson2_1;
    private VisualElement _lesson2_2;
    private VisualElement _lesson2_3;
    private VisualElement _lesson2_4;
    private VisualElement _lesson2_5;

    private List<Lesson> _lessons1 = new();
    private List<Lesson> _lessons2 = new();
    private LessonPage _lessonPage;
    
    public LessonsPage(VisualElement root, Header header, string name, LessonPage lessonPage, List<Lesson> lessons1, List<Lesson> lessons2) : base(root, header, name)
    {
        _lessons1 = lessons1;
        _lessons2 = lessons2;
        _lessonPage = lessonPage;
        SetupPage();
    }
    
    protected override void SetupPage()
    {
        _level1 = Root.Q<Label>("Level1");
        _levels1 = Root.Q("Levels1");
        _lesson1_1 = _levels1.Q("Level1_1");
        _lesson1_2 = _levels1.Q("Level1_2");
        _lesson1_3 = _levels1.Q("Level1_3");
        _lesson1_4 = _levels1.Q("Level1_4");
        _lesson1_5 = _levels1.Q("Level1_5");

        _level2 = Root.Q<Label>("Level2");
        _levels2 = Root.Q("Levels2");
        _lesson2_1 = _levels2.Q("Level2_1");
        _lesson2_2 = _levels2.Q("Level2_2");
        _lesson2_3 = _levels2.Q("Level2_3");
        _lesson2_4 = _levels2.Q("Level2_4");
        _lesson2_5 = _levels2.Q("Level2_5");
        
        _level1.RegisterCallback<ClickEvent>(evt => ShowLevel(true));
        _level2.RegisterCallback<ClickEvent>(evt => ShowLevel(false));
        
        _lesson1_1.RegisterCallback<ClickEvent>(evt => ShowLessonPage(0, _lessons1));
        _lesson1_2.RegisterCallback<ClickEvent>(evt => ShowLessonPage(1, _lessons1));
        _lesson1_3.RegisterCallback<ClickEvent>(evt => ShowLessonPage(2, _lessons1));
        _lesson1_4.RegisterCallback<ClickEvent>(evt => ShowLessonPage(3, _lessons1));
        _lesson1_5.RegisterCallback<ClickEvent>(evt => ShowLessonPage(4, _lessons1));
        
        _lesson2_1.RegisterCallback<ClickEvent>(evt => ShowLessonPage(0, _lessons2));
        _lesson2_2.RegisterCallback<ClickEvent>(evt => ShowLessonPage(1, _lessons2));
        _lesson2_3.RegisterCallback<ClickEvent>(evt => ShowLessonPage(2, _lessons2));
        _lesson2_4.RegisterCallback<ClickEvent>(evt => ShowLessonPage(3, _lessons2));
        _lesson2_5.RegisterCallback<ClickEvent>(evt => ShowLessonPage(4, _lessons2));
    }

    private void ShowLessonPage(int index, List<Lesson> lessons)
    {
        ChangePageTo(_lessonPage);
        _lessonPage.ChangeContent(lessons[index].Title, lessons[index].FirstParagraph, lessons[index].SecondParapgraph, lessons[index].ThirdParagraph, lessons[index].ImagePath);
    }

    private void ShowLevel(bool isLevel1)
    {
        _levels2.style.display = isLevel1 ? new StyleEnum<DisplayStyle>(DisplayStyle.None) : new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        _levels1.style.display = isLevel1 ? new StyleEnum<DisplayStyle>(DisplayStyle.Flex) : new StyleEnum<DisplayStyle>(DisplayStyle.None);

        if (isLevel1)
        {
            _level1.AddToClassList("level_active");
            _level2.RemoveFromClassList("level_active");
        }

        else
        {
            _level2.AddToClassList("level_active");
            _level1.RemoveFromClassList("level_active");
        }
    }
}

public class Lesson
{
    public string ImagePath;
    public string Title;
    public string FirstParagraph;
    public string SecondParapgraph;
    public string ThirdParagraph;

    public Lesson(string title, string first, string second, string third, string imagePath)
    {
        ImagePath = imagePath;
        Title = title;
        FirstParagraph = first;
        SecondParapgraph = second;
        ThirdParagraph = third;
    }
}
