using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    private bool _triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!_triggered)
        {
            Services.gameManager.CloseDoors();
            Services.gameManager.GrainEffect();
            _triggered = true;
        }
    }
}
