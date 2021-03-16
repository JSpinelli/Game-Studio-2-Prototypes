using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public AudioSource source;
    public TextMeshProUGUI dialogBox;
    public float textOnScreenTime;

    private void Start()
    {
        Services.EventManager.Register<DialogTriggered>(OnDialogTriggered);
    }

    private void OnDialogTriggered(GameEvent e)
    {
        DialogTriggered dialogInfo = (DialogTriggered) e;
        if (dialogInfo.soundClip)
        {
            source.clip = dialogInfo.soundClip;
            source.Play();
        }
        // if (dialogInfo.dialog.Length > 0)
        //     StartCoroutine(Countdown(dialogInfo.dialog,dialogInfo.timers));
    }
    
    private IEnumerator Countdown(string[] textToDisplay , float[] timer)
    {
        int timerIndex = 0;
        foreach (var text in textToDisplay)
        {
            dialogBox.text = text;
            yield return new WaitForSeconds(timer[timerIndex]);
            timerIndex++;
        }
        dialogBox.text = "";
    }
}
