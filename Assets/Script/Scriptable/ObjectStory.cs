using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    [CreateAssetMenu(fileName = "New Story Object", menuName = "Interactable/Object/Story")]
    public class ObjectStory : Object
    {
        public GameManager_GlobalVariables.StoryType type;
        [TextArea]
        public string StoryContent = "";
    }
}
