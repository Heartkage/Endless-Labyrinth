using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Enemy_Reached : MonoBehaviour {

        private Enemy_Master enemyMaster;

		void OnEnable()
		{
            SetInitialReferences();
            enemyMaster.EnemyReachedTargetEvent += ReachedDestination;
		}
		
		void OnDisable()
		{
            enemyMaster.EnemyReachedTargetEvent -= ReachedDestination;
		}
		
		void SetInitialReferences()
		{
            enemyMaster = GetComponent<Enemy_Master>();
		}

        void ReachedDestination()
        {
            enemyMaster.isOnRoute = false;
        }
	}
}

