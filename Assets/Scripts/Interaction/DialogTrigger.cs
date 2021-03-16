using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : InteractableObject
{
    public string[] text;
    public float[] timers;
    public AudioClip[] lines;
    public int[] breakdownOfLines;
    private int lineCounter;
    void Start()
    {
        base.Start();
        lineCounter = 0;
    }

    public override bool CanInteract()
    {
        return lineCounter < lines.Length;
    }

    public override void OnInteract()
    {
        
        if (lineCounter == lines.Length) return;
        Services.EventManager.Fire(new DialogTriggered(text,timers, lines[lineCounter]));
        Services.EventManager.Fire(new InteractionTriggered(gameObject.name));
        lineCounter++;
    }
}
    
