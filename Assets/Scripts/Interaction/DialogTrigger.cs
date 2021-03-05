using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : InteractableObject
{
    public string text="";
    public AudioClip sound;
    void Start()
    {
        base.Start();
    }

    public override void OnInteract()
    {
        Services.EventManager.Fire(new DialogTriggered(text,sound));
    }
}
    
