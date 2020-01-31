using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace gm
{
    public class TorchBehavior : MonoBehaviour
    {
        ParticleSystem ps;
        AnimationCurve curve;
        //AnimationCurve curveForEmit;
        public float energy;
        public float reduceRate;
        public bool IsIgnited;
        public Item torch;
        public GameObject torch_GameObject;
        [SerializeField]
        private GameObject flame;

        private float timeInterval;
        private Player_Master playerMaster;

        
        void OnEnable()
        {
            SetInitialReference();
            ResetTorchValue();
            playerMaster.TorchToggleEvent += ToggleTorch;
            playerMaster.TorchIgniteEvent += LightUpTorch;
        }

        void OnDisable()
        {
            playerMaster.TorchToggleEvent -= ToggleTorch;
            playerMaster.TorchIgniteEvent -= LightUpTorch;
        }

        void SetInitialReference()
        {
            ps = flame.GetComponent<ParticleSystem>();
            playerMaster = GetComponent<Player_Master>();
            torch_GameObject.SetActive(false);
        }

        void LateUpdate()
        {

            if (IsIgnited && torch_GameObject.activeSelf && !GameManager_Master.instance.gamePause)
            {
                ReduceEnergy();
            }
            CheckEnergy();
            EmissionControl();
         
        }

       

        IEnumerator LightUpTorch(float time)
        {
            if (torch_GameObject.activeSelf)
            {
                yield return new WaitForSeconds(time);
                Debug.Log("Torch is lighted up!");
                energy = 100.0f;
                IsIgnited = true;
                timeInterval = 0.0f;
                if (!flame.activeSelf)
                    flame.SetActive(true);
                if(!ps.isPlaying)
                    ps.Play();
                //light.SetActive(true);
            }
        }

        void ToggleTorch()
        {
            
            //if (playerMaster.itemLists.IsItemInInventory(GameManager_GlobalVariables._torch))
            if (Player_Inventory.instance.hasItem(torch))
            {
                playerMaster.torchIsOut = !playerMaster.torchIsOut;
                if (playerMaster.torchIsOut)
                {
                    torch_GameObject.SetActive(true);
                    AudioManager.instance.UnPause("Torch_Flame");

                    if (IsIgnited && !AudioManager.instance.IsPlaying("Torch_Flame"))
                    {
                        //Debug.Log("Toggle Torch");
                        AudioManager.instance.Play("Torch_Flame");
                    }   
                }
                else
                {
                    AudioManager.instance.Pause("Torch_Flame");
                    torch_GameObject.SetActive(false);
                }
            }
        }

        /*void FlameMovementControl()
        {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * 2f; // factor 2
            //float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * 2f;
            var fo = ps.forceOverLifetime;
            fo.enabled = true;

            if (yRot < -0.05)
            {
                curve = new AnimationCurve();
                curve.AddKey(0.0f, 2.0f);
                curve.AddKey(1.0f, 2.3f);
                fo.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
            }
            else if (yRot > 0.05)
            {
                curve = new AnimationCurve();
                curve.AddKey(0.0f, -2.0f);
                curve.AddKey(1.0f, -2.3f);
                fo.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
            }
            else
            {
                curve = new AnimationCurve();
                curve.AddKey(0.0f, 0.0f);
                fo.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
            }
        }*/

        void ResetTorchValue()
        {
            if (SystemSaveAndLoad.sceneLoadType == SystemSaveAndLoad.LoadType.newGame)
            {
                energy = 0;
                IsIgnited = false;
            }
            else
                IsIgnited = true;


            timeInterval = 0;
        }

        void ReduceEnergy()
        {
            timeInterval += Time.deltaTime;
            // Reduce by second
            if (timeInterval > 1)
            {
                timeInterval = 0.0f;
                energy -= reduceRate;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    energy -= reduceRate*2;
                } 
            }
        }

        void CheckEnergy()
        {
            if (energy <= 0)
            {
                energy = 0;
                IsIgnited = false;
            }
            else
                IsIgnited = true;
        }

        void EmissionControl()
        {
            if (IsIgnited)
            {
                if (!flame.activeSelf)
                    flame.SetActive(true);

                if (!GameManager_Master.instance.gamePause)
                {
                    if (!ps.isPlaying || ps.isStopped)
                    {
                        ps.Play();
                    }
                }
                else
                {
                    if (ps.isPlaying)
                    {
                        //ps.Clear();
                        ps.Pause();
                    }
                }
            }
            else
            {
                if (flame.activeSelf)
                {
                    if (ps.isPlaying)
                    {
                        ps.Clear();
                        ps.Pause();
                        flame.SetActive(false);
                    }

                }
            }
        }

    }
}

