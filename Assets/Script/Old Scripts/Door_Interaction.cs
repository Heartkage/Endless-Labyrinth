using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Door_Interaction : MonoBehaviour {

        /*
        private Player_Master playerMaster;
        private Transform initialTransform;
        private Transform currentTransform;

        private float timeCounter;

        // Not Use
        [HideInInspector]
        public int doorState;

        [SerializeField]
        private float doorSpeedFactor = 1.8f;

        public bool isDoorOpen = false;
        public bool frontOpen = true;
        [HideInInspector]
        public bool isDoorLocked = true;

        [SerializeField]
        GameManager_GlobalVariables.DoorType doorList;
        private string doorKeyName;

        private bool animeTransition;
  
		void Update ()
		{
            if (isDoorLocked)
            {
                CheckKeyOwned();
            }

            if (animeTransition)
            {
                GetComponent<MeshCollider>().enabled = false;
                DoorAnimation();
            }
            else
            {             
                GetComponent<MeshCollider>().enabled = true;
            }               
		}

		void OnEnable()
		{
            SetInitialReferences(); 

            timeCounter = 0f;          
            isDoorLocked = true;            
		}
		
		void OnDisable()
		{
            
		}
		
		void SetInitialReferences()
		{
            playerMaster = GameObject.Find("PlayerMasterController").GetComponent<Player_Master>();
            initialTransform = GetComponent<Transform>();
            currentTransform = initialTransform;       
		}

        void CheckKeyOwned()
        {
            if (doorList == GameManager_GlobalVariables.DoorType.Open)
            {
                isDoorLocked = false;
            }
            else if (doorList == GameManager_GlobalVariables.DoorType.Locked)
            {
                isDoorLocked = true;
            }
            else
            {
                doorKeyName = GameManager_GlobalVariables._doorsName[doorList];
                bool owned = playerMaster.itemLists.IsItemInInventory(doorKeyName);

                if (owned)
                {
                    isDoorLocked = false;
                }
            }
        }

        public void AddEvent()
        {
            if (!isDoorLocked)
            {
                playerMaster.DoorOpenEvent += ToggleDoorState;
                playerMaster.DoorCloseEvent += ToggleDoorState;
            }
            else
            {

            }
        }

        public void DeleteEvent()
        {
            if (!isDoorLocked)
            {
                playerMaster.DoorOpenEvent -= ToggleDoorState;
                playerMaster.DoorCloseEvent -= ToggleDoorState;
            }
            else
            {

            }
        }

        void ToggleDoorState()
        {
            isDoorOpen = !isDoorOpen;
            animeTransition = true;
        }

        void DoorAnimation()
        {
            timeCounter += Time.deltaTime;

            if (timeCounter > doorSpeedFactor)
            {
                timeCounter = 0f;
                animeTransition = false;
            }
            else
            {
                float angle;
                if (isDoorOpen)
                    angle = 80 * (Time.deltaTime / doorSpeedFactor);
                else
                    angle = -80 * (Time.deltaTime / doorSpeedFactor);

                if (!frontOpen)
                    angle *= -1;

                currentTransform.Rotate(0, angle, 0, Space.Self);
            }
        }

        */
    }
}

