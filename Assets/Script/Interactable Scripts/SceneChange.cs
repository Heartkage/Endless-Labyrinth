using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gm
{
    public class SceneChange : Triggerable
    {
        public int nextSceneIndex = 1;

        public override void Trigger()
        {
            base.Trigger();
            GameManager_SceneController.instance.MovedToNextScene(nextSceneIndex);
        }
    }
}

