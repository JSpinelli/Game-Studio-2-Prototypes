using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyLooking : MonoBehaviour
{
    bool triggered = false;
    public Camera cam;

    void Update() 
    {
        Vector3 CameraCenter = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, cam.nearClipPlane));
        if (Physics.Raycast(CameraCenter,  transform.forward, 100))
            Debug.Log("Ou yeah!");
    }
}
