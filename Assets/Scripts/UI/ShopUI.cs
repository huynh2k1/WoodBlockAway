using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BasePopup
{
    public override UIType Type => UIType.Shop;

    [SerializeField] Button _btnRandomMap500;

    public static Action OnClickRandomMap500Button;

    [SerializeField] BackgroundSO _data;
    [SerializeField] ShopElement[] _listElement;

    public override void Awake()
    {
        base.Awake();
        _btnRandomMap500.onClick.AddListener(RegisterEventRandomMap500Button);
    }

    private void Start()
    {
        InitShopElement();
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

    //Init Shop Elements
    void InitShopElement()
    {
        for(int i = 0; i < _listElement.Length; i++)
        {
            _listElement[i].ID = i;
            _listElement[i].UpdateIconMap(_data.data[i].bgIcon);
            _listElement[i].OnShopElementClick += RaiseOnElementClicked;

            _listElement[i].CheckUnlock();
        }
    }

    public void RaiseOnElementClicked(int id)
    {
        
    }

}
