using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(Enemy_Master))]
    [RequireComponent(typeof(Enemy_Sound))]
	public class Enemy_Attack : MonoBehaviour {

        [SerializeField]
        private float attackRange;
        [Range(0, 1)]
        [SerializeField]
        private float attackAngle;
        [SerializeField]
        private float attackSpeed;
        [SerializeField]
        private int attackDamage;
        
        private float nextAttack;

        private Enemy_Master enemyMaster;
        private Enemy_Sound enemySound;

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
            enemySound = GetComponent<Enemy_Sound>();
        }

		void Start ()
		{
            nextAttack = 0f;
            SetInitialReferences();
		}

		void Update ()
		{
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackSpeed;
                if (enemyMaster.isOnChase && !enemyMaster.isOnRoute && !Player_Master.instance.playerInAnimation)
                {
                    if (Vector3.Distance(transform.position, enemyMaster.target.position) <= attackRange)
                    {
                        enemyMaster.isInAttack = true;
                        Vector3 lookAtVector = new Vector3(enemyMaster.target.position.x, transform.position.y, enemyMaster.target.position.z);
                        transform.LookAt(lookAtVector);

                        enemyMaster.CallEventEnemyAttack();
                    }
                }
            }
		}


        /// <summary>
        /// Called by Animation
        /// </summary>
        void AttackTarget()
        {
            if (Vector3.Distance(transform.position, enemyMaster.target.position) <= attackRange)
            {
                Vector3 targetVector = enemyMaster.target.position - transform.position;
                if (Vector3.Dot(targetVector, transform.forward) >= attackAngle)
                {
                    enemySound.PlaySlashSound();
                    Player_Master.instance.CallEventReceivedDamage(attackDamage);
                }
            }
        }

        void EndAttack()
        {
            //Debug.Log("End attack called");
            enemyMaster.isInAttack = false;
        }
	}
}

