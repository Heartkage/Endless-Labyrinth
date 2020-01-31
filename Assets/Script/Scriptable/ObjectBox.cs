using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [CreateAssetMenu(fileName = "New Box Object", menuName = "Interactable/Object/Box")]
    public class ObjectBox: Object
    {
        public GameManager_GlobalVariables.BoxType type;
        public string sound_DoorOpen;
        public string sound_DoorClose;
        public string sound_DoorLocked;
    }
}
