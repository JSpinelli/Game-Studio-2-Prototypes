using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        Services.EventManager.Fire(new DialogTriggered(dialog.line,dialog.screenTime, dialog.clip));
        triggered = true;
    }
}
