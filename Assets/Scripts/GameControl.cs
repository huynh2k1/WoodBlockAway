using Unity.VisualScripting;
using UnityEngine;

public class GameControl : BaseGameCtrl
{
    public static GameControl I;
    public LevelCtrl levelCtrl;
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


        CompleteUI.OnButtonHomeClick += Home;   
        CompleteUI.OnButtonNextClick += NextGame;
    }

    private void OnDisable()
    {
        //UIGame
        UIGame.EventClickHome -= Home;
        UIGame.EventClickReplay -= ReplayGame;
        UIGame.EventClickSetting -= Setting;
        UIGame.EventClickShop -= Shop;

        CompleteUI.OnButtonHomeClick -= Home;
        CompleteUI.OnButtonNextClick -= NextGame;
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
        levelCtrl.InitLevel();
    }

    public override void ReplayGame()
    {
        State = GameState.Playing;
        levelCtrl.InitLevel();
    }

    public override void NextGame()
    {
        State = GameState.Playing;
        levelCtrl.InitLevel();
    }

    public override void WinGame()
    {
        PlayerPrefData.Coin += 100;
        CoinCtrl.I.UpdateCoin();

        State = GameState.None;
        levelCtrl.CheckIncreaseLevel();
        uiCtrl.Show(UIType.Win);
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
