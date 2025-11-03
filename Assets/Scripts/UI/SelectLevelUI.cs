using UnityEngine;

public class SelectLevelUI : BasePopup
{
    public override UIType Type => UIType.SelectLevel;

    [SerializeField] ButtonLevel[] listBtnLevel;

    public override void Awake()
    {
        base.Awake();
        InitButtonLevel();
    }

    private void OnEnable()
    {
        for (int i = 0; i < listBtnLevel.Length; i++)
        {
            listBtnLevel[i].CheckUnlock();
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < listBtnLevel.Length; i++)
        {
            listBtnLevel[i].OnClickThisEvent -= HandleOnClickLevelEvent;
        }
    }

    void InitButtonLevel()
    {
        for(int i = 0; i < listBtnLevel.Length; i++)
        {
            listBtnLevel[i].Initialize(i);
            listBtnLevel[i].OnClickThisEvent += HandleOnClickLevelEvent;
        }
    }

    void HandleOnClickLevelEvent(int levelID)
    {
        Hide();
        PlayerPrefData.CurLevel = levelID;
        GameControl.I.PlayGame();
    }
}
