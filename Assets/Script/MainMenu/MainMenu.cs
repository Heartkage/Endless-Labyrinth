using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
    [RequireComponent(typeof(Animator))]
    public class MainMenu : MonoBehaviour
    {
        private Animator animation;
        [SerializeField]
        private Button loadButton;
        [SerializeField]
        private Animator panelAnimation;
        public string[] allSceneNames;

        private bool loadable;
        private int loadableSceneIndex;

        void Start()
        {
            animation = GetComponent<Animator>();
            CheckLoadFile();
            Time.timeScale = 1;
            Audio_Controller.instance.PlayMainBGM();
        }

        void CheckLoadFile()
        {
            GameSavedData data = SystemSaveAndLoad.LoadGame(SystemSaveAndLoad.savedFileName);
            if (data != null)
            {
                loadableSceneIndex = data.savedSceneIndex;
                loadable = true;
                loadButton.interactable = true;
            }
            else
            {
                loadableSceneIndex = 1;
                loadable = false;
                loadButton.interactable = false;
            }
                
        }

        public void OpenStartBtn()
        {
            if (!animation.GetBool("StartButtonOpen"))
                animation.SetBool("StartButtonOpen", true);
        }

        public void ToggleStartBtn()
        {
            if (!animation.GetBool("StartButtonOpen"))
                animation.SetBool("StartButtonOpen", true);
            else
                animation.SetBool("StartButtonOpen", false);
        }

        public void CloseStartBtn()
        {
            if (animation.GetBool("StartButtonOpen"))
                animation.SetBool("StartButtonOpen", false);
        }

        public void OpenExitPanel()
        {

            panelAnimation.SetBool("ExitOpen", true);
        }

        public void CloseExitPanel()
        {
            panelAnimation.SetBool("ExitOpen", false);
        }

        public void NewGame()
        {
            SystemSaveAndLoad.sceneLoadType = SystemSaveAndLoad.LoadType.newGame;
            AudioManager.instance.StopAllSounds();
            UnityEngine.SceneManagement.SceneManager.LoadScene(allSceneNames[1]);
        }

        public void LoadGame()
        {
            if (loadable)
            {
                SystemSaveAndLoad.sceneLoadType = SystemSaveAndLoad.LoadType.loadSaved;
                AudioManager.instance.StopAllSounds();
                UnityEngine.SceneManagement.SceneManager.LoadScene(allSceneNames[loadableSceneIndex]);
            }
        }

        public void ExitGame()
        {
            AudioManager.instance.StopAllSounds();
            Application.Quit();
        }
    }
}

