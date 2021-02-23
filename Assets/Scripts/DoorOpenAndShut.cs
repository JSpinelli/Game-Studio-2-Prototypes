using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAndShut : MonoBehaviour
{
    private bool doorisOpen;
    public GameObject rug;
    public GameObject trapdoor;

    // Start is called before the first frame update
    void Start()
    {
        doorisOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (doorisOpen && Input.GetKeyUp(KeyCode.E))
            {
                transform.Rotate(new Vector3(0, -90, 0));
                doorisOpen = false;
                Debug.Log("door closed!");
            }
            else if (!doorisOpen && Input.GetKeyUp(KeyCode.E))
            {
                transform.Rotate(new Vector3(0, 90, 0));
                doorisOpen = true;
                Debug.Log("door opened!");
                rug.SetActive(false);
                trapdoor.SetActive(true);
            }
        }
    }
}
