using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : InteractableObject
{

    public Vector3 newRotation;
    public Vector3 newPosition;

    private Vector3 _originalRotation;
    private Vector3 _originalPosition;

    private bool _moved = false;

    private new void Start()
    {
        base.Start();
        _originalPosition = gameObject.transform.position;
    }

    public override bool CanInteract()
    {
        return true;
    }

    public override void OnInteract()
    {
        if (!_moved)
        {
            transform.Rotate(newRotation);
        }
        else
        {
            transform.Rotate(-newRotation);
        }

        _moved = !_moved;
    }
}
