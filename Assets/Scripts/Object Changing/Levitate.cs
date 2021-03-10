using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : ObjectChange
{
    public float minY;
    public float maxY;

    public float timeToMove;
    private float timer;

    private Vector3 initialPos;
    private Vector3 objectivePos;
    private Vector3 currentObjective;
    private Vector3 objectivePos2;

    private bool goingToTarget = true;

    private void Start()
    {
        timer = timeToMove;
        initialPos = gameObject.transform.localPosition;
        objectivePos = currentObjective = new Vector3(initialPos.x, initialPos.y + minY, initialPos.z);
        objectivePos2 = new Vector3(initialPos.x, initialPos.y + maxY, initialPos.z);
        triggered = false;
    }

    private void Update()
    {
        if (startAtPlay || triggered)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, currentObjective, Time.deltaTime * (timeToMove/timer) ); 
            }
            else
            {
                goingToTarget = !goingToTarget;
                timer = timeToMove;
                currentObjective = goingToTarget ? objectivePos : objectivePos2;
            }
        }

        if (!triggered && !startAtPlay)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, initialPos, Time.deltaTime * (timeToMove/timer) ); 
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
