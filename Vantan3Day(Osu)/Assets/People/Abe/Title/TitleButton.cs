using System;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TitleScreen
{
    public class TitleButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TitleButton, UxmlTraits> { }

        const string UxmlPath = "Assets/UI Toolkit/Title-Button.uxml";

        public event Action<TitleButton> Clicked;

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription _textAttribute = new UxmlStringAttributeDescription
            {
                name = "text",
                defaultValue = "Button",
            };
        }

        public readonly Button _button;
        Action _onClicked;

        public TitleButton()
        {
            var container = BuildFromUxml();
            hierarchy.Add(container);

            _button = container.Q<Button>("Title-Button");
            if (_button == null)
            {
                Debug.Log("Button is null");
            }

            _button.clicked += OnClicked;
            SetShown(false);

        }

        public string Text
        {
            get => _button.text;
            set => _button.text = value;
        }

        public void SetAction(Action<TitleButton> action)
        {
            Clicked += action;
        }

        public void ClearAction(Action<TitleButton> action)
        {
            Clicked -= action;
        }

        void OnClicked()
        {
            Clicked?.Invoke(this);
        }

        public void SetShown(bool shown)
        {
            if (_button == null) return;

            _button.EnableInClassList("is-shown", shown);
            _button.EnableInClassList("is-hidden", !shown);
            _button.pickingMode = shown ? PickingMode.Position : PickingMode.Ignore;
        }




        static VisualElement BuildFromUxml()
        {
#if UNITY_EDITOR
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            if (tree != null)
            {
                return tree.Instantiate();
            }

            Debug.LogError($"[TitleButton] Failed to load UXML at path: {UxmlPath}");
#endif
            return new VisualElement();
        }
    }
}
