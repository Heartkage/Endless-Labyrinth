using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class Player_Inventory : MonoBehaviour
    {
        #region Singleton
        public static Player_Inventory instance;
        void Awake()
        {
            if (instance != null)
                Debug.LogWarning("Two Inventories found");
            else
                instance = this;
        }
        #endregion

        public Transform itemListUIPanel;

        public List<Item> items = new List<Item>();
        private Vector3 originalSize = new Vector3(1, 1, 1);

        public delegate void InventoryMusic();
        public event InventoryMusic InventoryOnEvent;
		
        public void AddItem(Item newItem)
        {
            //if (!newItem.isDefaultItem)
                items.Add(newItem);
        }

        public void RemoveItem(Item oldItem)
        {
            if (items.Contains(oldItem))
            {
                items.Remove(oldItem);
            }
        }

        public bool hasItem(Item item)
        {
            return items.Contains(item);
        }

        //---Handle Inventory On Music---//
        void Update()
        {
            if (GameManager_Master.instance.isInventoryOn)
            {
                if (InventoryOnEvent != null)
                    InventoryOnEvent();
            }
        }

        public void UpdateInventoryUI()
        {
            RemoveAllButtons();
            AddButtons();
        }

        void AddButtons()
        {
            foreach (Item item in items)
            {
                GameObject newButton = GameObjectsPool.instance.SpawnGameObject();
                newButton.transform.SetParent(itemListUIPanel, false);
                newButton.transform.localScale = originalSize;

                newButton.GetComponent<UI_ItemButton>().Setup(item);
            }
        }

        void RemoveAllButtons()
        {
            while (itemListUIPanel.childCount > 0)
            {
                GameObject storeGameObject = itemListUIPanel.GetChild(0).gameObject;
                GameObjectsPool.instance.StoreGameObject(storeGameObject);
            }
        }

	}
}

