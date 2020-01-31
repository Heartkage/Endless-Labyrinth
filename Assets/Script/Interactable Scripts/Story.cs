using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    public class Story : Interactable
    {

        public ObjectStory storyObject;
        public string storyMessage;
        private bool storyOpen; 
        public GameObject storyCanvas;
       
        void Start()
        {
            interactType = GameManager_GlobalVariables.InteractType.Story;
            storyOpen = false;
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            if (storyCanvas == null)
                Debug.LogWarning("Story not found!");
            else
                storyCanvas.SetActive(false);
        }

        public override void Interact()
        {
            base.Interact();

            if (storyCanvas == null) return;
            storyCanvas.SetActive(!storyCanvas.activeSelf);
            UI_Story.instance.SetText(storyObject);
            storyOpen = storyCanvas.activeSelf;
            GameManager_Master.instance.isStoryOn = storyOpen;
        }

        public void CloseStory()
        {
            if (storyCanvas == null) return;
            storyCanvas.SetActive(false);
            storyOpen = false;
            GameManager_Master.instance.isStoryOn = false;
        }

        protected override void Update()
        {
            base.Update();

            if (storyCanvas == null) return;
            if (!Player_Master.instance.stationary && storyCanvas.activeSelf)
            {
                CloseStory();
            }

            storyOpen = storyCanvas.activeSelf;
            if (storyOpen)
            {
                storyMessage = string.Concat("Press [F] to Close");
            }
            else
            {
                storyMessage = string.Concat("Press [F] to Read");
            }
        }
    }
}