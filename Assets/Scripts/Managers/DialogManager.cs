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
        source.clip = dialogInfo.soundClip;
        source.Play();
        StartCoroutine(Countdown(dialogInfo.dialog));
    }
    
    private IEnumerator Countdown(string textToDisplay)
    {
        float duration = textOnScreenTime;
        float normalizedTime = 0;
        dialogBox.text = textToDisplay;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        dialogBox.text = "";
    }
}
