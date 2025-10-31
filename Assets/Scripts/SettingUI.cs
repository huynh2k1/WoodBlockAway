using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BasePopup
{
    public override UIType Type => UIType.Setting;

    [SerializeField] Button _btnOK;

    public override void Awake()
    {
        _btnOK.onClick.AddListener(OnClickOK);
    }

    void OnClickOK()
    {
        Hide();
        GameControl.I.State = GameState.Playing;
    }
}
