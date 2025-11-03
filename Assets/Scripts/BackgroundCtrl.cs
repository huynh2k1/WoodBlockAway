using UnityEngine;
using UnityEngine.UI;

public class BackgroundCtrl : MonoBehaviour
{
    public static BackgroundCtrl I;
    public BackgroundSO data;
    [SerializeField] private Image _bg;

    private void Awake()
    {
        I = this;   
        PlayerPrefData.SetBGUnlock(0, true);    
    }

    private void Start()
    {
        UpdateBG(PlayerPrefData.CurBackgroundID);
    }

    public void UpdateBG(int ID)
    {
        PlayerPrefData.CurBackgroundID = ID;
        Sprite bg = data.data[ID].bgTexture;
        UpdateBG(bg);
    }

    void UpdateBG(Sprite bg)
    {
        _bg.sprite = bg;
    }
}
