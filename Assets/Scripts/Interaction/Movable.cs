using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : InteractableObject
{

    public Vector3 newRotation;
    public Vector3 newPosition;

    private Vector3 _originalRotation;

    public AudioSource sound;

    private bool _moved = false;

    private new void Start()
    {
        base.Start();
        _originalRotation = transform.rotation.eulerAngles;
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
        if(sound) sound.Play();
        _moved = !_moved;
    }

    public void Reset(bool triggerSound)
    {
        _moved = false;
        transform.Rotate(_originalRotation);
        if (triggerSound && sound) sound.Play();
    }
}
