using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [RequireComponent(typeof(Collider))]
    public class Triggerable : MonoBehaviour
    {
        public LayerMask triggerableLayer;
        public bool hasTriggerTime;
        public float triggerTime;

        public bool isTriggerOnce;
        public bool isRapidlyTrigger;

        protected float nextTriggerTime;
        protected bool thingsFound;
        private bool hasTriggered;
        private int realLayerValue;

        public virtual void Trigger()
        {
            //Debug.Log("Triggering~");
        }

        protected virtual void Start()
        {
            thingsFound = false;
            hasTriggered = false;
        }

        protected virtual void Update()
        {
            if (thingsFound && !hasTriggered)
            {
                if (hasTriggerTime)
                {
                    if (Time.time > nextTriggerTime)
                    {
                        Trigger();

                        //---If it can't be triggering every frame, then things need to re-enter the trigger point---//
                        if (!isRapidlyTrigger)
                            thingsFound = false;

                        //---If it only can be triggered once, then once triggered, it cannot be trigger the second time---//
                        if (isTriggerOnce)
                            hasTriggered = true;
                    }
                }
                else
                {
                    Trigger();

                    //---If it can't be triggering every frame, then things need to re-enter the trigger point---//
                    if (!isRapidlyTrigger)
                        thingsFound = false;

                    //---If it only can be triggered once, then once triggered, it cannot be trigger the second time---//
                    if (isTriggerOnce)
                        hasTriggered = true;
                } 
            }
        }


        void OnTriggerEnter(Collider collidedThing)
        {
            realLayerValue = 1 << collidedThing.gameObject.layer;

            if (realLayerValue == triggerableLayer.value)
            {
                nextTriggerTime = Time.time + triggerTime;
                thingsFound = true;
            }
        }

        protected virtual void OnTriggerExit(Collider collidedThing)
        {
            realLayerValue = 1 << collidedThing.gameObject.layer;

            if (realLayerValue == triggerableLayer.value)
            {
                thingsFound = false;
            }
        }

    }
}

