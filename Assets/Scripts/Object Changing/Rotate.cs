using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Rotate : ObjectChange
{
    public float x;
    public float y;
    public float z;

    public float timeToMove;
    private float timer;

    private Quaternion initialQuaternion;
    private Quaternion objectiveQuaternion;
    private Quaternion currentObjective;

    private bool goingToTarget = true;

    private void Start()
    {
        timer = timeToMove;
        initialQuaternion = gameObject.transform.localRotation;
        objectiveQuaternion  = currentObjective = Quaternion.Euler(x, y, z);
        triggered = false;
    }

    private void Update()
    {
        if (startAtPlay || triggered)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, currentObjective, Time.deltaTime * (timeToMove/timer) ); 
            }
            else
            {
                goingToTarget = !goingToTarget;
                timer = timeToMove;
                currentObjective = goingToTarget ? objectiveQuaternion : initialQuaternion;
            }
        }
    }

    public override void Change()
    {
        triggered = true;
    }

    public override void Reset()
    {
        triggered = false;
    }
}