using System;
using UnityEngine;

public class BackgroundSO : MonoBehaviour
{
    public BackgroundData[] data;
}

[Serializable]
public class BackgroundData
{
    public Sprite bgIcon;
    public Sprite bgTexture;
}
