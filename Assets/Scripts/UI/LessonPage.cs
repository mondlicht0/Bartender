using UnityEngine;
using UnityEngine.UIElements;

public class LessonPage : BasePage
    {
        private Label _title;
        private Label _firstStepText;
        private Label _secondStepText;
        private Label _thirdStepText;

        private VisualElement _image;
        private VisualElement _firstStep;
        private VisualElement _secondStep;
        private VisualElement _thirdStep;

        private VisualElement _continue;
        private VisualElement _nextArrowFirst;
        private VisualElement _nextArrowSecond;
        private VisualElement _backArrowSecond;
        private VisualElement _nextArrowThird;
        private VisualElement _backArrowThird;

        private int _currentStep;
        
        public LessonPage(VisualElement root, Header header, string name) : base(root, header, name)
        {
            SetupPage();
        }

        protected override void SetupPage()
        {
            _image = Root.Q("Image");
            _title = Root.Q<Label>("Title");
            _firstStep = Root.Q("FirstStep");
            _secondStep = Root.Q("SecondStep");
            _thirdStep = Root.Q("ThirdStep");

            _firstStepText = _firstStep.Q<Label>("Text");
            _secondStepText = _secondStep.Q<Label>("Text");
            _thirdStepText = _thirdStep.Q<Label>("Text");

            _continue = Root.Q("Continue");
            _nextArrowFirst = _firstStep.Q("NextArrow");
            _nextArrowSecond = _secondStep.Q("NextArrow");
            _nextArrowThird = _thirdStep.Q("NextArrow");
            _backArrowSecond = _secondStep.Q("BackArrow");
            _backArrowThird = _thirdStep.Q("BackArrow");
            
            _continue.RegisterCallback<ClickEvent>(evt => NextStep());
            _nextArrowFirst.RegisterCallback<ClickEvent>(evt => ShowSecondStep());
            _nextArrowSecond.RegisterCallback<ClickEvent>(evt => ShowThirdStep());
            _backArrowSecond.RegisterCallback<ClickEvent>(evt => ShowFirstStep());
            _backArrowThird.RegisterCallback<ClickEvent>(evt => ShowSecondStep());
        }

        private void NextStep()
        {
            switch (_currentStep)
            {
                case 0:
                    ShowSecondStep();
                    break;
                case 1:
                    ShowThirdStep();
                    break;
                case 2:
                    ShowFirstStep();
                    break;
            }
        }

        private void ShowFirstStep()
        {
            _currentStep = 0;
            _firstStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _secondStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _thirdStep.AddToClassList("third_step_inactive");
            _secondStep.AddToClassList("second_step_inactive");
        }
        
        private void ShowSecondStep()
        {
            _currentStep = 1;
            _firstStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _secondStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _firstStep.AddToClassList("first_step_inactive");
            _thirdStep.AddToClassList("third_step_inactive");
            _secondStep.RemoveFromClassList("second_step_inactive");
        }
        
        private void ShowThirdStep()
        {
            _currentStep = 2;
            _thirdStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            _firstStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _secondStep.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _thirdStep.RemoveFromClassList("third_step_inactive");
            _firstStep.AddToClassList("first_step_inactive");
            _secondStep.AddToClassList("second_step_inactive");
        }

        public void ChangeContent(string title, string first, string second, string third, string imagePath)
        {
            Texture2D image = Resources.Load<Texture2D>(imagePath);
            _image.style.backgroundImage = new StyleBackground(image);
            _title.text = title;
            _firstStepText.text = first;
            _secondStepText.text = second;
            _thirdStepText.text = third;
        }
    }