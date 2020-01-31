using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Enemy_Master : MonoBehaviour
    {

        public Transform target;
        public Vector3 nextDestinationPosition;

        public bool isOnRoute = false;
        public bool isOnChase = false;
        public bool isInAttack = false;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EnemyWalkingEvent;
        public event GeneralEventHandler EnemyPursuingEvent;
        public event GeneralEventHandler EnemyLostTargetEvent;
        public event GeneralEventHandler EnemyReachedTargetEvent;  
        public event GeneralEventHandler EnemyAttackEvent;

        public delegate void TargetEventHandler(Transform targetTransform);
        public event TargetEventHandler EnemySetTargetEvent;


        public void CallEventEnemyWalking()
        {
            if (EnemyWalkingEvent != null)
                EnemyWalkingEvent();
        }

        public void CallEventEnemyPursuing()
        {
            if (EnemyPursuingEvent != null)
                EnemyPursuingEvent();        
        }

        public void CallEventEnemyLostTarget()
        {
            if (EnemyLostTargetEvent != null)
                EnemyLostTargetEvent();

            isOnChase = false;
            target = null;         
        }

        public void CallEventEnemyReachedTarget()
        {
            if (EnemyReachedTargetEvent != null)
                EnemyReachedTargetEvent();
        }

        public void CallEventEnemyAttack()
        {
            if (EnemyAttackEvent != null)
                EnemyAttackEvent();
        }

        public void CallEventEnemySetTarget(Transform targetPosition)
        {
            if (EnemySetTargetEvent != null)
                EnemySetTargetEvent(targetPosition);

            isOnChase = true;
            target = targetPosition;
        }


	}
}

