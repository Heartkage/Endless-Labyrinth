using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Player_Master : MonoBehaviour
    {

        #region Singleton
        public static Player_Master instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        public Interactable focus;
        public Item itemFound;

        [HideInInspector]
        public bool torchIsOut;
        public bool stationary;

        public float health;
        public bool playerInAnimation;
        public bool playerIsHaunted;

        public delegate void PlayerEventHandler();
        //Player and Object Interaction
        public event PlayerEventHandler TorchToggleEvent;


        //Player Behavior
        public event PlayerEventHandler ItemDetectionEvent;
        public event PlayerEventHandler HudUpdateEvent;

        public event PlayerEventHandler DeathEvent;

        public event PlayerEventHandler HauntedStateEvent;
        public event PlayerEventHandler RecoveringStateEvent;

        public delegate void DamageEventHandler(int amount);
        public event DamageEventHandler ReceivedDamageEvent;

        public delegate void SetAnimationEvent(string animationName);
        public event SetAnimationEvent TriggerAnimationEvent;

        public delegate IEnumerator DelayEventHandler(float time);
        public event DelayEventHandler TorchIgniteEvent;
        public event DelayEventHandler PlayerInAnimationState;

        public delegate IEnumerator DelayEventWithName(float time, string name);
        public event DelayEventWithName ItemPickUpEvent;
        public event DelayEventWithName DoorInteractEvent;
        public event DelayEventWithName BoxInteractEvent;
        public event DelayEventWithName StoryInteractEvent;
        
        //---Item Detection---//
        public void CallEventItemDetection()
        {
            if (ItemDetectionEvent != null)
            {
                ItemDetectionEvent();
            }
        }

        //---Update Hud Infomation---//
        public void CallEventHudUpdate()
        {
            if (HudUpdateEvent != null)
            {
                HudUpdateEvent();
            }
        }

        public void CallEventDeath()
        {
            if (DeathEvent != null)
                DeathEvent();
        }
        
        //---Player Behavior event---//  
        public void CallEventHauntedState()
        {
            if (HauntedStateEvent != null)
                HauntedStateEvent();
        }

        public void CallEventRecoveringState()
        {
            if (RecoveringStateEvent != null)
                RecoveringStateEvent();
        }

        //---Item Related CallEvent---//
        public void CallEventTorchToggle()
        {
            if (TorchToggleEvent != null)
                TorchToggleEvent();
        }

        public void CallEventTriggerAnimation(string animationName)
        {
            if (TriggerAnimationEvent != null)
                TriggerAnimationEvent(animationName);
        }


        //------Health Control------//
        public void CallEventReceivedDamage(int amount)
        {
            if (ReceivedDamageEvent != null)
                ReceivedDamageEvent(amount);
        }


        public void CallEventItemPickUp(float time, string name)
        {
            if (ItemPickUpEvent != null)
            {
                foreach (DelayEventWithName handler in ItemPickUpEvent.GetInvocationList())
                    StartCoroutine(handler.Invoke(time, name));
            }
        }

        public void CallEventDoorInteract(float time, string name)
        {
            if (DoorInteractEvent != null)
            {
                foreach (DelayEventWithName handler in DoorInteractEvent.GetInvocationList())
                    StartCoroutine(handler.Invoke(time, name));
            }
        }

        public void CallEventBoxInteract(float time, string name)
        {
            if (BoxInteractEvent != null)
            {
                foreach (DelayEventWithName handler in BoxInteractEvent.GetInvocationList())
                    StartCoroutine(handler.Invoke(time, name));
            }
        }

        public void CallEventStoryInteract(float time, string name)
        {
            if (StoryInteractEvent != null)
            {
                foreach (DelayEventWithName handler in StoryInteractEvent.GetInvocationList())
                    StartCoroutine(handler.Invoke(time, name));
            }
        }

        public void CallEventTorchIgnite(float time)
        {
            if (TorchIgniteEvent != null)
            {
                foreach(DelayEventHandler handler in TorchIgniteEvent.GetInvocationList())
                    StartCoroutine(handler.Invoke(time));   
            }       
        }

        public void CallEventPlayerInAnimationState(float time)
        {
            if (PlayerInAnimationState != null)
            {
                foreach (DelayEventHandler handler in PlayerInAnimationState.GetInvocationList())
                    StartCoroutine(handler.Invoke(time));
            }
        }    

        void Start()
        {
            health = 100;
            stationary = true;
            playerInAnimation = false;
            playerIsHaunted = false;
        }

	}
}

