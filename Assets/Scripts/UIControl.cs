using H_Utils;
using UnityEngine;

public class UIControl : BaseUICtrl
{
    private void OnEnable()
    {
        UIHome.OnClickHowToPlayButton += ShowHowToPlay;
    }

    private void OnDisable()
    {
        UIHome.OnClickHowToPlayButton -= ShowHowToPlay;
    }

    void ShowHowToPlay()
    {
        Show(UIType.HowToPlay);
    }
}
