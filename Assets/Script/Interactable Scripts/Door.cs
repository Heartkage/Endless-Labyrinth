using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(Animator))]
	public class Door : Interactable{

        public ObjectDoor doorObject;
        public Item keyForDoor;
        public bool hasLocked;  
        public bool isCurrentlyOpened;          
        public string doorMessage;

        private Animator animation;
        private bool doorLockState;

        void Start()
        {
            interactType = GameManager_GlobalVariables.InteractType.Door;
            animation = GetComponent<Animator>();
            doorLockState = hasLocked;    

        }

        public override void Interact()
        {
            base.Interact();
            if (doorLockState)
            {
                animation.SetTrigger("Locked");
                Player_Master.instance.CallEventDoorInteract(0, doorObject.sound_DoorLocked);
                StartCoroutine(Player_HudControl.instance.HintText(2.2f, string.Concat(doorObject.objectName, " is Locked")));
            }               
            else
            {
                StartCoroutine(DisableDoorCollider(2f));
                if (isCurrentlyOpened)
                {
                    animation.SetBool("Open", false);
                    Player_Master.instance.CallEventDoorInteract(0, doorObject.sound_DoorClose);
                }
                else
                {
                    animation.SetBool("Open", true);
                    Player_Master.instance.CallEventDoorInteract(0, doorObject.sound_DoorOpen);
                }
                    

                isCurrentlyOpened = !isCurrentlyOpened;
            }
        }

        protected override void Update()
        {
 	        base.Update();

            if (doorLockState)
            {
                if (keyForDoor != null)
                {
                    doorMessage = string.Concat("Press [F] to Open");
                    CheckForInventory();               
                }
                else
                {
                    doorMessage = string.Concat("This door is broken");
                }     
            }
            else
            {
                if (isCurrentlyOpened)
                {
                    doorMessage = string.Concat("Press [F] to Close");
                }
                else
                {
                    doorMessage = string.Concat("Press [F] to Open");
                }
            }
        }

        void CheckForInventory()
        {
            if (Player_Inventory.instance.hasItem(keyForDoor))            
                doorLockState = false;         
            else
                doorLockState = true;
        }

        IEnumerator DisableDoorCollider(float time)
        {
            GetComponent<MeshCollider>().enabled = false;

            yield return new WaitForSeconds(time);

            GetComponent<MeshCollider>().enabled = true;
        }
       
	}
}

