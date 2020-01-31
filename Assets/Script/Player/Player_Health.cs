using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gm
{
	public class Player_Health : MonoBehaviour {

        [SerializeField]
        private GameObject gameManager;
        private GameManager_Master gameManagerMaster;
        private Player_Master playerMaster;


        private float recoveringRate = 1f;
        private float nextCheck = 0;
        private float regenerateTime;
        private float recoveringAmount = 0f;
        [SerializeField]
        private float recoveringAmountPerRate = 8;
        //public float health = 100;
		
		void OnEnable()
		{
            SetInitialReferences();
            playerMaster.ReceivedDamageEvent += PlayerTakeDamage;
		}
		
		void OnDisable()
		{
            playerMaster.ReceivedDamageEvent -= PlayerTakeDamage;
		}
		
		void SetInitialReferences()
		{
            gameManagerMaster = gameManager.GetComponent<GameManager_Master>();
            playerMaster = GetComponent<Player_Master>();
		}

        void Update()
        {
            if (!gameManagerMaster.isGameOver)
            {
                if (playerMaster.health <= 0)
                    gameManagerMaster.CallEventGameOver();
                else if (playerMaster.health < 100)
                {
                    if(!playerMaster.playerIsHaunted)
                        RecoveringHP();

                    playerMaster.CallEventRecoveringState();
                }
            }     
        }

        void PlayerTakeDamage(int amount)
        {
            playerMaster.health -= amount;
            if (playerMaster.health < 0)
                playerMaster.health = 0f;
            regenerateTime = 0f;
        }

        void RecoveringHP()
        {
            if (nextCheck + recoveringRate < Time.time)
            {
                nextCheck = Time.time;
                recoveringAmount = Mathf.Pow(regenerateTime / 20f, 2);
                if (playerMaster.stationary)
                    playerMaster.health += recoveringAmount * recoveringAmountPerRate;
                else
                    playerMaster.health += recoveringAmount * recoveringAmountPerRate / 2;

                //Debug.Log("Time: " + regenerateTime + ", Recovering " + recoveringAmount * recoveringAmountPerRate + " HP");
                regenerateTime++;

                if (playerMaster.health > 100)
                {
                    playerMaster.health = 100;
                    regenerateTime = 0f;
                }                   
            }
        }
   
        
	}
}

