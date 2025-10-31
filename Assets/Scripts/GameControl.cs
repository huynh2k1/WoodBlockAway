using Unity.VisualScripting;
using UnityEngine;

public class GameControl : BaseGameCtrl
{
    public static GameControl I;
    public UIControl uiCtrl;
    public GameState State;
    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Home();
    }

    private void OnEnable()
    {
        //UIGame
        UIGame.EventClickHome += Home;
        UIGame.EventClickReplay += ReplayGame;
        UIGame.EventClickSetting += Setting;
        UIGame.EventClickShop += Shop;

        //UIHome
        UIHome.OnClickPlayButton += PlayGame;
    }

    private void OnDisable()
    {
        //UIGame
        UIGame.EventClickHome -= Home;
        UIGame.EventClickReplay -= ReplayGame;
        UIGame.EventClickSetting -= Setting;
        UIGame.EventClickShop -= Shop;

        //UIHome
        UIHome.OnClickPlayButton -= PlayGame;
    }

    public override void Home()
    {
        State = GameState.None;
        uiCtrl.Hide(UIType.Game);
        uiCtrl.Show(UIType.Home);

    }

    public override void PlayGame()
    {
        State = GameState.Playing;
        uiCtrl.Show(UIType.Game);
        uiCtrl.Hide(UIType.Home);
    }

    public override void ReplayGame()
    {
        State = GameState.Playing;
    }

    public override void WinGame()
    {
        State = GameState.None;
    }

    public void Setting()
    {
        State = GameState.None;
        uiCtrl.Show(UIType.Setting);
    }

    public void Shop()
    {
        State = GameState.None;
        uiCtrl.Show(UIType.Shop);
    }
}

public enum GameState
{
    None,
    Playing
}
