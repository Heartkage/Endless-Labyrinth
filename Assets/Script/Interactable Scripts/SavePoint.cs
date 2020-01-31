using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gm
{
    public class SavePoint : Triggerable
    {

        public KeyCode confirmKey;
        private float currentSecond;

        private bool windowIsUp;

        public override void Trigger()
        {
            base.Trigger();


            Player_HudControl.instance.OpenConfirmWindow();
            windowIsUp = true;     
        }

        protected override void Start()
        {
            base.Start();

            windowIsUp = false;
            Player_HudControl.instance.SetCircleFillAmount(0);
        }
        
        protected override void Update()
        {
            base.Update();

            if (thingsFound)
            {
                currentSecond = triggerTime - (nextTriggerTime - Time.time);
                if (currentSecond >= 0)
                {
                    //Debug.Log(currentSecond);
                    Player_HudControl.instance.SetCircleFillAmount(currentSecond / triggerTime);
                }
            }

            if (windowIsUp)
            {
                Player_HudControl.instance.SetCircleFillAmount(0);
                if (Input.GetKeyDown(confirmKey))
                {
                    GameManager_SceneController.instance.SaveCurrentProgress();
                    Debug.Log("Progress Saved");
                    AudioManager.instance.Play("Item_Obtain");
                    Player_HudControl.instance.CloseConfirmWindow();
                    StartCoroutine(Player_HudControl.instance.HintText(2f, "Progress Saved"));
                    windowIsUp = false;
                }
                else if (!Player_Master.instance.stationary || GameManager_Master.instance.gamePause)
                {
                    Player_HudControl.instance.CloseConfirmWindow();
                    windowIsUp = false;
                }
            }
        }

        protected override void OnTriggerExit(Collider collidedThing)
        {
            base.OnTriggerExit(collidedThing);
            Player_HudControl.instance.SetCircleFillAmount(0);
        }
    }
}

