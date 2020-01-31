using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
    [RequireComponent(typeof(Animator))]
	public class UI_DialogEvent : MonoBehaviour
    {

        #region Singleton
        public static UI_DialogEvent instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        private Animator animation;
        public Text dialogText;

        void Start()
        {
            SetupInitialReferences();
        }

        void SetupInitialReferences()
        {
            animation = GetComponent<Animator>();
        }

        public void ShowDialog(string words)
        {
            dialogText.text = words;
            animation.SetTrigger("ShowDialog");
        }
	}
}

