using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RadioHostMovement : MonoBehaviour
{
    public Transform[] pathToMoveThrough;

    private bool going = true;

    private int currentObjective = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = pathToMoveThrough[0].localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (going)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,
                pathToMoveThrough[currentObjective + 1].localPosition, Time.deltaTime);
            if (Vector2.Distance(gameObject.transform.localPosition, pathToMoveThrough[currentObjective + 1].localPosition) <
                0.1f)
            {
                currentObjective = currentObjective + 1;
                if (currentObjective == pathToMoveThrough.Length - 1)
                {
                    going = false;
                }
            }
        }
        else
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,
                pathToMoveThrough[currentObjective - 1].localPosition, Time.deltaTime);
            if (Vector2.Distance(gameObject.transform.localPosition, pathToMoveThrough[currentObjective - 1].localPosition) <
                0.1f)
            {
                currentObjective = currentObjective - 1;
                if (currentObjective == 0)
                {
                    going = true;
                }
            }
        }
    }
}