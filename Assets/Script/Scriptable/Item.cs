using UnityEngine;

namespace gm
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Interactable/Item")]
	public class Item : ScriptableObject{

        public string itemName = "New Item";
        public Sprite itemIcon = null;
        //public bool isDefaultItem = false;
        [TextArea]
        public string itemDescription = "";
        public string pickupSound;

        public InnerItem[] hiddenItems;
	}

    [System.Serializable]
    public class InnerItem
    {
        public Item[] requiredItems;
        public Item itemInside;
    }
}

