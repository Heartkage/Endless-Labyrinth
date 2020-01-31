using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Ignite_Torch : Interactable {

        protected override void Start()
        {
            base.Start();

            interactType = GameManager_GlobalVariables.InteractType.Ignite;
        }

        public override void Interact()
        {
            base.Interact();
            //---Animation---//
            Player_Master.instance.CallEventTriggerAnimation("Ignite");
            //Light up fire and turn on sound
            Player_Master.instance.CallEventTorchIgnite(1.4f);
        }
		
	}
}

