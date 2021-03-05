using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : InteractableObject
{
    public AudioSource source;
    public bool InteractWillTurnOff = false;
    void Start()
    {
        
    }

    public override void OnInteract()
    {
        if (InteractWillTurnOff)
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }
    }
}
