using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace gm
{
    [RequireComponent(typeof(NavMeshAgent))]
	public class Player_Motor : MonoBehaviour {

        private NavMeshAgent agent;
        private Vector3 interactionPoint;
        private Vector3 targetPoint;
        private Camera m_Camera;
        bool pointIsSet;

        void Start()
        {
            SetInitialReferences();
        }
        void SetInitialReferences()
        {
            agent = GetComponent<NavMeshAgent>();
            m_Camera = Camera.main;
            pointIsSet = false;
        }

        void Update()
        {
            if (pointIsSet)
            {
                agent.SetDestination(interactionPoint);
                FaceTarget();
            }
        }

        public void SetInteractionTarget(Interactable newTarget)
        {
            agent.stoppingDistance = newTarget.interactionRadius*0.8f;
            agent.updateRotation = false;

            interactionPoint = newTarget.interactionTransform.position;
            targetPoint = newTarget.targetTransform.position;
            pointIsSet = true;

            agent.enabled = true;
        }

        public void RemoveInteractionPoint()
        {
            if (agent.isActiveAndEnabled)
            {
                agent.stoppingDistance = 0.2f;
                agent.updateRotation = true;
                agent.isStopped = true;
                pointIsSet = false;

                agent.enabled = false;
            }            
        }

        void FaceTarget()
        {
            Vector3 direction = (targetPoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            /*direction = (targetPoint - m_Camera.transform.position).normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, lookRotation, Time.deltaTime * 5f);*/
        }
	}
}

