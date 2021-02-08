using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
   public GameObject text;
 
    void OnTriggerEnter ()
    {
        text.SetActive(true);
    }
    void OnTriggerStay ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("interaction");
        }
    }
 
    void OnTriggerExit ()
    {
        text.SetActive(false);
    }
}
