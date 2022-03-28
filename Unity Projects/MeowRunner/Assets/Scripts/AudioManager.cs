using System;
using UnityEngine;

namespace Utility
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] soundList;
        public Sound[] musicList;

        public static AudioManager Instance;

        private string _playingMusic;
        private bool _isAudioOn = true;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        
            DontDestroyOnLoad(gameObject);
        
            foreach (var sound in soundList)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.AudioClip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }

            foreach (var music in musicList)
            {
                music.source = gameObject.AddComponent<AudioSource>();
                music.source.clip = music.AudioClip;
                music.source.volume = music.volume;
                music.source.pitch = music.pitch;
                music.source.loop = music.loop;
            }
        }
        
    
        public void PlayMusic(string music)
        {
            Sound s = Array.Find(musicList, sound => sound.name == music);
            if (s == null)
            {
                return;
            }
            
            if (IsAudioOn)
            {
                s.source.Play();
            }
            else
            {
                StopAll();
            }

        }

        public void PlaySound(string soundName)
        {
            Sound s = Array.Find(soundList, sound => sound.name == soundName);
            if (s == null)
            {
                return;
            }

            if (IsAudioOn)
            {
                s.source.Play();
            }
        }
        
        public void PlaySoundwPitch(string soundName, float pitch)
        {
            Sound s = Array.Find(soundList, sound => sound.name == soundName);
            if (s == null)
            {
                return;
            }

            if (IsAudioOn)
            {
                s.source.pitch = pitch;
                s.source.PlayOneShot(s.AudioClip);
            }
        }

        public void StopAll()
        {
            foreach (var s in musicList)
            {
                s.source.Stop();
            }
        }

        public bool IsAudioOn
        {
            get => _isAudioOn;
            set => _isAudioOn = value;
        }
    }
}
