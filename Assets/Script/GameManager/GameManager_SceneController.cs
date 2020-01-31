using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gm
{
    [RequireComponent(typeof(GameManager_Master))]
	public class GameManager_SceneController : MonoBehaviour
    {

        #region Singleton
        public static GameManager_SceneController instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        private GameManager_Master gameManagerMaster;
        public GameObject confirmPanel;
        public GameObject[] itemsInScene;
        public Item[] itemsInGame;

        private bool itemInThisScene;
        private Pickup pickupScriptOnItem;

        void Start()
        {
            StartCoroutine(LateStart(0.1f));
        }

        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if (SystemSaveAndLoad.sceneLoadType == SystemSaveAndLoad.LoadType.loadSaved)
            {
                Debug.Log("LoadSaved");
                LoadLastSavedProgress();
            }
            else if (SystemSaveAndLoad.sceneLoadType == SystemSaveAndLoad.LoadType.sceneChanged)
            {
                Debug.Log("SceneChange");
                LoadSceneChangedProgress();
            }
        }
		
		void OnEnable()
		{
            SetInitialReferences();
            gameManagerMaster.GotoMainManuEvent += GoToMainManu;
            gameManagerMaster.RestartEvent += RestartNewGame;
		}
		
		void OnDisable()
		{
            gameManagerMaster.GotoMainManuEvent -= GoToMainManu;
            gameManagerMaster.RestartEvent -= RestartNewGame;
		}
		
		void SetInitialReferences()
		{
            gameManagerMaster = GetComponent<GameManager_Master>();
		}

        public void SaveCurrentProgress()
        {
            SystemSaveAndLoad.SaveGame(gameManagerMaster, Player_Inventory.instance);
        }

        void LoadLastSavedProgress()
        {
            GameSavedData data = SystemSaveAndLoad.LoadGame(SystemSaveAndLoad.savedFileName);

            //---If save file exist---//
            if (data != null)
            {
                gameManagerMaster.currentBuildIndex = data.savedSceneIndex;

                Vector3 position;
                position.x = data.playerPosition[0];
                position.y = data.playerPosition[1];
                position.z = data.playerPosition[2];
                Player_Master.instance.transform.position = position;

                //Debug.Log(position);
                //Player_Master.instance.transform.SetPositionAndRotation(position, Quaternion.EulerAngles(0, data.playerY_Rotation, 0));
                
                Player_Master.instance.gameObject.GetComponent<TorchBehavior>().energy = data.torchEnergy;

                for (int i = 0; i < data.itemsName.Length; i++)
                {
                    itemInThisScene = false;
                    foreach (GameObject gameObject in itemsInScene)
                    {
                        pickupScriptOnItem = gameObject.GetComponent<Pickup>();
                        if (pickupScriptOnItem != null)
                        {
                            if (pickupScriptOnItem.item.itemName == data.itemsName[i])
                            {
                                itemInThisScene = true;
                                Player_Inventory.instance.AddItem(pickupScriptOnItem.item);
                                gameObject.SetActive(false);
                                break;
                            }
                        }
                    }

                    if (!itemInThisScene)
                    {
                        foreach (Item item in itemsInGame)
                        {
                            if (item.itemName == data.itemsName[i])
                                Player_Inventory.instance.AddItem(item);
                        }
                    }
                }
            }
        }

        void LoadSceneChangedProgress()
        {
            GameSavedData data = SystemSaveAndLoad.LoadGame(SystemSaveAndLoad.savedFileName);

            if (data != null)
            {
                Player_Master.instance.gameObject.GetComponent<TorchBehavior>().energy = data.torchEnergy;

                for (int i = 0; i < data.itemsName.Length; i++)
                {
                    itemInThisScene = false;
                    foreach (GameObject gameObject in itemsInScene)
                    {
                        pickupScriptOnItem = gameObject.GetComponent<Pickup>();
                        if (pickupScriptOnItem != null)
                        {
                            if (pickupScriptOnItem.item.itemName == data.itemsName[i])
                            {
                                itemInThisScene = true;
                                Player_Inventory.instance.AddItem(pickupScriptOnItem.item);
                                gameObject.SetActive(false);
                                break;
                            }
                        }
                    }

                    if (!itemInThisScene)
                    {
                        foreach (Item item in itemsInGame)
                        {
                            if (item.itemName == data.itemsName[i])
                                Player_Inventory.instance.AddItem(item);
                        }
                    }
                }
            }
        }

        public void OpenConfirmPanel()
        {
            confirmPanel.SetActive(true);
        }

        public void CloseConfirmPanel()
        {
            confirmPanel.SetActive(false);
        }

        void GoToMainManu()
        {
            SceneManager.LoadScene(GameManager_GlobalVariables.instance.scenesName[0]);
        }

        public void MovedToNextScene(int nextBuildIndex)
        {
            SystemSaveAndLoad.sceneLoadType = SystemSaveAndLoad.LoadType.sceneChanged;
            SceneManager.LoadScene(GameManager_GlobalVariables.instance.scenesName[nextBuildIndex]);
        }

        public void RestartFromSavedPoint()
        { 
            GameSavedData data = SystemSaveAndLoad.LoadGame(SystemSaveAndLoad.savedFileName);
            if (data != null)
            {
                SystemSaveAndLoad.sceneLoadType = SystemSaveAndLoad.LoadType.loadSaved;
                SceneManager.LoadScene(GameManager_GlobalVariables.instance.scenesName[data.savedSceneIndex]);
            }
            else
            {
                SystemSaveAndLoad.sceneLoadType = SystemSaveAndLoad.LoadType.newGame;
                SceneManager.LoadScene(GameManager_GlobalVariables.instance.scenesName[1]);
            }
        }

        void RestartNewGame()
        {
            SystemSaveAndLoad.sceneLoadType = SystemSaveAndLoad.LoadType.newGame;
            SceneManager.LoadScene(GameManager_GlobalVariables.instance.scenesName[1]);       
        }
	}
}

