using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyLooking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.gameObject.tag=="Baby"){
                Debug.Log("Stop Looking at the baby");
            }
        }

    }
}
