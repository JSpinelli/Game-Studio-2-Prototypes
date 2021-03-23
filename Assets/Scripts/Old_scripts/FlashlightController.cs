using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            if (GetComponent<Light>().enabled == false)
            {
                GetComponent<Light>().enabled = true;

            }
            else
                GetComponent<Light>().enabled = false;
        }
    }
}
