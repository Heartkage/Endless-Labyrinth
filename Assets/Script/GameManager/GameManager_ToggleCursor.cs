using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gm
{
	public class GameManager_ToggleCursor : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        private bool isCursorShowing;

		void Update ()
        {
            UpdateCursorState();
		}
		
		void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += ToggleCursorState;
            gameManagerMaster.InventoryToggleEvent += ToggleCursorState;
            gameManagerMaster.GameOverEvent += ToggleCursorState;
		}
		
		void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
            gameManagerMaster.InventoryToggleEvent -= ToggleCursorState;
            gameManagerMaster.GameOverEvent -= ToggleCursorState;
		}
		
		void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
            isCursorShowing = false;
		}

        void ToggleCursorState()
        {
            isCursorShowing = !isCursorShowing;
        }

        void UpdateCursorState()
        {
            if (!isCursorShowing)
            {           
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if(isCursorShowing)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

	}
}

