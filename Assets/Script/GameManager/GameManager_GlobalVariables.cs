using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class GameManager_GlobalVariables : MonoBehaviour
    {

        #region Singleton
        public static GameManager_GlobalVariables instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        //-----Enum----//
        public enum DoorType
        {      
            Default = 0,
            Wooden = 1,
            Iron = 2,
        }

        public enum BoxType
        {
            Default = 0
        }

        public enum StoryType
        {
            Default = 0
        }

        public enum InteractType
        {
            None = 0,
            Pickup = 1,
            Ignite = 2,
            Door = 3,
            Box = 4,
            Story = 5
        }

        //----End of Enum------//

        //-----String----- 
        public string _playerTag;


        public string[] scenesName;

        void OnEnable()
        {


        }

	}
}

