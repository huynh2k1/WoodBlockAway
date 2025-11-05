using System.Collections.Generic;
using UnityEngine;

namespace H_Utils
{
    public class BaseSoundCtrl : MonoBehaviour
    {
        [Header("Music")]
        public AudioSource music_source;
        public AudioClip music_sound;

        [Header("Sound")]
        public AudioSource[] sound_sources;
        private Queue<AudioSource> queue_sources;


        private void Start()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            queue_sources = new Queue<AudioSource>(sound_sources);
            UpdateVolume();
        }

        public void UpdateVolume()
        {
            OnChangeVolumnSounds();
            OnChangeVolumnMusic();
        }

        public virtual void OnChangeVolumnSounds()
        {
            foreach (var sound in sound_sources)
            {
                sound.volume = PlayerPrefData.Sound;
            }
        }

        public virtual void OnChangeVolumnMusic()
        {
            music_source.volume = PlayerPrefData.Music;
        }


        public virtual void PlaySound(TypeSound type)
        {
        }



        public virtual void PlayClip(AudioClip clip)
        {
            var source = queue_sources.Dequeue();
            if (source == null)
                return;
            source.volume = PlayerPrefData.Sound;
            source.PlayOneShot(clip);
            queue_sources.Enqueue(source);
        }

        public virtual void StartMusic()
        {
            music_source.clip = music_sound;
            music_source.Play();
        }

        public void StopMusic()
        {
            music_source.Stop();
        }
    }
    
}
public enum TypeSound
{
    CLICK,
    WIN,
    WOODSUCCESS,
}