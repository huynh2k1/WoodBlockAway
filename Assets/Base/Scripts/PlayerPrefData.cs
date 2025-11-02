using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerPrefData
{
    public static int CurLevel
    {
        get => PlayerPrefs.GetInt(ConstUtils.CURLEVEL, 0);
        set => PlayerPrefs.SetInt(ConstUtils.CURLEVEL, value);
    }

    public static int Coin
    {
        get => PlayerPrefs.GetInt(ConstUtils.COIN);
        set => PlayerPrefs.SetInt(ConstUtils.COIN, value);
    }

    public static bool IsMapUnlock(int id)
    {
        return PlayerPrefs.GetInt(ConstUtils.MAPUNLOCK + id, 0) == 1 ? true : false; 
    }

    public static void SetMapUnlock(int id, bool value)
    {
        PlayerPrefs.SetInt(ConstUtils.MAPUNLOCK + id, value ? 1 : 0);
    }
}

/// <summary>
/// STATIC CLASS 
/// Không thể kế thừa hoặc bị kế thừa
/// Không thế khởi tạo new Class()
/// Chỉ chứa static members
/// </summary>

public class ConstUtils
{
    public static string CURLEVEL = "CURLEVEL";
    public static string COIN = "COIN";
    public static string MAPUNLOCK = "MAPUNLOCK";
}