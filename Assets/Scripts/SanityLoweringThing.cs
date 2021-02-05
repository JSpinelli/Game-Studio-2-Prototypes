using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityLoweringThing : MonoBehaviour
{
    private bool triggered = false;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.tag == "Player")
            {
                triggered = true;
                gameManager.player_manager.decreaseSanity();
            }

        }

    }
}
