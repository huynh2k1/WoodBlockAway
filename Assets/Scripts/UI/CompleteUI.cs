using System;
using UnityEngine;
using UnityEngine.UI;

public class CompleteUI : BasePopup
{
    public override UIType Type => UIType.Win;

    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnNext;

    public static Action OnButtonHomeClick;
    public static Action OnButtonNextClick;

    public override void Awake()
    {
        _btnHome.onClick.AddListener(OnClickHomeButton);
        _btnNext.onClick.AddListener(OnClickNextButton);
    }

    void OnClickHomeButton()
    {
        Hide();
        OnButtonHomeClick?.Invoke();    
    }
    void OnClickNextButton()
    {
        Hide();
        OnButtonNextClick?.Invoke();
    }
}
