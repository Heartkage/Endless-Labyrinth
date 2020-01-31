using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Interactable : MonoBehaviour {

        public bool hasInteractionPoint = false;
        public float interactionRadius = 0.3f;
        public Transform interactionTransform;
        public Transform targetTransform;

        public float animationTime = 0;
        
        private bool readyForInteract = false;
        private bool hasInteracted;
        private Transform playerTransform;

        [HideInInspector]
        public GameManager_GlobalVariables.InteractType interactType = GameManager_GlobalVariables.InteractType.None;

        public virtual void Interact()
        {
            Debug.Log("Interacting with " + transform.name + "...");
        }

        protected virtual void Start()
        {
            if(targetTransform == null)
                targetTransform = transform;
        }

        protected virtual void Update()
        {
            if (readyForInteract && !hasInteracted)
            {
                if (hasInteractionPoint)
                {
                    Vector3 sameY_Position = new Vector3(interactionTransform.position.x, playerTransform.position.y, interactionTransform.position.z);
                    //Debug.Log(Vector3.Distance(correctPosition, playerTransform.position));
                    if (Vector3.Distance(sameY_Position, playerTransform.position) <= interactionRadius)
                    {
                        Interact();
                        hasInteracted = true;
                    }
                }
                else
                {
                    Interact();
                    hasInteracted = true;
                }
            }

        }

        public void OnFocused(Transform target)
        {
            playerTransform = target;
            readyForInteract = true;
            hasInteracted = false;
        }

        public void OnDefocused()
        {
            playerTransform = null;
            readyForInteract = false;
            hasInteracted = false;
        }

        void OnDrawGizmosSelected()
        {
            if (interactionTransform == null)
                interactionTransform = transform;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(interactionTransform.position, interactionRadius);
        }
	}
}

