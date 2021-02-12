using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyLooking : MonoBehaviour
{
    bool triggered = false;
    public GameObject babyLookingPrompt;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something Entered");
        if (!triggered)
        {
            Debug.Log(other.gameObject.tag);
            if (other.gameObject.tag == "Baby")
            {
                Debug.Log("YOU ARE LOOKING AT BABY");
                triggered = true;
                babyLookingPrompt.SetActive(true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggered)
        {
            if (other.gameObject.tag == "Baby")
            {
                triggered = false;
                babyLookingPrompt.SetActive(false);
            }

        }
    }

}
