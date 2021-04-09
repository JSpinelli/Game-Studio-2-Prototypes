using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Dictionary<string, Stack<Action>> actionMapping;

    public string[] activeKeys;
    public string[] correspondingAction;

    private void Awake()
    {
        Services.InputManager = this;
        actionMapping = new Dictionary<string, Stack<Action>>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activeKeys.Length; i++)
        {
            if (Input.GetKeyDown(activeKeys[i]))
            {
                Stack<Action> actionsToTrigger;
                if (actionMapping.TryGetValue(correspondingAction[i], out actionsToTrigger))
                {
                    actionsToTrigger.Peek().Invoke();
                }
            }
        }
    }

    public void PushAction(string action, Action a)
    {
        Stack<Action> actions;
        if (!actionMapping.TryGetValue(action,out actions))
        {
            actions = new Stack<Action>();
            actions.Push(a);
            actionMapping.Add(action,actions);
        }
        else
        {
            actions.Push(a);
        }
    }

    public void PopAction(string action)
    {
        Stack<Action> actions;
        if (actionMapping.TryGetValue(action,out actions))
        {
            actions.Pop();
        }
    }
}
