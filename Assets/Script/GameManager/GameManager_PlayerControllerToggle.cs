using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(GameManager_Master))]
	public class GameManager_PlayerControllerToggle : MonoBehaviour {

        
        [SerializeField]
        private GameObject player;
        private Player_Master playerMaster;
        private NewFirstPersonController playerController;
        private GameManager_Master gameManagerMaster;
		
		void OnEnable(){
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += TogglePlayerController;
            gameManagerMaster.InventoryToggleEvent += TogglePlayerController;
            gameManagerMaster.GameOverEvent += TogglePlayerController;

            playerMaster.PlayerInAnimationState += SetTimerForPlayerController;
		}
		
		void OnDisable(){
            gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
            gameManagerMaster.InventoryToggleEvent -= TogglePlayerController;
            gameManagerMaster.GameOverEvent -= TogglePlayerController;

            playerMaster.PlayerInAnimationState -= SetTimerForPlayerController;
		}
		
		void SetInitialReferences(){
            gameManagerMaster = GetComponent<GameManager_Master>();
            if (player != null)
            {
                playerMaster = player.GetComponent<Player_Master>();
                playerController = player.GetComponent<NewFirstPersonController>();
            }          
            else
                Debug.LogWarning("Player is not assigned on GameManager_PlayerControllerToggle");
		}

        void TogglePlayerController()
        {
            if (playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
            else
            {
                Debug.LogWarning("PlayerController not assigned");
            }
        }

        IEnumerator SetTimerForPlayerController(float time)
        {
            playerMaster.playerInAnimation = true;
            //playerController.enabled = false;

            yield return new WaitForSeconds(time);

            //playerController.enabled = true;
            playerMaster.playerInAnimation = false;
        }
	}
}

