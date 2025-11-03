using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerPrefData
{
    public static int CurLevel
    {
        get => PlayerPrefs.GetInt(ConstUtils.CURLEVEL, 0);
        set => PlayerPrefs.SetInt(ConstUtils.CURLEVEL, value);
    }

    public static int CurLevelUnlock
    {
        get => PlayerPrefs.GetInt(ConstUtils.CURLEVELUNLOCK, 0);
        set => PlayerPrefs.SetInt(ConstUtils.CURLEVELUNLOCK, value);
    }

    public static int Coin
    {
        get => PlayerPrefs.GetInt(ConstUtils.COIN, 10000);
        set => PlayerPrefs.SetInt(ConstUtils.COIN, value);
    }

    public static int CurBackgroundID
    {
        get => PlayerPrefs.GetInt(ConstUtils.CURBGID);
        set => PlayerPrefs.SetInt(ConstUtils.CURBGID, value);
    }

    public static bool IsBGUnlock(int id)
    {
        return PlayerPrefs.GetInt(ConstUtils.BGUNLOCK + id, 0) == 1 ? true : false; 
    }

    public static void SetBGUnlock(int id, bool value)
    {
        PlayerPrefs.SetInt(ConstUtils.BGUNLOCK + id, value ? 1 : 0);
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
    public static string BGUNLOCK = "BGUNLOCK";
    public static string CURBGID = "CURBGID";
    public static string CURLEVELUNLOCK = "CURLEVELUNLOCK";
}