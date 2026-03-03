using UnityEngine;
using UnityEngine.UIElements;

namespace TitileScreen
{
    public class TitleScreenPresenter : MonoBehaviour
    {
        [SerializeField] UIDocument uiDocument;

        VisualElement _titlePanel;
        VisualElement _optionsPanel;

        TitleButton _startButton;
        TitleButton _optionsButton;
        TitleButton _quitButton;

        Button _backButton;

        void OnEnable()
        {
            var root = uiDocument.rootVisualElement;

            _titlePanel   = root.Q<VisualElement>("TitlePanel");
            _optionsPanel = root.Q<VisualElement>("OptionsPanel");

            _startButton   = root.Q<TitleButton>("start");
            _optionsButton = root.Q<TitleButton>("options");
            _quitButton    = root.Q<TitleButton>("quit");

            _backButton = root.Q<Button>("back");

            // イベント登録
            if (_startButton   != null) _startButton.Clicked   += OnTitleButtonClicked;
            if (_optionsButton != null) _optionsButton.Clicked += OnTitleButtonClicked;
            if (_quitButton    != null) _quitButton.Clicked    += OnTitleButtonClicked;

            if (_backButton != null)
                _backButton.clicked += ShowTitle;

            // 初期表示
            ShowTitle();
        }

        void OnDisable()
        {
            // 必ず解除（重複防止）
            if (_startButton   != null) _startButton.Clicked   -= OnTitleButtonClicked;
            if (_optionsButton != null) _optionsButton.Clicked -= OnTitleButtonClicked;
            if (_quitButton    != null) _quitButton.Clicked    -= OnTitleButtonClicked;

            if (_backButton != null)
                _backButton.clicked -= ShowTitle;
        }

        void OnTitleButtonClicked(TitleButton button)
        {
            switch (button.name)
            {
                case "start":
                    Debug.Log("START GAME");
                    break;

                case "options":
                    ShowOptions();
                    break;

                case "quit":
                    Application.Quit();
                    break;
            }
        }

        void ShowOptions()
        {
            _titlePanel.style.display   = DisplayStyle.None;
            _optionsPanel.style.display = DisplayStyle.Flex;
        }

        void ShowTitle()
        {
            _optionsPanel.style.display = DisplayStyle.None;
            _titlePanel.style.display   = DisplayStyle.Flex;
        }
    }
}