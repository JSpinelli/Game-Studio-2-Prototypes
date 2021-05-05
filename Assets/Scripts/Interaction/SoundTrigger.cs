using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : InteractableObject
{
    public AudioSource source;
    public bool InteractWillTurnOff = false;
    private bool _interacted = false;
    public Dialog clip;

    public bool hasMultipleLines = false;
    public Dialog[] clips;
    private int clipCounter = 0;

    private new void Start()
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
            _interacted = true;
        }
        else
        {
            if (hasMultipleLines)
            {
                Services.EventManager.Fire(new SoundTriggered(clips[clipCounter].line,clips[clipCounter].screenTime,clips[clipCounter].clip,source));
                clipCounter++;
                if (clipCounter == clips.Length)
                {
                    _interacted = true;
                }
            }
            else
            {
                Services.EventManager.Fire(new SoundTriggered(clip.line,clip.screenTime,clip.clip,source));
                _interacted = true;
            }
        }
        Services.EventManager.Fire(new InteractionTriggered(gameObject.name));
    }
}
