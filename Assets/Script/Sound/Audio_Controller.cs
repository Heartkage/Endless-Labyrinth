using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(AudioManager))]
    public class Audio_Controller : MonoBehaviour
    {

        #region Singleton
        public static Audio_Controller instance;

        void Awake()
        {
            instance = this;
            SetInitialReferences();
        }
        #endregion

        public MusicType menuMusic;
        private AudioManager audioManager;

        void SetInitialReferences()
        {
            audioManager = GetComponent<AudioManager>();
        }


        public void PlayMainBGM()
        {
            audioManager.Play(menuMusic.mainBGM);
        }

        public void StopMainBGM()
        {
            audioManager.Stop(menuMusic.mainBGM);
        }

        public void PlayClick()
        {
            audioManager.Play(menuMusic.clickSound);
        }

        public void PlayPanel()
        {
            audioManager.Play(menuMusic.panelSound);
        }


    }

    [System.Serializable]
    public class MusicType
    {
        public string mainBGM;
        public string clickSound;
        public string panelSound;
    }
}

