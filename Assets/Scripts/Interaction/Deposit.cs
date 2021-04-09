using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposit : InteractableObject
{
    
    public override bool CanInteract()
    {
        return Services.KitchenSequence.objectInHand;
    }

    public override void OnInteract()
    {
        Services.KitchenSequence.PutObject();
    }
    
}
