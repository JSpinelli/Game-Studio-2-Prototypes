using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositChair : InteractableObject
{
    public GameObject spawnBaby;
    
    public override bool CanInteract()
    {
        return Services.gameManager.BabyInArms.activeSelf;
    }

    public override void OnInteract()
    {
        spawnBaby.SetActive(true);
        Services.KitchenSequence.StartScene();
    }
    
}