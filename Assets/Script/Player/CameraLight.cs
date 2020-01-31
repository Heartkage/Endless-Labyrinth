using UnityEngine;

public class CameraLight : MonoBehaviour {

    private Transform cameraTransform;
    public Light light;
    //public Light sourceLight;

	void Update () {
        light.transform.position = cameraTransform.transform.position + cameraTransform.transform.forward;
        //sourceLight.transform.position = cameraTranform.transform.position + cameraTranform.transform.forward;       
	}

    void OnEnable()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        cameraTransform = transform;
    }
}
