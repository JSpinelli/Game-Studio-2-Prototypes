using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public ObjectSwitch[] changedObjects;
    public float chanceOfSwitch = 5;
    private List<ObjectSwitch> possibleObjects;

    public Light[] roomLights;
    
    public float minflickerSpeed = 0.2f;
    public float maxflickerSpeed = 0.8f;
    public float timer = 0.7f;
                                          
    public float SpookyTreshold = 50;

    private delegate void DelegateAction();

    private DelegateAction actionToDo;
    

    public AudioSource windowTapping;

    private bool _playerInside = false;

    private void Start()
    {
        Services.EventManager.Register<SpookyMeterChange>(OnSpookyMeterChange);
        timer = Random.Range(minflickerSpeed, maxflickerSpeed);

        possibleObjects = new List<ObjectSwitch>(changedObjects);
    }

    private void Update()
    {
        if (!Services.gameManager.IsBabyActive()) return;
        if (_playerInside && Services.gameManager.ActiveSpookySpike())
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = Random.Range(minflickerSpeed, maxflickerSpeed);
                foreach (var light in roomLights)
                {
                    light.enabled = !light.enabled;
                }
            }
        }
    }

    private void OnSpookyMeterChange(GameEvent e)
    {
        SpookyMeterChange smc = (SpookyMeterChange) e;
        float chance = Random.Range(1f, 10f);
        if (chance < chanceOfSwitch && changedObjects.Length >0 && !_playerInside)
        {
            int randomIndex = Random.Range(0, possibleObjects.Count);
            possibleObjects[randomIndex].Change();
            if (possibleObjects[randomIndex].IsOver())
                possibleObjects.RemoveAt(randomIndex);
        }
        
        if (_playerInside)
        {
            if (windowTapping)
            {
                float chance1 = Random.Range(1, 10);
                if (chance1 > 8)
                {
                    windowTapping.Play();
                }
            }
        }

        if (smc.CurrentSpookyValue >= SpookyTreshold)
        {
            //effectActive = true;
        }
        else if (smc.CurrentSpookyValue < SpookyTreshold)
        {
            //effectActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInside = false;
    }
}