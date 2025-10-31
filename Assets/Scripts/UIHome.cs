using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHome : PanelBase
{
    public override UIType Type => UIType.Home;

    [SerializeField] Button _btnPlay;
    [SerializeField] Button _btnHowToPlay;

    public static Action OnClickPlayButton;
    public static Action OnClickHowToPlayButton;

    private void Awake()
    {
        _btnPlay.onClick.AddListener(RegisterEventPlayButton);
        _btnHowToPlay.onClick.AddListener(RegisterEventHowToPlayButton);    
    }

    void RegisterEventPlayButton()
    {
        OnClickPlayButton?.Invoke();
    }

    void RegisterEventHowToPlayButton()
    {
        OnClickHowToPlayButton?.Invoke();
    }
}
