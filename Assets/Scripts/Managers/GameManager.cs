using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float spookyIncreasePerTick;
    public float spookyTickRate;

    public Transform playerPos;

    private bool babySpawned = false;

    

    private float _borednessTimer;

    public TextAsset savedData;
    
    private float _spookyLevel;
    private int _currentTask;
    private List<int> _completedTasks;
    private List<string> _triggeredInteractions;

    void Awake()
    {
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
        _InitializeServices();
    }

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OnSave();
        }
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

    public void AddTriggeredInteraction(string interaction)
    {
        if (!_triggeredInteractions.Exists((item) => item == interaction))
        {
            _triggeredInteractions.Add(interaction);
        }
    }

    public bool InteractionTriggered(string interaction)
    {
        return _triggeredInteractions.Exists((item) => item == interaction);
    }
}