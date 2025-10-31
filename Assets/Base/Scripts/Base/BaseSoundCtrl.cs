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
            UpdateVolumeSounds();
            UpdateVolumeMusic();
        }

        public virtual void UpdateVolumeSounds()
        {
            foreach (var sound in sound_sources)
            {
                //sound.volume = PrefData.Sound;
            }
        }

        public virtual void UpdateVolumeMusic()
        {
            //music_source.volume = PrefData.Music;
        }


        public virtual void PlaySoundByType(TypeSound type)
        {
        }



        public virtual void PlaySound(AudioClip clip)
        {
            var source = queue_sources.Dequeue();
            if (source == null)
                return;
            //source.volume = PrefData.Sound;
            source.PlayOneShot(clip);
            queue_sources.Enqueue(source);
        }

        public virtual void PlayMusic(float volume = 0.7f)
        {
            music_source.clip = music_sound;
            //music_source.volume = PrefData.Music;
            music_source.Play();
        }

        public void StopMusic()
        {
            music_source.Stop();
        }
    }
    public enum TypeSound
    {
    
    }
}