using UnityEngine;

public class LevelCtrl : MonoBehaviour
{
    [SerializeField] Level[] listLevel;
    Level _curLevel;

    public void InitLevel()
    {
        DestroyCurLevel();
        _curLevel = Instantiate(listLevel[PlayerPrefData.CurLevel], transform);
    }


    public void CheckIncreaseLevel()
    {
        if(PlayerPrefData.CurLevel < listLevel.Length - 1)
        {
            PlayerPrefData.CurLevel++;
            if (PlayerPrefData.CurLevel > PlayerPrefData.CurLevelUnlock) 
                PlayerPrefData.CurLevelUnlock = PlayerPrefData.CurLevel;
        }
        else
        {
            PlayerPrefData.CurLevel = 0;
        }
    }

    public void DestroyCurLevel()
    {
        if(_curLevel)
            _curLevel.Destroy();
        _curLevel = null;
    }

}
