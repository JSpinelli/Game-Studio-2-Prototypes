using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Note : MonoBehaviour
{

    public Image noteImage;
    
    // Start is called before the first frame update
    void Start()
    {
        noteImage.enabled = false;
    }

    public void ShowNoteImage()
    {
        noteImage.enabled = true;
    }

    public void HideNoteImage()
    {
        noteImage.enabled = false;
    }
}
