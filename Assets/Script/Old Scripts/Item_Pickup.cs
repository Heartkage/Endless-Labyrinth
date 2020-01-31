using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Item_Pickup : MonoBehaviour {

        /*
        [SerializeField]
        private bool shouldBeDestroyed = true;
        [SerializeField]
        GameManager_GlobalVariables.ItemName itemName;

        [HideInInspector]
        public int triggerState;
        private Player_Master playerMaster;
        
	
		void OnEnable()
		{
            SetInitialReferences();          
		}
		
		void OnDisable()
		{
            playerMaster.PickUpAnimationEvent -= DestroyItem;
		}

        void SetInitialReferences()
        {
            playerMaster = GameObject.Find("PlayerMasterController").GetComponent<Player_Master>();
            triggerState = 0;
        }
		
		public void AddEvent()
        {
            if (shouldBeDestroyed)
                playerMaster.PickUpAnimationEvent += DestroyItem;
        }

        public void DeleteEvent()
        {
            if (shouldBeDestroyed)
                playerMaster.PickUpAnimationEvent -= DestroyItem;
        }

        void DestroyItem()
        {
            if (playerMaster.itemLists.AddItem(GameManager_GlobalVariables._itemsName[itemName]))
            {
                playerMaster.itemPickedUp = GameManager_GlobalVariables._itemsName[itemName];
                Destroy(gameObject, 0.7f);  
            }                 
            else
                Debug.LogWarning("There is no such item in itemlist");
        }*/
	}
}

