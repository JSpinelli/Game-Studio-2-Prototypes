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

    public Dialog[] dialogs;

    private new void Start()
    {
        base.Start();
        lineCounter = 0;
    }

    public override bool CanInteract()
    {
        return lineCounter < dialogs.Length;
    }

    public override void OnInteract()
    {
        
        if (lineCounter == lines.Length) return;
        Services.EventManager.Fire(new DialogTriggered(dialogs[lineCounter].line,dialogs[lineCounter].screenTime, dialogs[lineCounter].clip));
        Services.EventManager.Fire(new InteractionTriggered(gameObject.name));
        lineCounter++;
    }
}
    
