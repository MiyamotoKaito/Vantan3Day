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

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((TitleButton)ve).Text = _textAttribute.GetValueFromBag(bag, cc);
            }
        }

        readonly Button _button;
        Action _onClicked;

        public TitleButton()
        {
            var container = BuildFromUxml();
            hierarchy.Add(container);

            _button = container.Q<Button>("Title-Button");
            if (_button == null)
            {
                _button = container.Q<Button>();
            }

            if (_button == null)
            {
                _button = new Button();
                _button.text = "Button";
                _button.AddToClassList("title-button");
                hierarchy.Add(_button);
            }

            _button.clicked += OnClicked;
        }

        public string Text
        {
            get => _button.text;
            set => _button.text = value;
        }

        public void SetAction(Action action)
        {
            _onClicked = action;
        }

        void OnClicked()
        {
            Clicked?.Invoke(this);
            _onClicked?.Invoke();
        }

        public void SetShown(bool shown)
        {
            if (shown)
            {
                _button.RemoveFromClassList("is-hidden");
                _button.AddToClassList("is-shown");
                _button.pickingMode = PickingMode.Position;
            }
            else
            {
                _button.RemoveFromClassList("is-shown");
                _button.AddToClassList("is-hidden");
                _button.pickingMode = PickingMode.Ignore;
            }
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
