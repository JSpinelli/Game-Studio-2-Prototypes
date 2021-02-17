using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public PlayerManager player_manager;


    public TextMeshProUGUI calmnessMeter;
    public TextMeshProUGUI dialogBox;

    public float initialCalmness;
    public float calmnessDecreaseRate;

    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;

    public GameObject babyToSpawn;
    private GameObject spawnedBaby;

    private GameObject activeBaby;

    public Transform[] babySpawns;

    public float textOnScreenTime = 8f;

    private bool babySpawned = false;

    private bool firstStepTriggered = false;
    private bool secondStepTriggered = false;

    private string[] dialogs = {
        "ads",
        "dsada"
    };

    void Awake()
    {
        _InitializeServices();
    }
    
    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<CalmnessDecreased>(OnCalmDecrease);
    }

    public void OnCalmDecrease(GameEvent e)
    {
        CalmnessDecreased ev = (CalmnessDecreased) e;
        // if (ev.currentCalmness == 70)
        // {
        //     baby2.SetActive(baby1.activeSelf);
        //     baby1.SetActive(!baby1.activeSelf);
        //     activeBaby = baby1.activeSelf ? baby1 : baby2;
        // }        
        // if (ev.currentCalmness == 30)
        // {
        //     baby3.SetActive(baby2.activeSelf);
        //     baby2.SetActive(!baby2.activeSelf);
        //     activeBaby = baby3;
        // }        
        // if (ev.currentCalmness == 70)
        // {
        // }        
        // if (ev.currentCalmness == 70)
        // {
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        player_manager = new PlayerManager(initialCalmness, calmnessDecreaseRate, calmnessMeter);
        StartCoroutine(Countdown("Something is at my door! I hope is not a demon baby"));
        activeBaby = baby1;
    }

    // Update is called once per frame
    void Update()
    {
        if (baby1.activeSelf && player_manager.calmness < 70)
        {
            baby1.SetActive(false);
            baby2.SetActive(true);
            activeBaby = baby2;
        }
        if (baby2.activeSelf && player_manager.calmness < 30)
        {
            baby2.SetActive(false);
            baby3.SetActive(true);
            activeBaby = baby3;
        }
        if (baby2.activeSelf && player_manager.calmness >= 70)
        {
            baby1.SetActive(true);
            baby2.SetActive(false);
            activeBaby = baby1;
        }
        if (baby3.activeSelf && player_manager.calmness >= 30)
        {
            baby2.SetActive(true);
            baby3.SetActive(false);
            activeBaby = baby2;
        }

        if ((player_manager.calmness == 80 && !firstStepTriggered) && !babySpawned){
            firstStepTriggered = true;
            SpawnBaby();
        }
        if ( (player_manager.calmness == 40 && !secondStepTriggered) && !babySpawned){
            secondStepTriggered = true;
            SpawnBaby();
        }

        if (player_manager.calmness == 50 && secondStepTriggered){
            secondStepTriggered = false;
        }

        if (player_manager.calmness == 90 && firstStepTriggered){
            firstStepTriggered = false;
        }
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
        spawnedBaby= Instantiate(babyToSpawn, babySpawns[Random.Range(0,babySpawns.Length)].position , Quaternion.identity);
        spawnedBaby.GetComponent<PickUpBaby>().gm = this;

    }

    public void DeSpawnBaby()
    {
        activeBaby.SetActive(true);
        Destroy(spawnedBaby);
        babySpawned = false;
    }
}
