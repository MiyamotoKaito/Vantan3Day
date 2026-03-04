using UnityEngine;
using UnityEngine.UIElements;
namespace Option
{

    public class OptionButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<OptionButton, UxmlTraits> { }
        const string UxmlPath = "Assets/UI Toolkit/Option.uxml";
    }
}