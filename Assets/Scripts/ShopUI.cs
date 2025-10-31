using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BasePopup
{
    public override UIType Type => UIType.Shop;

    [SerializeField] Button _btnRandomMap500;

    public static Action OnClickRandomMap500Button;

    public override void Awake()
    {
        base.Awake();
        _btnRandomMap500.onClick.AddListener(RegisterEventRandomMap500Button);
    }

    void RegisterEventRandomMap500Button()
    {
        OnClickRandomMap500Button?.Invoke();
    }

    public override void Hide()
    {
        base.Hide();
        GameControl.I.State = GameState.Playing;
    }
}
