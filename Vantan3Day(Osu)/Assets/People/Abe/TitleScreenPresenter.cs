using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

namespace TitleScreen
{
    public enum TitleButtonType
    {
        Start,
        Options,
        Quit
    }
    public class TitleScreenPresenter : MonoBehaviour
    {
        [SerializeField] UIDocument uiDocument;

        VisualElement _titlePanel;
        VisualElement _optionsPanel;

        TitleButton _start;
        TitleButton _options;
        TitleButton _quit;

        Button _back;

        bool _revealed;

        void OnEnable()
        {
            if (uiDocument == null)
            {
                Debug.LogError("[TitleScreenPresenter] UIDocument is not assigned.");
                return;
            }

            var root = uiDocument.rootVisualElement;
            if (root == null)
            {
                Debug.LogError("[TitleScreenPresenter] rootVisualElement is null.");
                return;
            }

            // 画面（パネル）参照
            _titlePanel = root.Q<VisualElement>("TitlePanel");
            _optionsPanel = root.Q<VisualElement>("OptionsPanel");

            // ボタン参照（TitleButtonのnameで取る）
            _start = root.Q<TitleButton>("start");
            _options = root.Q<TitleButton>("options");
            _quit = root.Q<TitleButton>("quit");

            // Options側の戻るボタン（通常のButton）
            _back = root.Q<Button>("back");

            if (_titlePanel == null) Debug.LogWarning("[TitleScreenPresenter] TitlePanel not found.");
            if (_optionsPanel == null) Debug.LogWarning("[TitleScreenPresenter] OptionsPanel not found.");

            if (_start == null) Debug.LogWarning("[TitleScreenPresenter] start TitleButton not found.");
            if (_options == null) Debug.LogWarning("[TitleScreenPresenter] options TitleButton not found.");
            if (_quit == null) Debug.LogWarning("[TitleScreenPresenter] quit TitleButton not found.");

            // ラベル設定（任意）
            if (_start != null) _start.Text = "Start";
            if (_options != null) _options.Text = "Options";
            if (_quit != null) _quit.Text = "Exit";

            // イベント登録
            if (_start != null) _start.Clicked += OnTitleButtonClicked;
            if (_options != null) _options.Clicked += OnTitleButtonClicked;
            if (_quit != null) _quit.Clicked += OnTitleButtonClicked;
            if (_back != null) _back.clicked += ShowTitle;

            // 初期状態：タイトル表示、オプション非表示
            ShowTitle();

            // 初期状態：ボタンは隠す（入力で出す）
            _revealed = false;
            if (_start != null) _start.SetShown(false);
            if (_options != null) _options.SetShown(false);
            if (_quit != null) _quit.SetShown(false);
        }

        void OnDisable()
        {
            // イベント解除（OnEnableが複数回走っても多重登録しないように）
            if (_start != null) _start.Clicked -= OnTitleButtonClicked;
            if (_options != null) _options.Clicked -= OnTitleButtonClicked;
            if (_quit != null) _quit.Clicked -= OnTitleButtonClicked;
            if (_back != null) _back.clicked -= ShowTitle;
        }
        public void ButtonInput(TitleButtonType buttonType)
        {
            switch (buttonType)
            {
                case TitleButtonType.Start:
                    RevealWithDelay(_start, 40);
                    break;
                case TitleButtonType.Options:
                    RevealWithDelay(_options, 40);
                    break;
                case TitleButtonType.Quit:
                    RevealWithDelay(_quit, 40);
                    break;
            }
        }

        void RevealWithDelay(TitleButton button, long delayMs)
        {
            if (button == null) return;

            button.schedule.Execute(() =>
            {
                button.SetShown(true);
            }).StartingIn(delayMs);
        }

        void OnTitleButtonClicked(TitleButton button)
        {
            if (button == null) return;

            switch (button.name)
            {
                case "start":
                    Debug.Log("START");
                    // 例：SceneManager.LoadScene("Game");
                    break;

                case "options":
                    ShowOptions();
                    break;

                case "quit":
                    Application.Quit();
                    break;

                default:
                    Debug.LogWarning($"[TitleScreenPresenter] Unknown TitleButton name: {button.name}");
                    break;
            }
        }

        void ShowOptions()
        {
            if (_titlePanel != null) _titlePanel.style.display = DisplayStyle.None;
            if (_optionsPanel != null) _optionsPanel.style.display = DisplayStyle.Flex;
        }

        void ShowTitle()
        {
            if (_optionsPanel != null) _optionsPanel.style.display = DisplayStyle.None;
            if (_titlePanel != null) _titlePanel.style.display = DisplayStyle.Flex;
        }


    }
}