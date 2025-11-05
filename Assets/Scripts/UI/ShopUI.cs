using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BasePopup
{
    public override UIType Type => UIType.Shop;

    [SerializeField] Button _btnRandomMap500;
    [SerializeField] GameObject _lockBtnRandom500;


    [SerializeField] BackgroundSO _data;
    [SerializeField] ShopElement[] _listElement;

    public override void Awake()
    {
        base.Awake();
        _btnRandomMap500.onClick.AddListener(RegisterEventRandomMap500Button);
        InitShopElement();
    }

    public override void Show()
    {
        base.Show();
        ReloadUI();
    }

    void RegisterEventRandomMap500Button()
    {
        //random 1 cái shop element chưa mở khóa => mở khóa
        //Reload UI
        PlayerPrefData.Coin -= 500;
        CoinCtrl.I.UpdateCoin();    
        //Lấy danh sách BG chưa unlock
        var lockedList = new System.Collections.Generic.List<int>();
        for (int i = 0; i < _listElement.Length; i++)
        {
            if (PlayerPrefData.IsBGUnlock(i) == false)
                lockedList.Add(i);
        }

        //Random 1 cái trong danh sách chưa unlock
        int randomIndex = UnityEngine.Random.Range(0, lockedList.Count);
        int unlockID = lockedList[randomIndex];

        //Lưu mở khóa
        PlayerPrefData.SetBGUnlock(unlockID, true);
        PlayerPrefData.CurBackgroundID = unlockID;
        //Cập nhật background ngay lập tức (option)
        BackgroundCtrl.I.UpdateBG(unlockID);

        //Reload lại UI để cập nhật button, lock overlay,...
        ReloadUI();
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
        }
    }

    public void RaiseOnElementClicked(int id)
    {
        BackgroundCtrl.I.UpdateBG(id);
    }

    public void ReloadUI()
    {
        foreach(var element in _listElement)
        {
            element.CheckUnlock();
        }

        //Check Show Button Random 500
        ShowBtnRandom500(!IsAllBackgroundUnlocked());
        if (IsAllBackgroundUnlocked())
        {
            return;
        }

        InteractableBtnRandom500(PlayerPrefData.Coin >= 500);
    }

    void InteractableBtnRandom500(bool isInteractable)
    {
        _btnRandomMap500.interactable = isInteractable;
        _btnRandomMap500.GetComponent<Image>().raycastTarget = isInteractable;  
        _lockBtnRandom500.SetActive(!isInteractable);
    }

    void ShowBtnRandom500(bool isShow)
    {
        _btnRandomMap500.gameObject.SetActive(isShow);
    }

    bool IsAllBackgroundUnlocked()
    {
        for(int i = 0; i < _listElement.Length; i++)
        {
            if (PlayerPrefData.IsBGUnlock(i) == false)
                return false;
        }
        return true;
    }

    //Logic click random 500
}
