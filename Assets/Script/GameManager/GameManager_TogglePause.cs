using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class GameManager_TogglePause : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        private bool isPaused;

        void Start()
        {
            Time.timeScale = 1;
        }
		
		void OnEnable(){
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += TogglePause;
            gameManagerMaster.InventoryToggleEvent += TogglePause;
            gameManagerMaster.GameOverEvent += TogglePause;
		}
		
		void OnDisable(){
            gameManagerMaster.MenuToggleEvent -= TogglePause;
            gameManagerMaster.InventoryToggleEvent -= TogglePause;
            gameManagerMaster.GameOverEvent -= TogglePause;
		}
		
		void SetInitialReferences(){
            gameManagerMaster = GetComponent<GameManager_Master>();
            isPaused = false;
		}

        void TogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
	}
}

