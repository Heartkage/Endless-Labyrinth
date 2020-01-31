using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gm
{
	public class GameManager_Master : MonoBehaviour
    {

        #region Singleton
        public static GameManager_Master instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        public delegate void GameManagerEventHandler();

        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler InventoryToggleEvent;
        public event GameManagerEventHandler RestartEvent;
        public event GameManagerEventHandler GotoMainManuEvent;
        public event GameManagerEventHandler GameOverEvent;
        public event GameManagerEventHandler StoryEvent;

        public bool isMenuOn;
        public bool isInventoryOn;
        public bool isGameOver;
        public bool isStoryOn;

        public bool gamePause;

        public int currentBuildIndex;

        [SerializeField]
        private GameObject testlight;
        private void Start()
        {
            if(testlight != null)
                testlight.SetActive(false);
            
            isGameOver = false;
            isMenuOn = false;
            isInventoryOn = false;
            gamePause = false;
            isStoryOn = false;

            currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void CallEventMenuToggle()
        {
            if (MenuToggleEvent != null)
            {
                MenuToggleEvent();
            }
        }

        public void CallEventInventoryToggle()
        {
            if (InventoryToggleEvent != null)
            {
                InventoryToggleEvent();
            }
        }

        public void CallEventRestart()
        {
            if (RestartEvent != null)
            {
                RestartEvent();
            }
        }

        public void CallEventGotoMainManu()
        {
            if (GotoMainManuEvent != null)
            {
                GotoMainManuEvent();
            }
        }

        public void CallEventGameOver()
        {
            if (GameOverEvent != null)
            {
                isGameOver = true;
                GameOverEvent();
            }
        }

        public void CallEventStory()
        {
            if (StoryEvent != null)
            {
                StoryEvent();
            }
        }
	}
}

