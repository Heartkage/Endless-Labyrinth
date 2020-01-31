using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
    public class SaveBeforeNextScene : Triggerable
    {
        public override void Trigger()
        {
            base.Trigger();
            GameManager_SceneController.instance.SaveCurrentProgress();
        }
    }
}


