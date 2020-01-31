using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [CreateAssetMenu(fileName = "New Door Object", menuName = "Interactable/Object/Door")]
    public class ObjectDoor : Object
    {
        public GameManager_GlobalVariables.DoorType type;
        public string sound_DoorOpen;
        public string sound_DoorClose;
        public string sound_DoorLocked;
	}
}

