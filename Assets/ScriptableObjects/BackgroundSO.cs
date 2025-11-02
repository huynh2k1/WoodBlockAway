using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BackgroundSO", fileName = "BackgroundSO")]
public class BackgroundSO : ScriptableObject
{
    public BackgroundData[] data;
}

[Serializable]
public class BackgroundData
{
    public Sprite bgIcon;
    public Sprite bgTexture;
}
