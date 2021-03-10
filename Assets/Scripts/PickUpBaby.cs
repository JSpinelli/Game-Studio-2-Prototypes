using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBaby : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //gm.DeSpawnBaby();
        }

    }
}
