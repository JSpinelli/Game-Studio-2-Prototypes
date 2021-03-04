using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public float spookyIncreasePerTick;
    public float spookyTickRate;

    public Transform playerPos;

    private bool babySpawned = false;

    private float _spookyLevel;

    private float _borednessTimer;


    void Awake()
    {
        _InitializeServices();
        _spookyLevel = 0;
    }

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
    }

    void Update()
    {
        if (!babySpawned)
        {
            if (_borednessTimer < spookyTickRate)
            {
                _borednessTimer += Time.deltaTime;
            }
            else
            {
                _borednessTimer = 0;
                _spookyLevel += spookyIncreasePerTick;
                Services.EventManager.Fire(new SpookyMeterChange(_spookyLevel));
            }
        }
    }

    // private void SpawnBaby()
    // {
    //     babySpawned = true;
    //     activeBaby.SetActive(false);
    //     spawnedBaby = Instantiate(babyToSpawn, babySpawns[Random.Range(0, babySpawns.Length)].position, Quaternion.identity);
    //     spawnedBaby.GetComponent<PickUpBaby>().gm = this;
    // }

    // public void DeSpawnBaby()
    // {
    //     activeBaby.SetActive(true);
    //     Destroy(spawnedBaby);
    //     babySpawned = false;
    //     babyTeleported = false;
    // }
}