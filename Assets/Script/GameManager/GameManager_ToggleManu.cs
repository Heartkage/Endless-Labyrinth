using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class GameManager_ToggleManu : MonoBehaviour {

        [SerializeField]
        private GameObject manu;
        [SerializeField]
        private GameObject mainPanel;
        [SerializeField]
        private GameObject settingPanel;
        private GameManager_Master gameManagerMaster;
        public KeyCode buttonForMenu;
		
		// Update is called once per frame
		void Update ()
        {
            checkForManuInput();
		}
		
		void OnEnable()
        {
            SetInitialReferences();
            manu.SetActive(false);
		}
		
		void OnDisable()
        {
		}
		
		void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
		}

        void checkForManuInput()
        {
            if (manu != null)
            {
                if (Input.GetKeyUp(buttonForMenu) && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryOn && !gameManagerMaster.isStoryOn)
                {
                    ToggleManu();
                }
            }
            else
            {
                Debug.LogWarning("Manu Object not found!");
            }
        }

        public void ToggleManu()
        {
            manu.SetActive(!manu.activeSelf);
            mainPanel.SetActive(true);
            settingPanel.SetActive(false);
            gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
            gameManagerMaster.gamePause = !gameManagerMaster.gamePause;
            gameManagerMaster.CallEventMenuToggle();  
        }
	}
}

