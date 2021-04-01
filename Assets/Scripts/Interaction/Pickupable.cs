using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : InteractableObject
{
    public bool shouldBeDisplayed = true;
    private bool isOnHand = false;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Transform originalParent;
    
    private new void Start()
    {
        base.Start();
        gameObject.SetActive(shouldBeDisplayed);
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
        originalParent = gameObject.transform.parent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Reset();
        }
    }

    public override bool CanInteract()
    {
        return !isOnHand;
    }

    public override void OnInteract()
    {
        Services.EventManager.Fire(new ObjectPickedUp(gameObject));
        isOnHand = true;
    }

    public void Reset()
    {
        gameObject.transform.parent = originalParent;
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = initialRotation;
        isOnHand = false;
    }
}
