using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        Services.InputManager.PushAction("Pause",OnPause);
    }

    void OnPause()
    {
        Services.InputManager.PushAction("Pause",OnResume);
        Services.InputManager.PushAction("Interact",OnSelect);
        Services.InputManager.PushRangeAction(MoveCursor);
        Time.timeScale = 0;
    }

    void OnResume()
    {
        Time.timeScale = 1;
        Services.InputManager.PopAction("Pause");
        Services.InputManager.PopAction("Interact");
        Services.InputManager.PopRangeAction();
    }    
    
    void OnSelect()
    {
        Debug.Log("Selected OPTION");
    }

    void MoveCursor(float x,float y)
    {
    }
}
