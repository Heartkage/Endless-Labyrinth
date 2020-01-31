using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Player_ItemDetection : MonoBehaviour {

        public LayerMask layerToDetect;
        public Transform rayTransformPivot;
        public KeyCode buttonInteract;

        private RaycastHit hit;
        [SerializeField]
        private float detectDistanceRange = 2;
        [SerializeField]
        private float sphereRadius = 2;
        private Player_Master playerMaster;
        private Player_Motor playerMotor;

        void OnEnable()
        {
            SetInitialReferences();
        }
        void OnDisable()
        {
        }

        void SetInitialReferences()
        {
            playerMaster = GetComponent<Player_Master>();
            playerMotor = GetComponent<Player_Motor>();
        }

		void Update ()
		{
            /*if (!playerMaster.stationary)
            {
                RemoveFocus();
            }
            else
            {
                if (!playerMaster.playerInAnimation)
                {
                    InteractableDetection();
                }
            } */

            if (!playerMaster.stationary && (playerMaster.focus != null))
            {
                if (playerMaster.focus.interactType == GameManager_GlobalVariables.InteractType.Ignite)
                    RemoveFocus();
            }
            if (!playerMaster.playerInAnimation)
            {
                InteractableDetection();
            }
            
		}

        
        void InteractableDetection()
        {
            if (Physics.SphereCast(rayTransformPivot.position, sphereRadius, rayTransformPivot.forward, out hit, detectDistanceRange, layerToDetect))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (CheckForInteractable(interactable))
                    {
                        if (Input.GetKeyDown(buttonInteract) && (Time.timeScale > 0))
                        {
                            SetFocus(interactable);

                            if (playerMaster.focus.animationTime > 0)
                                playerMaster.CallEventPlayerInAnimationState(playerMaster.focus.animationTime);

                            GetComponent<Player_HudControl>().InteractKeyDisplay("");
                        }
                    }            
                }
                else
                    Debug.LogWarning("Layer(" + hit.transform.name + ") is set to ItemLayer, but no Interactable script found");
            }
            else
            {                
                RemoveFocus();
            }
        }

        bool CheckForInteractable(Interactable interactable)
        {
            bool canInteract = false;

            if (interactable.interactType == GameManager_GlobalVariables.InteractType.Pickup)
            {
                playerMaster.itemFound = interactable.GetComponent<Pickup>().item;
                GetComponent<Player_HudControl>().InteractKeyDisplay("Press [F] to Pickup");

                canInteract = true;
            }
            else if (interactable.interactType == GameManager_GlobalVariables.InteractType.Ignite)
            {
                if (playerMaster.torchIsOut)
                {
                    GetComponent<Player_HudControl>().InteractKeyDisplay("Press [F] to interact");
                    canInteract = true;
                }
                else
                {
                    GetComponent<Player_HudControl>().InteractKeyDisplay("You need a Torch");
                    canInteract = false;
                }
            }
            else if (interactable.interactType == GameManager_GlobalVariables.InteractType.Door)
            {
                Door doorFound = interactable.gameObject.GetComponent<Door>();
                GetComponent<Player_HudControl>().InteractKeyDisplay(doorFound.doorMessage);
                canInteract = true;
            }
            else if (interactable.interactType == GameManager_GlobalVariables.InteractType.Box)
            {
                Box boxFound = interactable.gameObject.GetComponent<Box>();
                GetComponent<Player_HudControl>().InteractKeyDisplay(boxFound.boxMessage);
                canInteract = true;
            }
            else if (interactable.interactType == GameManager_GlobalVariables.InteractType.Story)
            {
                Story storyFound = interactable.gameObject.GetComponent<Story>();
                GetComponent<Player_HudControl>().InteractKeyDisplay(storyFound.storyMessage);
                canInteract = true;
            }

            return canInteract;
        }

        void SetFocus(Interactable newFocus)
        {
            if (playerMaster.focus != newFocus)
            {
                if (playerMaster.focus != null)
                    playerMaster.focus.OnDefocused();

                playerMaster.focus = newFocus;

                if(playerMaster.focus.hasInteractionPoint)
                    playerMotor.SetInteractionTarget(playerMaster.focus);        
            }

            newFocus.OnFocused(transform);         
        }

        void RemoveFocus()
        {
            if (playerMaster.focus != null)
            {
                playerMaster.focus.OnDefocused();
                playerMaster.focus = null;         
            }

            GetComponent<Player_HudControl>().InteractKeyDisplay("");
            playerMaster.itemFound = null;
            playerMotor.RemoveInteractionPoint();
        }

	}
}

