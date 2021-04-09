using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public float spookyIncreasePerTick;
    public float spookyTickRate;
    public PostProcessProfile profile;

    public bool startWithBaby = false;

    public GameObject begginingObjects;
    public GameObject doorBellRinging;
    public GameObject babyPickedUp;

    public KitchenSequence kitchenSequence;

    private float _borednessTimer;

    public TextAsset savedData;
    
    private float _spookyLevel;
    private int _currentTask;
    
    private List<int> _completedTasks;
    private List<string> _triggeredInteractions;

    public AudioSource doorBell;

    public float DoorBellTimer = 0;
    private float _doorBellTimer = 0;
    public int InteractionTreshold = 3;
    private int _interactionCount = 0;
    
    //State Managment
    private bool _babyActive = false;
    private bool _spookySpike = false;

    public GameObject BabyOutside;
    public GameObject BabyInArms;

    public Movable[] doors;

    void Awake()
    {
        _InitializeServices();
        if (savedData)
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(savedData.text);
            _spookyLevel = saveData.spookyMeter;
            _currentTask = saveData.currentTask;
            _completedTasks = new List<int>(saveData.completedTask);
            _triggeredInteractions = new List<string>(saveData.triggeredInteractions);
        }
        else
        {
            _spookyLevel = 0;
            _currentTask = 0;
            _completedTasks = new List<int>();
            _triggeredInteractions = new List<string>();
        }

        _borednessTimer = 0;
        if (!startWithBaby)
        {
            BabyOutside.SetActive(false);
            BabyInArms.SetActive(false);
            begginingObjects.SetActive(true);
            doorBellRinging.SetActive(false);
            babyPickedUp.SetActive(false);
            kitchenSequence.StartSequence();
        }
        else
        {
            BabyOutside.SetActive(false);
            BabyInArms.SetActive(true);
            _babyActive = true;
            begginingObjects.SetActive(false);
            doorBellRinging.SetActive(false);
            babyPickedUp.SetActive(false);
        }
    }

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<InteractionTriggered>(AddTriggeredInteraction);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnSave();
        }
        if (_babyActive)
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

            if (_spookyLevel > 100)
            {
                CloseDoors();
                _spookySpike = true;
                _spookyLevel = 0;
                StartCoroutine(GrainEffect(3));
                Services.EventManager.Fire(new SpookyMeterChange(_spookyLevel));
            }
        }
        if ( !_babyActive && _interactionCount > InteractionTreshold)
        {
            if (DoorBellTimer > _doorBellTimer)
            {
                _doorBellTimer += Time.deltaTime;
            }
            else
            {
                _doorBellTimer = 0;
                BabyOutside.SetActive(true);
                doorBell.Play();
                begginingObjects.SetActive(false);
                doorBellRinging.SetActive(true);
            }
        }
    }

    void OnSave()
    {
        SaveData saveData = new SaveData();
        saveData.completedTask = _completedTasks.ToArray();
        saveData.currentTask = _currentTask;
        saveData.triggeredInteractions = _triggeredInteractions.ToArray();
        saveData.spookyMeter = _spookyLevel;
        string json = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(Application.dataPath + "/Saves/" + "spooky_data.json", json);
        Debug.Log("Game Saved");
        
    }

    public void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.Reset(true);
        }
    }

    public void AddTriggeredInteraction(GameEvent interaction)
    {
        InteractionTriggered inter = (InteractionTriggered) interaction;
        if (!_triggeredInteractions.Exists((item) => item == inter.name))
        {
            _triggeredInteractions.Add(inter.name);
        }

        _interactionCount++;

        if (inter.name == "BabyOutside")
        {
            //_babyActive = true;
            BabyOutside.SetActive(false);
            BabyInArms.SetActive(true);
            doorBellRinging.SetActive(false);
            babyPickedUp.SetActive(true);
            kitchenSequence.StartSequence();
        }
    }
    
    public bool InteractionTriggered(string interaction)
    {
        return _triggeredInteractions.Exists((item) => item == interaction);
    }

    public bool IsBabyActive()
    {
        return _babyActive;
    }

    public bool ActiveSpookySpike()
    {
        return _spookySpike;
    }
    
    private IEnumerator GrainEffect(float t)
    {
        Grain gr = profile.GetSetting<Grain>();
        gr.active = true;
        yield return new WaitForSeconds(t);
        gr.active = false;
    }
}