using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
    public class UI_Story : MonoBehaviour
    {

        #region Singleton
        public static UI_Story instance;
        void Awake()
        {
            instance = this;
        }
        #endregion

        //public Button descriptionButton;
        //public Image descriptionIcon;
        public Text storyText;
        public Scrollbar textScrollbar;

        void Start()
        {
            if ((storyText == null) || (textScrollbar == null))
                Debug.LogWarning("UI_ItemDescription is missing GameObject");
        }

        public void SetText(ObjectStory story)
        {
            //descriptionIcon.sprite = item.itemIcon;
            storyText.text = story.StoryContent;
            textScrollbar.value = 1;
        }
    }
}