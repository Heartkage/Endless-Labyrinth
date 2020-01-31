using UnityEngine.Audio;
using UnityEngine;
using System;

namespace gm
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Sound[] sounds;

        private float lastVolume = 1f;
        

        // Use this for initialization
        void Awake()
        {
            instance = this;
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.volume = 0;
                s.source.volume = s.baseVolume;        
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.addedVolume = 0f;
            }
        }

        public void AdjustAllVolume(float value)
        {

            float realValue = (value - lastVolume);
            lastVolume = value;

            //Debug.Log(realValue);
            foreach (Sound s in sounds)
            {
                s.volume += realValue * s.rate;
                s.source.volume = s.baseVolume + s.volume;
                if (s.source.volume <= 0.00001f)
                    s.source.volume = 0;
                
                //Debug.Log(realValue * s.rate);
            }     
        }

        public void SetVolume(string name, int amount)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.addedVolume = (((float)amount / 100f) * s.rate);
            s.source.volume = (s.baseVolume + s.volume) / s.baseVolume * s.addedVolume;
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }

        public void Mute(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.mute = true;
        }

        public void UnMute(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.mute = false;
        }

        public void Pause(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Pause();
        }

        public void UnPause(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.UnPause();
        }

        public void PauseAll()
        {
            foreach (Sound s in sounds)
            {
                s.source.Pause();
            }
        }

        public void UnPauseAll()
        {
            foreach (Sound s in sounds)
            {
                s.source.UnPause();
            }
        }

        public void StopAllSounds()
        {
            foreach (Sound s in sounds)
            {
                s.source.Stop();
            }
        }

        public bool IsPlaying(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            return s.source.isPlaying;
        }
    }
}

