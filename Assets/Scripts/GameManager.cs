using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI borednessMeter;
    public TextMeshProUGUI dialogBox;

    public float borednessIncrease;
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

    private float babyBoreness;

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
        babyBoreness = 0;
        borednessMeter.text = "";
        kitchenOriginalPos = kitchenObjects.transform.position;
        wallChange.SetActive(false);
    }

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<BorednessChange>(OnBorednessChange);
    }

    public void OnBorednessChange(GameEvent e)
    {
        BorednessChange ev = (BorednessChange) e;
        babyBoreness += ev.calmnessChange;
        if (ev.calmnessChange > 0)
        {
            borednessMeter.text += "|";
        }
        else
        {
            if (borednessMeter.text.Length > 0)
                borednessMeter.text = borednessMeter.text.Substring(0,borednessMeter.text.Length - 1);
        }

        if (babyBoreness == 20)
        {
            baby2.SetActive(baby1.activeSelf);
            baby1.SetActive(!baby1.activeSelf);
            activeBaby = baby1.activeSelf ? baby1 : baby2;
            foreach (Light light1 in lights)
            {
                light1.color = Color.blue;
            }
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
            foreach (Light light1 in lights)
            {
                light1.color = Color.red;
            }
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
        //StartCoroutine(Countdown("Something is at my door! I hope is not a demon baby"));
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
                OnBorednessChange(new BorednessChange(borednessIncrease));
            }
        }
        
        if (babyBoreness == 20 && !shooting)
        {
            shooting = true;
            var colChildren = kitchenObjects.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colChildren)
            {
                collider.enabled = false;
            }
        }        
        
        if (babyBoreness == 40 && !flipping)
        {
            flipping = true;
        }        
        if (babyBoreness == 60 && !levitate)
        {
            levitate = true;
        }

        if (shooting)
        {
            var meshChildren = kitchenObjects.GetComponentsInChildren<MeshRenderer>();
            bool oneIsVisible = false;
            foreach (MeshRenderer meshes in meshChildren)
            {
                oneIsVisible = oneIsVisible || meshes.isVisible;
            }

            if (oneIsVisible)
            {
                kitchenObjects.transform.position = Vector3.Lerp(kitchenObjects.transform.position, playerPos.transform.position, Time.deltaTime / 5);
            }
            else
            {
                kitchenObjects.transform.position = kitchenOriginalPos;
            }
        }         
        if (levitate)
        {
            var meshChildren = hallwayObjects.GetComponentsInChildren<MeshRenderer>();
            bool oneIsVisible = false;
            foreach (MeshRenderer meshes in meshChildren)
            {
                oneIsVisible = oneIsVisible || meshes.isVisible;
            }

            if (oneIsVisible)
            {
                
                if (Vector3.Distance(hallwayObjects.transform.localPosition, new Vector3(0, 2, 0)) < 0.1f && goingUp)
                {
                    goingUp = false;
                }                
                if (Vector3.Distance(hallwayObjects.transform.localPosition, new Vector3(0, 2, 0)) > 1.9f && !goingUp)
                {
                    goingUp = true;
                }

                if ( goingUp)
                {
                    hallwayObjects.transform.localPosition = Vector3.Lerp(hallwayObjects.transform.localPosition, new Vector3(0,2,0), Time.deltaTime * effect); 
                }
                else
                {
                    hallwayObjects.transform.localPosition = Vector3.Lerp(hallwayObjects.transform.localPosition, new Vector3(0,0,0), Time.deltaTime * effect); 
                }
            }
        }        
        
        if (flipping)
        {
            var meshChildren = livingRoomObjects.GetComponentsInChildren<MeshRenderer>();
            bool oneIsVisible = false;
            foreach (MeshRenderer meshes in meshChildren)
            {
                oneIsVisible = oneIsVisible || meshes.isVisible;
            }

            if (!oneIsVisible && wasLooking)
            {
                if (!switched){
                    livingRoomObjects.transform.localScale = new Vector3(1, -1, 1);
                    livingRoomObjects.transform.localPosition = new Vector3(0, -4, 0);
                    switched = true;
                }
                else
                {
                    livingRoomObjects.transform.localScale = new Vector3(1, 1, 1);
                    livingRoomObjects.transform.localPosition = new Vector3(0, 0, 0);
                    switched = false;
                }
            }

            wasLooking = oneIsVisible;
        }

        // if (babyBoreness > 40)
        // {
        //     hallwayObjects.transform.rotation = Quaternion.Lerp(hallwayObjects.transform.rotation, Quaternion.Euler(Vector3.left), Time.deltaTime / 10);
        // }
    }

    private IEnumerator Countdown(string textToDisplay)
    {
        float duration = textOnScreenTime;
        float normalizedTime = 0;
        dialogBox.text = textToDisplay;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        dialogBox.text = "";
    }

    private void SpawnBaby()
    {
        babySpawned = true;
        activeBaby.SetActive(false);
        spawnedBaby = Instantiate(babyToSpawn, babySpawns[Random.Range(0, babySpawns.Length)].position,
            Quaternion.identity);
        spawnedBaby.GetComponent<PickUpBaby>().gm = this;
    }

    public void DeSpawnBaby()
    {
        activeBaby.SetActive(true);
        Destroy(spawnedBaby);
        babySpawned = false;
        babyTeleported = false;
        //StartCoroutine(Countdown("Baby: Ohhh, you found me, I guess we can play again"));
        foreach (Light light1 in lights)
        {
            light1.color = Color.white;
        }
    }
}