using H_Utils;
using UnityEngine;

public class UIControl : BaseUICtrl
{
    [SerializeField] UIGame _uiGame;
    private void OnEnable()
    {
        UIHome.OnClickHowToPlayButton += ShowHowToPlay;
        UIHome.OnClickPlayButton += ShowSelectLevel;
    }

    private void OnDisable()
    {
        UIHome.OnClickHowToPlayButton -= ShowHowToPlay;
        UIHome.OnClickPlayButton -= ShowSelectLevel;
    }

    void ShowHowToPlay()
    {
        Show(UIType.HowToPlay);
    }

    void ShowSelectLevel()
    {
        Show(UIType.SelectLevel);
    }

    public void UpdateTextLevel()
    {
        _uiGame.UpdateTextLevel();
    }
}
