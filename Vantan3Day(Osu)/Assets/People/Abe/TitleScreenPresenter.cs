using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TitileScreen
{
    public class TitleScreenPresenter : MonoBehaviour
    {
        [SerializeField] UIDocument uiDocument;

        Dictionary<string, Action> _actions;
        Dictionary<string, string> _labels;

        public event Action OnStartClicked;
        public event Action OnOptionsClicked;
        public event Action OnQuitClicked;

        void Awake()
        {
            _actions = new Dictionary<string, Action>
            {
                ["start"]   = () => OnStartClicked?.Invoke(),
                ["options"] = () => OnOptionsClicked?.Invoke(),
                ["quit"]    = () => OnQuitClicked?.Invoke(),
            };

            _labels = new Dictionary<string, string>
            {
                ["start"]   = "Start",
                ["options"] = "Options",
                ["quit"]    = "Exit",
            };
        }

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

            var buttons = root.Query<TitleButton>().ToList();
            if (buttons == null || buttons.Count == 0)
            {
                Debug.LogError("[TitleScreenPresenter] No TitleButton found in UIDocument.");
                return;
            }

            foreach (var b in buttons)
            {
                // UXMLの name を "start/options/quit" にしておく
                var key = b.name;

                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogWarning("[TitleScreenPresenter] TitleButton has empty name. Skipped.");
                    continue;
                }

                if (!_actions.TryGetValue(key, out var action))
                {
                    Debug.LogWarning($"[TitleScreenPresenter] No action mapped for key '{key}'.");
                    continue;
                }

                if (_labels.TryGetValue(key, out var label))
                    b.Text = label;

                b.SetAction(action);
            }
        }
    }
}