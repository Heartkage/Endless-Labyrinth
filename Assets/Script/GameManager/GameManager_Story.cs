using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gm
{
    public class GameManager_Story : MonoBehaviour
    {

        [SerializeField]
        private GameObject story;
        
        private GameManager_Master gameManagerMaster;


        /*// Update is called once per frame
        void Update()
        {
            CheckStoryOn();
        }

        void OnEnable()
        {
            SetInitialReferences();
            story.SetActive(false);
        }

        void OnDisable()
        {
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void CheckStoryOn()
        {
            gameManagerMaster.isStoryOn = story.activeSelf;
        }*/
    }
}
