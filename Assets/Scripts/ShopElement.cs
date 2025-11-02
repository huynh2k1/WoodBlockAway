using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    public int ID;
    [SerializeField] Button _btn;
    public event Action<int> OnShopElementClick;

    [SerializeField] Image _iconMap;
    [SerializeField] GameObject _lockIcon;

    private void Awake()
    {
        _btn.onClick.AddListener(HandleOnClickThis);
    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    public void UpdateIconMap(Sprite iconMap)
    {
        _iconMap.sprite = iconMap;
        _iconMap.SetNativeSize();
    }

    public void HandleOnClickThis()
    {
        OnShopElementClick?.Invoke(ID);
    }

    public void CheckUnlock()
    {
        if (PlayerPrefData.IsMapUnlock(ID) == true)
            Unlock();
        else
            Lock();
    }

    void Unlock()
    {
        _btn.interactable = true;
        _lockIcon.SetActive(false);
    }

    void Lock()
    {
        _btn.interactable = false;
        _lockIcon.SetActive(true);  
    }
}
