using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
	public class UI_ItemDescription : MonoBehaviour {

        #region Singleton
        public static UI_ItemDescription instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        public Button descriptionButton;
        public Image descriptionIcon;
        public Text descriptionText;
        public Scrollbar textScrollbar;

        private bool hasAllRequiredItems;
        private Item requiredItem;

		void Start ()
		{
            if ((descriptionButton == null) || (descriptionIcon == null) || (descriptionText == null) || (textScrollbar == null))
                Debug.LogWarning("UI_ItemDescription is missing GameObject");
		}

        public void SetIconAndText(Item item)
        {
            descriptionIcon.sprite = item.itemIcon;
            descriptionText.text = item.itemDescription;
            textScrollbar.value = 1;
            descriptionButton.onClick.AddListener(delegate { HandleImageClick(item); });
        }

        public void RemoveListenerOnButton()
        {
            descriptionButton.onClick.RemoveAllListeners();
        }

        void HandleImageClick(Item item)
        {
            //---If there is no hidden item---//
            if (item.hiddenItems.Length == 0)
            {
                AudioManager.instance.Play("Image_Click");
                UI_DialogEvent.instance.ShowDialog(string.Concat("It's just a ", item.itemName, ", nothing special."));
            }
            else
            {
                for (int i = 0; i < item.hiddenItems.Length; i++)
                {
                    if (item.hiddenItems[i].requiredItems.Length == 0)
                    {
                        if (!Player_Inventory.instance.hasItem(item.hiddenItems[i].itemInside))
                        {
                            AudioManager.instance.Play("Item_Obtain");
                            UI_ItemIconEvent.instance.FoundItem(item.hiddenItems[i].itemInside);
                        }
                        else
                            UI_DialogEvent.instance.ShowDialog(string.Concat("It's empty now..."));
                    }
                    else
                    {
                        //----Check for required Item----//
                        hasAllRequiredItems = true;
                        for (int j = 0; j < item.hiddenItems[i].requiredItems.Length; j++)
                        {
                            if (!Player_Inventory.instance.hasItem(item.hiddenItems[i].requiredItems[j]))
                            {
                                hasAllRequiredItems = false;
                                requiredItem = item.hiddenItems[i].requiredItems[j];
                                break;
                            }
                        }

                        if (hasAllRequiredItems)
                        {
                            if (!Player_Inventory.instance.hasItem(item.hiddenItems[i].itemInside))
                            {
                                AudioManager.instance.Play("Item_Obtain");
                                UI_ItemIconEvent.instance.FoundItem(item.hiddenItems[i].itemInside);
                            }
                            else
                                UI_DialogEvent.instance.ShowDialog(string.Concat("It's empty now..."));
                        }   
                        else
                        {
                            UI_DialogEvent.instance.ShowDialog(string.Concat("You need ", requiredItem.itemName));
                        }
                    }
                }

                Player_Inventory.instance.UpdateInventoryUI();
            }
        }

	}
}

