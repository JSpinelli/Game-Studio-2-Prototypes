using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPSwapper : MonoBehaviour
{
    public PostProcessProfile vanilla;
    public PostProcessProfile t1;
    public PostProcessProfile t2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<PostProcessVolume>().profile = vanilla;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<PostProcessVolume>().profile = t1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<PostProcessVolume>().profile = t2;
        }
    }
}
