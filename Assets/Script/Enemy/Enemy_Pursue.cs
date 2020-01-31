using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace gm
{
	public class Enemy_Pursue : MonoBehaviour {

        private Enemy_Master enemyMaster;
        private NavMeshAgent navMeshAgent;
        private float checkRate;
        private float nextCheck;

        private bool checkForHaunt;
	
		void OnEnable()
		{
            SetInitialReferences();
		}
		
		void OnDisable()
		{
		
		}
		
		void SetInitialReferences()
		{
            enemyMaster = GetComponent<Enemy_Master>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.1f, 0.2f);
            nextCheck = 0;
            checkForHaunt = false;
		}

        void Update()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                ChasePlayer();
            }
        }

        void ChasePlayer()
        {
            if (enemyMaster.target != null)
            {
                Player_Master.instance.playerIsHaunted = true;
                if (!enemyMaster.isInAttack)
                {
                    navMeshAgent.SetDestination(enemyMaster.target.position);

                    if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
                    {
                        enemyMaster.CallEventEnemyPursuing();
                    }
                    else
                    {
                        enemyMaster.CallEventEnemyReachedTarget();
                    }
                }
                else
                {
                    navMeshAgent.ResetPath();
                }
                checkForHaunt = false;
            }
            else if (!checkForHaunt)
            {
                Player_Master.instance.playerIsHaunted = false;
                checkForHaunt = true;
            }
                
        }
	}
}

