using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Dictionary<string, List<Action>> actionMapping;

    public string[] activeKeys;
    public string[] correspondingAction;

    private void Awake()
    {
        Services.InputManager = this;
        actionMapping = new Dictionary<string, List<Action>>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activeKeys.Length; i++)
        {
            if (Input.GetKeyDown(activeKeys[i]))
            {
                List<Action> actionsToTrigger;
                if (actionMapping.TryGetValue(correspondingAction[i], out actionsToTrigger))
                    foreach (var action in actionsToTrigger)
                    {
                        action.Invoke();
                    }
            }
        }
        foreach (var key in activeKeys)
        {

        }
    }
}
