using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
	public class Player_HudControl : MonoBehaviour
    {

        #region Singleton
        public static Player_HudControl instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        [SerializeField]
        private GameObject pressMessage;
        [SerializeField]
        private Text keyPressText;
        private string pressText;

        [SerializeField]
        private GameObject fireEnergyBar;
        

        [SerializeField]
        private GameObject pickupMessage;
        [SerializeField]
        private Image pickupImage;
        [SerializeField]
        private Text pickupItemName;

        [SerializeField]
        private Text hintText;

        [SerializeField]
        private GameObject countingCircle;
        [SerializeField]
        private GameObject confirmWindow;
        private Image circle;

        public Item torch;

        private Player_Master playerMaster;
        private bool middleHintBarDisplay;
	
		void OnEnable()
		{
            SetInitialReferences();

            playerMaster.TorchToggleEvent += ToggleEnergyBar;
            playerMaster.ItemPickUpEvent += PickupedItem;
		}
        void OnDisable()
        {
            playerMaster.TorchToggleEvent -= ToggleEnergyBar;
            playerMaster.ItemPickUpEvent -= PickupedItem;
        }

		void SetInitialReferences()
		{
            playerMaster = GetComponent<Player_Master>();
            pickupMessage.SetActive(false);
            middleHintBarDisplay = false;
            hintText.gameObject.SetActive(true);
            pressText = "";

            countingCircle.SetActive(true);
            circle = countingCircle.GetComponent<Image>();
            circle.fillAmount = 0;
            confirmWindow.SetActive(false);
		}

        void Update()
        {
            playerMaster.CallEventHudUpdate();
            if (middleHintBarDisplay)
            {
                hintText.CrossFadeAlpha(1, 0.25f, false);
            }
            else
            {
                hintText.CrossFadeAlpha(0, 0, false);
            }

            CheckForPressText();
        }

        void CheckForPressText()
        {
            if (pressText != "")
            {
                pressMessage.SetActive(true);
                keyPressText.text = pressText;
            }
            else
            {
                pressMessage.SetActive(false);
            }
        }

        public IEnumerator HintText(float time, string words)
        {
            middleHintBarDisplay = true;
            hintText.text = words;

            yield return new WaitForSeconds(time);
            middleHintBarDisplay = false;
        }


        public void InteractKeyDisplay(string text)
        {
            pressText = text;
        }


        void ToggleEnergyBar()
        {
            //if (playerMaster.itemLists.IsItemInInventory(GameManager_GlobalVariables._torch))
            if (Player_Inventory.instance.hasItem(torch))
            {
                fireEnergyBar.SetActive(!fireEnergyBar.activeSelf);
            }  
            else
                fireEnergyBar.SetActive(false);
        }

        IEnumerator PickupedItem(float time, string soundName)
        {
            pickupMessage.SetActive(true);

            if (playerMaster.itemFound != null)
                pickupItemName.text = string.Concat(playerMaster.itemFound.itemName, " Obtained!");
            pickupImage.sprite = playerMaster.itemFound.itemIcon;
            playerMaster.itemFound = null;

            yield return new WaitForSeconds(time*2.5f);
            pickupMessage.SetActive(false);
        }

        public void SetCircleFillAmount(float amount)
        {
            circle.fillAmount = amount;
        }

        public void OpenConfirmWindow()
        {
            confirmWindow.SetActive(true);
        }

        public void CloseConfirmWindow()
        {
            confirmWindow.SetActive(false);
        }

	}
}

