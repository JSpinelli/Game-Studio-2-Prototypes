using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Dictionary<string, Stack<Action>> _actionMapping;
    
    public delegate void RangeAction(float x,float y);
    
    private Stack<RangeAction> _rangeMovementMapping;
    private Stack<RangeAction> _rangeCameraMapping;
    
    public string[] activeKeys;
    public string[] correspondingAction;

    private float _inputX;
    private float _inputY;    
    
    private float _inputXCamera;
    private float _inputYCamera;
    private void Awake()
    {
        Services.InputManager = this;
        _actionMapping = new Dictionary<string, Stack<Action>>();
        _rangeMovementMapping = new Stack<RangeAction>();
        _rangeCameraMapping = new Stack<RangeAction>();
        LockCursor(true);
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f )
        {
            LockCursor(true);
        }
    
        // unlock when escape is hit
        if  ( Input.GetKeyDown(KeyCode.Escape) )
        {
            LockCursor(!Screen.lockCursor);
        }
        
        for (int i = 0; i < activeKeys.Length; i++)
        {
            if (Input.GetKeyDown(activeKeys[i]))
            {
                Stack<Action> actionsToTrigger;
                if (_actionMapping.TryGetValue(correspondingAction[i], out actionsToTrigger))
                {
                    actionsToTrigger.Peek().Invoke();
                }
            }
        }

        if (_rangeMovementMapping.Count != 0)
        {
            _inputX = Input.GetAxis("Horizontal");
            _inputY = Input.GetAxis("Vertical");
            _rangeMovementMapping.Peek().Invoke(_inputX,_inputY);
        }        
        if (_rangeCameraMapping.Count != 0)
        {
            _inputXCamera = Input.GetAxis("Mouse X");
            _inputYCamera = Input.GetAxis("Mouse Y");
            _rangeCameraMapping.Peek().Invoke(_inputXCamera,_inputYCamera);
        }
    }
    
    public void LockCursor(bool lockCursor)
    {
        Screen.lockCursor = lockCursor;
    }

    public void PushAction(string action, Action a)
    {
        Stack<Action> actions;
        if (!_actionMapping.TryGetValue(action,out actions))
        {
            actions = new Stack<Action>();
            actions.Push(a);
            _actionMapping.Add(action,actions);
        }
        else
        {
            actions.Push(a);
        }
    }

    public void PopAction(string action)
    {
        Stack<Action> actions;
        if (_actionMapping.TryGetValue(action,out actions))
        {
            actions.Pop();
        }
    }
    
    public void PushRangeAction(RangeAction a)
    {
        _rangeMovementMapping.Push(a);
    }    
    
    public void PopRangeAction()
    {
        _rangeMovementMapping.Pop();
    }    
    
    public void PushRangeActionCamera(RangeAction a)
    {
        _rangeCameraMapping.Push(a);
    }    
    
    public void PopRangeActionCamera()
    {
        _rangeCameraMapping.Pop();
    }
}
