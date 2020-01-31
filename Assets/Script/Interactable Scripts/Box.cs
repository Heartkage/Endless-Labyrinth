using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(Animator))]
    public class Box : Interactable
    {

        public ObjectBox boxObject;
        public Item keyForBox;
        public Pickup itemInBox;
        public bool hasLocked;
        public bool isCurrentlyOpened;
        public string boxMessage;

        private Animator animation;
        private bool boxLockState;

        protected override void  Start()
        {
 	        base.Start();
            interactType = GameManager_GlobalVariables.InteractType.Box;
            animation = GetComponent<Animator>();
            boxLockState = hasLocked;
        }

        public override void Interact()
        {
            base.Interact();
            if (boxLockState)
            {
                animation.SetTrigger("Locked");
                Player_Master.instance.CallEventBoxInteract(0, boxObject.sound_DoorLocked);
                StartCoroutine(Player_HudControl.instance.HintText(2.2f, boxObject.objectName));
            }
            else
            {
                StartCoroutine(DisableBoxCollider(2f));
                if (isCurrentlyOpened)
                {
                    if (itemInBox == null)
                    {
                        animation.SetBool("Open", false);
                        Player_Master.instance.CallEventBoxInteract(0, string.Concat(boxObject.sound_DoorClose, " is Locked"));
                        isCurrentlyOpened = !isCurrentlyOpened;
                    }
                    else
                    {
                        itemInBox.Interact();
                        //itemInBox = null;
                    }
                }
                else
                {
                    animation.SetBool("Open", true);
                    Player_Master.instance.CallEventBoxInteract(0, boxObject.sound_DoorOpen);
                    isCurrentlyOpened = !isCurrentlyOpened;
                }
                
            }
        }

        protected override void Update()
        {
            base.Update();

            if (boxLockState)
            {
                if (keyForBox != null)
                {
                    boxMessage = string.Concat("Press [F] to Open");
                    CheckForInventory();
                }
                else
                {
                    boxMessage = string.Concat("This box is broken");
                }
            }
            else
            {
                if (isCurrentlyOpened)
                {
                    if (itemInBox == null)
                    {
                        boxMessage = string.Concat("Press [F] to Close");
                    }
                    else
                    {
                        boxMessage = string.Concat("Press [F] to Pick up");
                    }
                }
                else
                {
                    boxMessage = string.Concat("Press [F] to Open");
                }
            }
        }

        void CheckForInventory()
        {
            if (Player_Inventory.instance.hasItem(keyForBox))
                boxLockState = false;
            else
                boxLockState = true;
        }

        IEnumerator DisableBoxCollider(float time)
        {
            GetComponent<MeshCollider>().enabled = false;

            yield return new WaitForSeconds(time);

            GetComponent<MeshCollider>().enabled = true;
        }

    }
}