using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace gm
{
	public class Player_Behavior : MonoBehaviour {

        public GameObject monster;

        private Animator anime;
        private bool walk;
        private bool run;
        private Player_Master playerMaster;
        private NewFirstPersonController firstPersonController;

        private int damageAmount;
        public float endurableTimeInAir = 0.8f;

        public bool activeTestingInput = false;

        void OnEnable()
        {
            SetInitialReferences();
            //playerMaster.PickUpAnimationEvent += TogglePickupAnimation;
            //playerMaster.TorchAnimationEvent += ToggleIgniteAnimation;

            playerMaster.TriggerAnimationEvent += TriggerAnimation;
        }

        void OnDisable()
        {
            //playerMaster.PickUpAnimationEvent -= TogglePickupAnimation;
            //playerMaster.TorchAnimationEvent -= ToggleIgniteAnimation;

            playerMaster.TriggerAnimationEvent -= TriggerAnimation;
        }

        void SetInitialReferences()
        {
            anime = monster.GetComponent<Animator>();
            playerMaster = GetComponent<Player_Master>();
            firstPersonController = GetComponent<NewFirstPersonController>();
            walk = false;
            run = false;
        }

        void Update()
        {
            if (!GameManager_Master.instance.gamePause)
            {
                if (playerMaster.playerInAnimation)
                {

                }
                else
                {
                    KeyboardInput();
                    MouseInput();
                    if (firstPersonController.backToGround)
                        CalculatingFallingDamage();
                }        
            }           
        }

        void TriggerAnimation(string animation)
        {
            anime.SetTrigger(animation);
        }

        void TogglePickupAnimation()
        {
            anime.SetTrigger("Pickup");
        }
        
        void ToggleIgniteAnimation()
        {
            anime.SetTrigger("Ignite");
        }

        void KeyboardInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                walk = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    run = true;    
                }
                else
                {
                    run = false;
                }

                playerMaster.stationary = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                walk = true;
                playerMaster.stationary = false;
            }
            else
            {
                walk = false;
                playerMaster.stationary = true;
            }
            anime.SetBool("Walk", walk);
            anime.SetBool("Run", run);

            if (activeTestingInput)
            {
                //---Test---//
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    playerMaster.CallEventReceivedDamage(99);
                }
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    GameManager_SceneController.instance.SaveCurrentProgress();
                }
            }  
        }

        void MouseInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                playerMaster.CallEventTorchToggle();
                anime.SetBool("Torch", playerMaster.torchIsOut);
            }
        }

        void CalculatingFallingDamage()
        {
            if (firstPersonController.timeInAir > endurableTimeInAir)
            {
                float x = firstPersonController.timeInAir / endurableTimeInAir;
                damageAmount = (int)((Mathf.Pow(x, 1.4f) - 1) * 100);

                Debug.Log("Damage Taken: " + damageAmount);

                playerMaster.CallEventReceivedDamage(damageAmount);
                firstPersonController.backToGround = false;
            }
        }
    }
}

