using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace gm
{
	public class Enemy_Animation : MonoBehaviour {

        private Enemy_Master enemyMaster;
        private Animator enemyAnimator;
        private NavMeshAgent agent;

        private const float animationSmoothTime = 0.1f;
        private float speedPercent;

        private bool isPursuing;
		
		void OnEnable()
		{
            SetInitialReferences();
            enemyMaster.EnemyLostTargetEvent += SetAnimationToIdle;
            enemyMaster.EnemyPursuingEvent += SetAnimationToChase;
            enemyMaster.EnemyReachedTargetEvent += SetAnimationToIdle;
            enemyMaster.EnemyAttackEvent += SetAnimationToAttack;
		}
		
		void OnDisable()
		{
            enemyMaster.EnemyLostTargetEvent -= SetAnimationToIdle;
            enemyMaster.EnemyPursuingEvent -= SetAnimationToChase;
            enemyMaster.EnemyReachedTargetEvent -= SetAnimationToIdle;
            enemyMaster.EnemyAttackEvent -= SetAnimationToAttack;
		}
		
		void SetInitialReferences()
		{
            enemyMaster = GetComponent<Enemy_Master>();         
            enemyAnimator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            isPursuing = false;
		}

        void Update()
        {
            speedPercent = agent.velocity.magnitude / agent.speed;
            enemyAnimator.SetFloat("SpeedPercent", speedPercent, animationSmoothTime, Time.deltaTime);       
        }

        void SetAnimationToIdle()
        {
            enemyAnimator.SetBool("isPursuing", false);
            isPursuing = false;
        }

        void SetAnimationToChase()
        {
            isPursuing = true;
            enemyAnimator.SetBool("isPursuing", true);
        }

        void SetAnimationToAttack()
        {
            enemyAnimator.SetTrigger("Attack");
        }

	}
}

