using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [CreateAssetMenu(fileName = "New Object", menuName = "Interactable/Object/Default")]
    public class Object : ScriptableObject
    {
        public string objectName = "New Object Name";
    }
}

