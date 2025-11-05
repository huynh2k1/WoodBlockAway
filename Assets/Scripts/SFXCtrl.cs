using UnityEngine;
using H_Utils;
public class SFXCtrl : BaseSoundCtrl 
{
    public static SFXCtrl I;

    public AudioClip click;
    public AudioClip win;
    public AudioClip woodSuccess;

    private void Awake()
    {
        I = this;
    }

    public override void Initialize()
    {
        base.Initialize();
        StartMusic();
    }

    public override void PlaySound(TypeSound type)
    {
        base.PlaySound(type);
        switch (type)
        {
            case TypeSound.CLICK:
                PlayClip(click);
                break;
            case TypeSound.WIN:
                PlayClip(win);
                break;
            case TypeSound.WOODSUCCESS:
                PlayClip(woodSuccess);
                break;
        }
    }
}
