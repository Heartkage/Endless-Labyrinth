using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
	public class UI_ItemButton : MonoBehaviour {

        public Button button;
        public Text itemName;
        public Image itemIcon;

        private Item item;

		void Start ()
		{
            button.onClick.AddListener(HandleClick);

		}

        /*void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }*/

        public void Setup(Item newItem)
        {
            item = newItem;
            itemName.text = newItem.itemName;
            itemIcon.sprite = newItem.itemIcon;
        }

        public void HandleClick()
        {
            UI_ItemDescription.instance.RemoveListenerOnButton();
            UI_ItemDescription.instance.SetIconAndText(item);
            AudioManager.instance.Play("Item_Click");
        }
	}
}

