using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
    [RequireComponent(typeof(Animator))]
	public class UI_ItemIconEvent : MonoBehaviour
    {
        #region Singleton
        public static UI_ItemIconEvent instance;

        void Awake()
        {
            instance = this;
        }

        #endregion

        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private Text itemName;

        private Animator animation;

        void Start()
        {
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            animation = GetComponent<Animator>();
        }

        public void FoundItem(Item item)
        {
            itemIcon.sprite = item.itemIcon;
            itemName.text = string.Concat(item.itemName, " Obtained");
            animation.SetTrigger("ShowBar");
            Player_Inventory.instance.AddItem(item);
        }


    }
}

