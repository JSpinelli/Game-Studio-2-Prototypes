using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkUpAndInteract : MonoBehaviour
{
   public GameObject text;
   public float radius = 3f;
 
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
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
