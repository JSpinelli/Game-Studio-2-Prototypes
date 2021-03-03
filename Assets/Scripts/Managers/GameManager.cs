using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI borednessMeter;
    public TextMeshProUGUI dialogBox;

    public float spookyIncreasePerTick;
    public float borednessIncreaseWhileNotMoving;
    public float borednessTickRate;

    public Transform playerPos;
    
    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;

    public GameObject wallChange;

    public GameObject babyToSpawn;
    private GameObject spawnedBaby;

    private GameObject activeBaby;

    public GameObject kitchenObjects;
    public GameObject livingRoomObjects;
    public GameObject hallwayObjects;
    public GameObject bathroomObjects;
    public GameObject bedroomObjects;

    public Transform[] babySpawns;

    public Light[] lights;

    public float textOnScreenTime = 8f;
    public float effect = 0.5f;

    private bool babySpawned = false;

    private float spookyLevel;

    private bool babyTeleported = false;


    private float borednessTimer;

    private Vector3 kitchenOriginalPos;

    private bool shooting = false;
    private bool wasLooking = false;
    private bool switched = false;
    private bool flipping = false;
    private bool levitate = false;

    private bool goingUp = true;

    private string[] dialogs =
    {
        "ads",
        "dsada"
    };

    void Awake()
    {
        _InitializeServices();
        spookyLevel = 0;
        borednessMeter.text = "";
        kitchenOriginalPos = kitchenObjects.transform.position;
        wallChange.SetActive(false);
    }

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
    }

    public void OnSpookyChange(GameEvent e)
    {
        
        if (babyBoreness == 20)
        {
            baby2.SetActive(baby1.activeSelf);
            baby1.SetActive(!baby1.activeSelf);
            activeBaby = baby1.activeSelf ? baby1 : baby2;
            // foreach (Light light1 in lights)
            // {
            //     light1.color = Color.blue;
            // }
        }        
        
        if (babyBoreness == 70)
        {
            wallChange.SetActive(true);
        }
        
        if (babyBoreness == 50)
        {
            baby3.SetActive(baby2.activeSelf);
            baby2.SetActive(!baby2.activeSelf);
            activeBaby = baby3;
            // foreach (Light light1 in lights)
            // {
            //     light1.color = Color.red;
            // }
        }
        
        if ((babyBoreness == 100 && !babyTeleported) && !babySpawned)
        {
           // StartCoroutine(Countdown("Baby: This sucks! Kill him! I'm bored"));
            babyTeleported = true;
            babyBoreness = 0;
            borednessMeter.text = "";
            SpawnBaby();
        }
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
                //OnSpookyChange(new SpookyMeterChange(borednessIncrease));
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
        foreach (Light light1 in lights)
        {
            light1.color = Color.white;
        }
    }
}