using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BasePopup
{
    public override UIType Type => UIType.Setting;

    [SerializeField] Slider _sliderMusic;
    [SerializeField] Slider _sliderSound;
    [SerializeField] Button _btnOK;

    public override void Awake()
    {
        _btnOK.onClick.AddListener(OnClickOK);

        _sliderMusic.onValueChanged.AddListener((v) =>
        {
            OnValueMusicChange(v);
        });
        _sliderSound.onValueChanged.AddListener((v) =>
        {
            OnValueSoundChange(v);
        });
    }

    public override void Show()
    {
        base.Show();
        _sliderMusic.value = PlayerPrefData.Music;
        _sliderSound.value = PlayerPrefData.Sound;
    }

    void OnClickOK()
    {
        Hide();
        GameControl.I.State = GameState.Playing;
    }

    void OnValueSoundChange(float value)
    {
        PlayerPrefData.Sound = value;
        SFXCtrl.I.OnChangeVolumnSounds();
    }

    void OnValueMusicChange(float value)
    {
        PlayerPrefData.Music = value;
        SFXCtrl.I.OnChangeVolumnMusic();
    }
}
