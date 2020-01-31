using UnityEngine;

namespace gm
{
	public class Pickup : Interactable {

        
        public Item item;

        protected override void Start()
        {
            base.Start();
            interactType = GameManager_GlobalVariables.InteractType.Pickup;
            this.name = item.itemName;
        }
        
        public override void Interact()
        {
            base.Interact();

            // Play Pickup Animation
            Player_Master.instance.CallEventTriggerAnimation("Pickup");
            //---Add Item to inventory---//
            Player_Master.instance.CallEventItemPickUp(0.7f, item.pickupSound);
            Player_Inventory.instance.AddItem(item);

            // Destory Item
            Player_Master.instance.focus = null;
            Destroy(gameObject, 0.7f);
        }

        
	}
}

