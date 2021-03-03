using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public float spookyIncreasePerTick;
    public float borednessTickRate;

    public Transform playerPos;
    
    public GameObject baby1;

    public GameObject babyToSpawn;
    private GameObject spawnedBaby;

    private GameObject activeBaby;

    public Transform[] babySpawns;

    private bool babySpawned = false;

    private float spookyLevel;

    private bool babyTeleported = false;
    
    private float borednessTimer;
    
    
    void Awake()
    {
        _InitializeServices();
        spookyLevel = 0;
    }

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        activeBaby = baby1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!babySpawned)
        {
            if (borednessTimer < borednessTickRate)
            {
                borednessTimer += Time.deltaTime;
            }
            else
            {
                borednessTimer = 0;
                spookyLevel += spookyIncreasePerTick;
                Services.EventManager.Fire(new SpookyMeterChange(spookyLevel));
            }
        }
    }

    private void SpawnBaby()
    {
        babySpawned = true;
        activeBaby.SetActive(false);
        spawnedBaby = Instantiate(babyToSpawn, babySpawns[Random.Range(0, babySpawns.Length)].position, Quaternion.identity);
        spawnedBaby.GetComponent<PickUpBaby>().gm = this;
    }

    public void DeSpawnBaby()
    {
        activeBaby.SetActive(true);
        Destroy(spawnedBaby);
        babySpawned = false;
        babyTeleported = false;
    }
}