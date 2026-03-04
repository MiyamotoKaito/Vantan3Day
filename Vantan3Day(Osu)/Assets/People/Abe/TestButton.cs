using System.Threading.Tasks;
using UnityEngine;
namespace TitleScreen
{
    public class TestButton : MonoBehaviour
    {
        [SerializeField]
        TitleScreenPresenter _presenter;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Hover(TitleButtonType type)
        {
            _presenter.ButtonInput(type,true);
        }

        public void Unhover(TitleButtonType type)
        {
            _presenter.ButtonInput(type,false);
        }

        public void Click(TitleButton button)
        {
            _presenter.OnTitleButtonClicked(button);
        }

    }

}