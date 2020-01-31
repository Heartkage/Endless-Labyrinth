using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Enemy_Detection : MonoBehaviour {

        private Enemy_Master enemyMaster;
        public Transform head;
        private Transform body;
        public LayerMask playerLayer;
        public LayerMask sightLayer;
        private float checkRate;
        private float nextCheck;
        public float bodyDetectRadius = 13f;
        public float eyeDetectRadius = 3f;
        public float eyeDetectRange = 30f;
        private RaycastHit hit;
        private RaycastHit hitBySight;
		void Update ()
		{
            PlayerDetection();
		}
		
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
            body = transform;
            checkRate = Random.Range(0.8f, 1.2f);
            nextCheck = 0;
		}

        void PlayerDetection()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                bool check = false;

                Collider[] colliders = Physics.OverlapSphere(head.position, bodyDetectRadius, playerLayer);
                if (colliders.Length > 0)
                {
                    foreach (Collider foundObject in colliders)
                    {
                        if (foundObject.CompareTag(GameManager_GlobalVariables.instance._playerTag))
                        {
                            //Debug.Log("found");
                            if (check = CanPlayerBeDetectInRange(foundObject.transform))
                            {
                                break;
                            }
                        }
                    }
                }
                
                if(!check)
                    DetectPlayerBySight();

            }
        }

        void DetectPlayerBySight()
        {
            if (Physics.SphereCast(head.position, eyeDetectRadius, head.forward, out hitBySight, eyeDetectRange, playerLayer))
            {
                if (hitBySight.transform.tag == GameManager_GlobalVariables.instance._playerTag)
                {
                    if (CanPlayerBeDetectInRange(hitBySight.transform))
                        Debug.Log("Player in Sight Range");
                }
            }
            else
                enemyMaster.CallEventEnemyLostTarget();
        }

        bool CanPlayerBeDetectInRange(Transform player)
        {
            if (Physics.Linecast(head.position, player.position, out hit, sightLayer))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform == player)
                {
                    enemyMaster.CallEventEnemySetTarget(player);
                    Debug.Log("Player in detect range");
                    return true;
                }
                else
                {
                    enemyMaster.CallEventEnemyLostTarget();
                    return false;
                }
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }     

        void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(head.position, head.position + head.forward * eyeDetectRange);
            Gizmos.DrawWireSphere(head.position + head.forward * eyeDetectRange, eyeDetectRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(head.position, bodyDetectRadius);
        }


	}
}

