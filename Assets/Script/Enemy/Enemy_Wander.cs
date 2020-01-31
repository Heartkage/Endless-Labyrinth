using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace gm
{
    [RequireComponent(typeof(Enemy_Master))]
	public class Enemy_Wander : MonoBehaviour {

        private Enemy_Master enemyMaster;
        public float checkRate = 0.4f;
        public float checkWanderRange = 10;
        private float nextCheck;
        private NavMeshAgent agent;
        private Transform enemyTransform;
        private NavMeshHit navHit;
        private Vector3 nextDestinationPosition;
        private RaycastHit hit;
		
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
            agent = GetComponent<NavMeshAgent>();
            enemyTransform = transform;
            nextCheck = 0;
		}

        void Update()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CheckForWandering();
            }
        }

        void CheckForWandering()
        {
            if (enemyMaster.target == null)
            {
                if (!enemyMaster.isOnRoute)
                {
                    if (GetNextDestination(out nextDestinationPosition))
                    {
                        agent.SetDestination(nextDestinationPosition);
                        enemyMaster.isOnRoute = true;
                        enemyMaster.CallEventEnemyWalking();
                    }
                }
                else
                {
                    if(agent.remainingDistance <= agent.stoppingDistance)
                        enemyMaster.CallEventEnemyReachedTarget();
                }
            }
        }

        bool GetNextDestination(out Vector3 nextDestination)
        {
            Vector3 temp = Random.insideUnitSphere;
            //float randomY = Random.Range(-2f, 2f);
            Vector3 randomPoint = enemyTransform.position + new Vector3(temp.x, 0, temp.z) * checkWanderRange;
            randomPoint.y += 5;

            if (Physics.Raycast(randomPoint, Vector3.down, out hit, Mathf.Infinity))
            {
                if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
                {
                    nextDestination = navHit.position;
                    enemyMaster.nextDestinationPosition = nextDestination;
                    Debug.DrawRay(enemyMaster.nextDestinationPosition, Vector3.up * 100, Color.magenta, 1.0f);
                    return true;
                }
                else
                    Debug.DrawRay(hit.point, Vector3.up * 100, Color.cyan, 1.0f);
            }      

            //Debug.Log("failed");
            nextDestination = enemyTransform.position;
            return false;         
        }

		
	}
}

