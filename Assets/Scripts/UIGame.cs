using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : PanelBase
{
    public override UIType Type => UIType.Game;

    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnSetting;
    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnShop;

    public static Action EventClickHome;
    public static Action EventClickSetting;
    public static Action EventClickReplay;
    public static Action EventClickShop;

    private void Awake()
    {
        _btnHome.onClick.AddListener(UserClickHome);
        _btnSetting.onClick.AddListener(UserClickSetting);
        _btnReplay.onClick.AddListener(UserClickReplay);
        _btnShop.onClick.AddListener(UserClickShop);
    }

    void UserClickHome()
    {
        EventClickHome?.Invoke();
    }

    void UserClickSetting()
    {
        EventClickSetting?.Invoke();
    }

    void UserClickReplay()
    {
        EventClickReplay?.Invoke();
    }

    void UserClickShop()
    {
        EventClickShop?.Invoke();
    }
}
