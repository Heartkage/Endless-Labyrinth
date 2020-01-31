using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class GameManager_GameOver : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        [SerializeField]
        private GameObject gameOverManu;
		
		void OnEnable()
		{
            SetInitialReferences();
            gameManagerMaster.GameOverEvent += ShowGameOverManu;
		}
		
		void OnDisable()
		{
            gameManagerMaster.GameOverEvent -= ShowGameOverManu;
		}
		
		void SetInitialReferences()
		{
            gameManagerMaster = GetComponent<GameManager_Master>();
            gameOverManu.SetActive(false);
		}

        void ShowGameOverManu()
        {
            gameManagerMaster.isGameOver = true;
            gameOverManu.SetActive(true);
        }


	}
}

