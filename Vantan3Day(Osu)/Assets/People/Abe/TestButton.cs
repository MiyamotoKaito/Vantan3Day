using UnityEngine;
namespace TitileScreen{
public class TestButton : MonoBehaviour
{
    [SerializeField]
   TitleScreenPresenter _presenter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _presenter.ButtonInput(TitleButtonType.Start);
        _presenter.ButtonInput(TitleButtonType.Options);
        _presenter.ButtonInput(TitleButtonType.Quit);

    }

}

}