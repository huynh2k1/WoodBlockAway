using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameCtrl : MonoBehaviour
{
    public abstract void Home();
    public abstract void PlayGame();
    public abstract void WinGame();
    public abstract void ReplayGame();

}
