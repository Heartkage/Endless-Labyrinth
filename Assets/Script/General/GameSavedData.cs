using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [System.Serializable]
    public class GameSavedData {

        public int savedSceneIndex;
        public float[] playerPosition;
        public float playerY_Rotation;
        public float torchEnergy;
        public string[] itemsName;

        public GameSavedData(GameManager_Master gameManagerMaster, Player_Inventory playerInventory, TorchBehavior torch)
        {
            //---Save current scene index---//
            savedSceneIndex = gameManagerMaster.currentBuildIndex;

            playerPosition = new float[3];
            playerPosition[0] = playerInventory.transform.position.x;
            playerPosition[1] = playerInventory.transform.position.y;
            playerPosition[2] = playerInventory.transform.position.z;

            playerY_Rotation = playerInventory.transform.rotation.y;

            torchEnergy = torch.energy;

            if (playerInventory.items.Count > 0)
                itemsName = new string[playerInventory.items.Count];
            else
                itemsName = new string[1];

            int currentIndex = 0;
            foreach (Item item in playerInventory.items)
            {
                itemsName[currentIndex] = item.itemName;
                currentIndex++;
            }
        }
    }
}


