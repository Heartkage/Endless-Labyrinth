using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Sound_Controller : MonoBehaviour {

        [SerializeField]
        private GameObject audioObject;
        [SerializeField]
        private GameObject game;

        private GameManager_Master gameManagerMaster;
        private AudioManager audioManager;
        private Player_Master playerMaster;
        private TorchBehavior torchMaster;

        private bool soundOn;

        void Start()
        {
            audioManager.Play("Normal_Environment");
        }

        void LateUpdate()
        {
            if (soundOn)
            {
                CheckIgnition();
                CheckChasingState();
            }
        }

		void OnEnable()
		{
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += PauseAllForGameStop;
            gameManagerMaster.InventoryToggleEvent += PauseAllForGameStop;
            gameManagerMaster.GameOverEvent += PauseAllForDeath;

            playerMaster.ReceivedDamageEvent += Minotaur_Hurt;
            playerMaster.RecoveringStateEvent += Breathing;

            playerMaster.TorchIgniteEvent += TorchSoundPlay;

            playerMaster.ItemPickUpEvent += InteractionSound;
            playerMaster.DoorInteractEvent += InteractionSound;
		}
		
		void OnDisable()
		{
            gameManagerMaster.MenuToggleEvent -= PauseAllForGameStop;
            gameManagerMaster.InventoryToggleEvent -= PauseAllForGameStop;
            gameManagerMaster.GameOverEvent -= PauseAllForDeath;

            playerMaster.ReceivedDamageEvent -= Minotaur_Hurt;
            playerMaster.RecoveringStateEvent -= Breathing;

            playerMaster.TorchIgniteEvent -= TorchSoundPlay;

            playerMaster.ItemPickUpEvent -= InteractionSound;
            playerMaster.DoorInteractEvent -= InteractionSound;
		}
		
		void SetInitialReferences()
		{
            if (audioObject != null)
                audioManager = audioObject.GetComponent<AudioManager>();
            else
                Debug.LogWarning("Please attach audioManager Gameobject");

            if (game != null)
                gameManagerMaster = game.GetComponent<GameManager_Master>();
            else
                Debug.LogWarning("Please attach GameManager Gameobject");

            playerMaster = GetComponent<Player_Master>();
            torchMaster = GetComponent<TorchBehavior>();
            soundOn = true;
		}

        void CheckIgnition()
        {
            if (!torchMaster.IsIgnited)
                audioManager.Stop("Torch_Flame");
        }

        void CheckChasingState()
        {
            if (playerMaster.playerIsHaunted)
            {
                if (audioManager.IsPlaying("Normal_Environment"))
                    audioManager.Stop("Normal_Environment");

                if (!audioManager.IsPlaying("Horror_Chase"))
                    audioManager.Play("Horror_Chase");            
            }
            else
            {
                if (audioManager.IsPlaying("Horror_Chase"))
                    audioManager.Stop("Horror_Chase");

                if (!audioManager.IsPlaying("Normal_Environment"))
                    audioManager.Play("Normal_Environment");
            }
        }

        void PauseAllForGameStop()
        {
            soundOn = !soundOn;

            if (!soundOn)
            {
                audioManager.PauseAll();
                audioManager.Play("Pause_BGM");
            }
            else
            {
                audioManager.UnPauseAll();
                audioManager.Pause("Pause_BGM");
            }
        }

        void PauseAllForDeath()
        {
            soundOn = !soundOn;

            if (!soundOn)
            {
                audioManager.PauseAll();
                audioManager.Play("Gameover");
                audioManager.Play("Pause_BGM");
            }
            else
            {
                audioManager.UnPauseAll();
                audioManager.Pause("Pause_BGM");
            }
        }

        IEnumerator TorchSoundPlay(float time)
        {
            yield return new WaitForSeconds(time);
            //Debug.Log("Fire sound starts");
            if (!audioManager.IsPlaying("Torch_Flame"))
                audioManager.Play("Torch_Flame");       
        }

        bool ifSoundExist(string soundName)
        {
            bool exist = false;
            foreach (Sound s in audioManager.sounds)
            {
                if (soundName == s.name)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        IEnumerator InteractionSound(float time, string name)
        {
            yield return new WaitForSeconds(time);
            if (ifSoundExist(name))
                audioManager.Play(name);
            else
                Debug.LogWarning("There is no such sound called: " + name);
        }

        void Minotaur_Hurt(int volume)
        {
            audioManager.SetVolume("Minotaur_Hurt", volume);
            audioManager.Play("Minotaur_Hurt");
        }

        void Breathing()
        {
            audioManager.SetVolume("Breathing", 100 - (int)playerMaster.health);
            audioManager.SetVolume("Heartbeat", 100 - (int)playerMaster.health);

            if (!audioManager.IsPlaying("Breathing") && !gameManagerMaster.gamePause)
                audioManager.Play("Breathing");
            
            if(!audioManager.IsPlaying("Heartbeat") && !gameManagerMaster.gamePause)
                audioManager.Play("Heartbeat");
        }


    }
}

