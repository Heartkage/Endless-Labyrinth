using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
	public class Hud_EnergyBar : MonoBehaviour {

        [SerializeField]
        private GameObject player;
        private Player_Master playerMaster;
        private TorchBehavior torch;

        private RectTransform energyTransform ;
        private Image image;
        private const float minimumX = -120f;
        private const float barLength = 120f;

        private float r = 177f/255f;
        private float g = 100f/255f;
        private float b = 5f/255f;

		void OnEnable()
		{
            SetInitialReferences();
            playerMaster.HudUpdateEvent += SetEnergyBarLength;
            playerMaster.HudUpdateEvent += SetEnergyBarColor;
		}	
		void OnDisable()
		{
            playerMaster.HudUpdateEvent -= SetEnergyBarLength;
            playerMaster.HudUpdateEvent -= SetEnergyBarColor;
		}
		
		void SetInitialReferences()
		{
            if (player != null)
            {
                playerMaster = player.GetComponent<Player_Master>();
                torch = player.GetComponent<TorchBehavior>();
            }                
            else
                Debug.LogWarning("Player is not assigned on Hud_EnergyBar");

            
            energyTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
		}

        void SetEnergyBarLength()
        {
            float positionX;
            positionX = minimumX + torch.energy*barLength/100f;
            //Debug.Log(torch.energy);
            energyTransform.localPosition = new Vector3(positionX, 0, 0);
        }

        void SetEnergyBarColor()
        {
            g = torch.energy/255f;
            //Debug.Log(g);
            image.color = new Color(r, g, b, 255);

        }



	}
}

