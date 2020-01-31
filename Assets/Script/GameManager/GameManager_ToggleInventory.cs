using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(GameManager_Master))]
	public class GameManager_ToggleInventory : MonoBehaviour {

        public GameObject inventoryObject;
        private GameManager_Master gameManagerMaster;

		void Start ()
		{
            SetInitialReferences();
            
		}

		void Update ()
		{
            CheckForInput();
		}
		
		void SetInitialReferences()
		{
            gameManagerMaster = GetComponent<GameManager_Master>();
            if (inventoryObject == null)
                Debug.LogWarning("Inventory Object not found!");
            else
                inventoryObject.SetActive(false);
		}

        void CheckForInput()
        {
            if (inventoryObject != null)
            {
                if (Input.GetKeyUp(KeyCode.I) && !gameManagerMaster.isGameOver && !gameManagerMaster.isMenuOn && !gameManagerMaster.isStoryOn)
                {
                    ToggleInventory();
                }
            }
            else
            {
                Debug.LogWarning("Inventory Object not found!");
            }
        }

        public void ToggleInventory()
        {
            inventoryObject.SetActive(!inventoryObject.activeSelf);

            if (inventoryObject.activeSelf)
                Player_Inventory.instance.UpdateInventoryUI();

            gameManagerMaster.isInventoryOn = !gameManagerMaster.isInventoryOn;
            gameManagerMaster.gamePause = !gameManagerMaster.gamePause;
            gameManagerMaster.CallEventInventoryToggle();
        }
	}
}

