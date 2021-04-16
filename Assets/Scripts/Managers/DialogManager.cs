using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public AudioSource source;
    public TextMeshProUGUI dialogBox;

    private int currentPrio = 0;

    private void Start()
    {
        Services.EventManager.Register<DialogTriggered>(OnDialogTriggered);
        Services.EventManager.Register<SoundTriggered>(OnSoundTriggered);
        dialogBox.text = "";
    }

    private void OnDialogTriggered(GameEvent e)
    {
        DialogTriggered dialogInfo = (DialogTriggered) e;
        if (dialogInfo.dialog.Length > 0)
        {
            StartCoroutine(Countdown(dialogInfo.dialog,dialogInfo.timers,2));
        }

        if (dialogInfo.soundClip)
        {
            source.clip = dialogInfo.soundClip;
            source.Play();
        }
    }

    private void OnSoundTriggered(GameEvent e)
    {
        SoundTriggered dialogInfo = (SoundTriggered) e;
        if (dialogInfo.dialog.Length > 0)
        {
            dialogInfo.audioSource.clip = dialogInfo.soundClip;
            dialogInfo.audioSource.Play();
            StartCoroutine(Countdown(dialogInfo.dialog,dialogInfo.timers,1));
        }
    }

    private IEnumerator Countdown(string[] textToDisplay , float[] timer, int priority)
    {
        int timerIndex = 0;
        int oldPriority = currentPrio;
        foreach (var text in textToDisplay)
        {
            if (currentPrio <= priority)
            {
                currentPrio = priority;
                dialogBox.text = text;
            }
            yield return new WaitForSeconds(timer[timerIndex]);
            timerIndex++;
        }

        currentPrio = oldPriority;
        dialogBox.text = "";
    }
}
