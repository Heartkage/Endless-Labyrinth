using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    public class Enemy_Sound : MonoBehaviour
    {
        private AudioManager audioManager;

        void SetInitialReferences()
        {
            audioManager = AudioManager.instance;
        }

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        public void PlaySlashSound()
        {
            audioManager.Play("Knife_Slash");
        }
    }
}

