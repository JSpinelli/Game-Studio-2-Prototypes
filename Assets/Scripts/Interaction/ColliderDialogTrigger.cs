using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDialogTrigger : MonoBehaviour
{
    public AudioClip[] clipToTrigger;
    public string[] subtitle;
    public float[] timers;
    
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        Services.EventManager.Fire(new DialogTriggered(subtitle,timers, clipToTrigger[0]));
        triggered = true;
    }
}
