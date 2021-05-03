using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{

    public Transform ToTrack;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(ToTrack);
    }
}
