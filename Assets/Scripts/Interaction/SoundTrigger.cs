﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : InteractableObject
{
    public AudioSource source;
    public bool InteractWillTurnOff = false;
    private bool _interacted = false;
    void Start()
    {
        base.Start();
        _interacted = Services.gameManager.InteractionTriggered(gameObject.name);
    }

    public override bool CanInteract()
    {
        return !_interacted;
    }

    public override void OnInteract()
    {
        if (_interacted) return;
        if (InteractWillTurnOff)
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }
        Services.gameManager.AddTriggeredInteraction(gameObject.name);
        _interacted = true;
    }
}
