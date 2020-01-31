using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenu_Camera : MonoBehaviour {


    private Animator animation;

    void SetInitialReferences()
    {
        animation = GetComponent<Animator>();
    }

	void Start () {
        SetInitialReferences();
	}

    public void SettingOn()
    {
        animation.SetBool("Setting", true);
    }

    public void SettingOff()
    {
        animation.SetBool("Setting", false);
    }
	
}
