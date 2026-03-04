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
        [SerializeField] Camera worldCamera;
        [SerializeField] Transform startTarget;
        [SerializeField] Transform optionsTarget;
        [SerializeField] Transform quitTarget;
        [SerializeField] Vector3 startLocalOffset;
        [SerializeField] Vector3 optionsLocalOffset;
        [SerializeField] Vector3 quitLocalOffset;
        [SerializeField] Vector2 screenOffset;
        [SerializeField] bool hideWhenTargetIsBehindCamera = true;

        VisualElement _titlePanel;
        VisualElement _optionsPanel;
        VisualElement _menuView;
        VisualElement _root;

        TitleButton _start;
        TitleButton _options;
        TitleButton _quit;

        Button _back;

        bool _revealed;
        bool _isOptionsShown;

        void OnEnable()
        {
            if (uiDocument == null)
            {
                Debug.LogError("[TitleScreenPresenter] UIDocument is not assigned.");
                return;
            }

            _root = uiDocument.rootVisualElement;
            if (_root == null)
            {
                Debug.LogError("[TitleScreenPresenter] rootVisualElement is null.");
                return;
            }

            _titlePanel = _root.Q<VisualElement>("TitlePanel");
            _optionsPanel = _root.Q<VisualElement>("OptionsPanel");
            _menuView = _root.Q<VisualElement>("MenuView");

            _start = _root.Q<TitleButton>("start");
            _options = _root.Q<TitleButton>("options");
            _quit = _root.Q<TitleButton>("quit");
            _back = _root.Q<Button>("back");

            if (_titlePanel == null) Debug.LogWarning("[TitleScreenPresenter] TitlePanel not found.");
            if (_optionsPanel == null) Debug.LogWarning("[TitleScreenPresenter] OptionsPanel not found.");
            if (_menuView == null) Debug.LogWarning("[TitleScreenPresenter] MenuView not found.");
            if (_start == null) Debug.LogWarning("[TitleScreenPresenter] start TitleButton not found.");
            if (_options == null) Debug.LogWarning("[TitleScreenPresenter] options TitleButton not found.");
            if (_quit == null) Debug.LogWarning("[TitleScreenPresenter] quit TitleButton not found.");

            if (_start != null) _start.Text = "Start";
            if (_options != null) _options.Text = "Options";
            if (_quit != null) _quit.Text = "Exit";

            if (_start != null) _start.SetAction(OnTitleButtonClicked);
            if (_options != null) _options.SetAction(OnTitleButtonClicked);
            if (_quit != null) _quit.SetAction(OnTitleButtonClicked);
            if (_back != null) _back.clicked += ShowTitle;

            if (worldCamera == null) worldCamera = Camera.main;

            SetupFollowContainerStyle();
            SetupFollowButtonStyle(_start);
            SetupFollowButtonStyle(_options);
            SetupFollowButtonStyle(_quit);

            ShowTitle();

            _revealed = false;
            _isOptionsShown = false;
            if (_start != null) _start.SetShown(false);
            if (_options != null) _options.SetShown(false);
            if (_quit != null) _quit.SetShown(false);
        }

        void OnDisable()
        {
            if (_start != null) _start.ClearAction(OnTitleButtonClicked);
            if (_options != null) _options.ClearAction(OnTitleButtonClicked);
            if (_quit != null) _quit.ClearAction(OnTitleButtonClicked);
            if (_back != null) _back.clicked -= ShowTitle;
        }

        void Update()
        {
            UpdateButtonFollow(_start, startTarget, startLocalOffset);
            UpdateButtonFollow(_options, optionsTarget, optionsLocalOffset);
            UpdateButtonFollow(_quit, quitTarget, quitLocalOffset);
        }


        public void ButtonInput(TitleButtonType buttonType,bool shown )
        {
            if (_isOptionsShown && buttonType != TitleButtonType.Options)
            {
                return;
            }

            switch (buttonType)
            {
                case TitleButtonType.Start:
                    _start.SetShown(shown);
                    break;
                case TitleButtonType.Options:
                    _options.SetShown(shown);
                    break;
                case TitleButtonType.Quit:
                    _quit.SetShown(shown);
                    break;
            }
        }

      public  void OnTitleButtonClicked(TitleButton button)
        {
            if (button == null) return;

            if (_isOptionsShown && !string.Equals(button.name, "options", System.StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            switch (button.name.ToLower())
            {
                case "start":
                    Debug.Log("START");
                    // SceneManager.LoadScene("Game");
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
            _isOptionsShown = true;
            if (_titlePanel != null) _titlePanel.style.display = DisplayStyle.None;
            if (_optionsPanel != null) _optionsPanel.style.display = DisplayStyle.Flex;
            if (_start != null) _start.SetShown(false);
            if (_quit != null) _quit.SetShown(false);
        }

        void ShowTitle()
        {
            _isOptionsShown = false;
            if (_optionsPanel != null) _optionsPanel.style.display = DisplayStyle.None;
            if (_titlePanel != null) _titlePanel.style.display = DisplayStyle.Flex;
        }

        void SetupFollowButtonStyle(TitleButton button)
        {
            if (button == null) return;
            button.style.position = Position.Absolute;
        }

        void SetupFollowContainerStyle()
        {
            if (_menuView == null) return;

            _menuView.style.position = Position.Absolute;
            _menuView.style.left = 0f;
            _menuView.style.top = 0f;
            _menuView.style.right = 0f;
            _menuView.style.bottom = 0f;
            _menuView.style.height = StyleKeyword.Auto;
            _menuView.style.marginBottom = 0f;
        }

        void UpdateButtonFollow(TitleButton button, Transform target, Vector3 localOffset)
        {
            if (button == null || target == null || _root == null || _root.panel == null || worldCamera == null) return;

            var worldPos = target.TransformPoint(localOffset);
            var screenPos = worldCamera.WorldToScreenPoint(worldPos);
            var isBehind = screenPos.z <= 0f;

            if (hideWhenTargetIsBehindCamera && isBehind)
            {
                button.style.display = DisplayStyle.None;
                return;
            }

            button.style.display = DisplayStyle.Flex;

            var panelPos = RuntimePanelUtils.ScreenToPanel(_root.panel, new Vector2(screenPos.x,screenPos.y));
            var rootHeight = _root.resolvedStyle.height;
            if (rootHeight > 0f)
            {
                panelPos.y = rootHeight - panelPos.y;
            }
            panelPos += screenOffset;

            var width = button.resolvedStyle.width > 0f ? button.resolvedStyle.width : 420f;
            var height = button.resolvedStyle.height > 0f ? button.resolvedStyle.height : 80f;

            button.style.left = panelPos.x - (width * 0.5f);
            button.style.top = panelPos.y - (height * 0.5f);
        }
    }
}
