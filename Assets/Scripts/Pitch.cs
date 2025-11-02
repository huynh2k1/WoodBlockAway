using UnityEngine;

public class Pitch : MonoBehaviour
{
    public GameObject pitchModel;
    public ParticleSystem _effect;

    public void PitchEffect()
    {
        pitchModel.SetActive(false);
        _effect.Play();
    }
}
