using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace gm
{
    public static class SystemSaveAndLoad
    {
        public enum LoadType
        {
            newGame = 0,
            loadSaved = 1,
            sceneChanged = 2
        }

        public static LoadType sceneLoadType = LoadType.newGame;

        public static string savedFileName = "minotuarData.sav";

        public static void SaveGame(GameManager_Master gameManagerMaster, Player_Inventory playerInventory)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = string.Concat(Application.persistentDataPath, "/", savedFileName);
            FileStream file = new FileStream(filePath, FileMode.Create);

            TorchBehavior torch = playerInventory.gameObject.GetComponent<TorchBehavior>();

            GameSavedData data = new GameSavedData(gameManagerMaster, playerInventory, torch);
            formatter.Serialize(file, data);

            file.Close();
        }

        public static GameSavedData LoadGame(string savedName)
        {
            string filePath = string.Concat(Application.persistentDataPath, "/", savedName);

            if (File.Exists(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                FileStream file = new FileStream(filePath, FileMode.Open);
                GameSavedData data = formatter.Deserialize(file) as GameSavedData;
                file.Close();

                return data;
            }
            else
            {
                Debug.Log("File not found");
                return null;
            }
        }

        

    }
}

