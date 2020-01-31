using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
	public class Hud_BloodSplash : MonoBehaviour {

        [SerializeField]
        private GameObject player;
        private Player_Master playerMaster;
        private Image image;
        public Image background;

        private float r = 177f / 255f;
        private float g = 177f / 255f;
        private float b = 177f / 255f;
        public float alpha = 0;

		void OnEnable()
		{
            SetInitialReferences();
            playerMaster.RecoveringStateEvent += SetBloodSplashTransparency;
		}
		
		void OnDisable()
		{
            playerMaster.RecoveringStateEvent -= SetBloodSplashTransparency;
		}
		
		void SetInitialReferences()
		{
            if (player != null)
                playerMaster = player.GetComponent<Player_Master>();
            else
                Debug.LogWarning("Player is not assigned on Hud_BloodSplash");

            image = GetComponent<Image>();
            image.color = new Color(r, g, b, alpha);

            background.color = new Color(84f / 255f, 0, 0, alpha);
		}

        void SetBloodSplashTransparency()
        {
            //Debug.Log("hi");

            alpha = (255f - playerMaster.health * 2.55f) / 255;
            image.color = new Color(r, g, b, alpha);

            alpha = (100f - playerMaster.health) / 255;
            background.color = new Color(84f/255f, 0, 0, alpha);
        }

	}
}

