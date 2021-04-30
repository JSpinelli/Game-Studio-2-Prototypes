using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KitchenSequence : MonoBehaviour
{
    public bool skipDialog;
    
    public Transform player;
    public Transform placeForPickedUpObjects;

    public float timeBetweenResponses = 1;
    
    public Dialog[] initialSequence;
    public Dialog[] firstPuzzleSolved;
    public Dialog[] firstPuzzleSolvedpart2;
    public Dialog[] secondPuzzleSolved;

    private bool isPlayingSequence = false;
    
    public Dialog[] fails;
    public Dialog[] filler;

    public float fillerTimer;
    private float _fillerTimerCounter;

    public bool puzzleStarted = false;

    public bool puzzle1Complete = false;
    public bool puzzle2Complete = false;
    public bool puzzle3Complete = false;

    public GameObject[] objectsForPuzzle1;
    public GameObject[] objectsForPuzzle2;
    public GameObject[] objectsForPuzzle3;

    public string[] puzzle1Solution;
    public string[] puzzle2Solution;
    public string[] puzzle3Solution;

    private int _failCounter;
    private int _fillerCounter;

    private string[] _currentPuzzle;

    private List<GameObject> _objectsInBlender;

    public GameObject oldKitchen;
    public GameObject newKitchen;

    private GameObject objectOnHand;
    public bool objectInHand = false;

    public GameObject house;
    public GameObject wall;
    public GameObject outsideLights;
    public GameObject stageColliders;

    public AudioSource stageSounds;
    public AudioSource host;
    public AudioSource pc;
    public AudioSource baby;

    public AudioSource footsteps;
    

    public AudioClip laughter;
    public AudioClip boo;
    public AudioClip wow;
    public AudioClip clap;

    public GameObject table;
    public GameObject blender;
    public GameObject cauldron;
    
    public void StartSequence()
    {
        
        Services.EventManager.Register<ObjectPickedUp>(OnObjectPickedUp);
        Services.KitchenSequence = this;
        oldKitchen.SetActive(false);
        newKitchen.SetActive(true);
        house.SetActive(true);
        wall.SetActive(false);
        puzzleStarted = false;
    }

    private void PlayLaughter()
    {
        stageSounds.clip = laughter;
        stageSounds.Play();
    }

    private void PlayBoos()
    {
        stageSounds.clip = boo;
        stageSounds.Play();
    }
    
    private void PlayWow()
    {
        stageSounds.clip = wow;
        stageSounds.Play();
    }
    
    private void PlayClap()
    {
        stageSounds.clip = clap;
        stageSounds.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        if (puzzleStarted) return;
        house.SetActive(false);
        wall.SetActive(true);
        _failCounter = 0;
        _fillerCounter = 0;
        _fillerTimerCounter = 0;
    }

    public void StartScene()
    {
        Services.gameManager.BabyInArms.SetActive(false);
        puzzleStarted = true;
        _objectsInBlender = new List<GameObject>();
        _currentPuzzle = null;
        
        if (puzzleStarted && !puzzle1Complete)
            _currentPuzzle = puzzle1Solution;
        if (puzzleStarted && puzzle1Complete && !puzzle2Complete)
            _currentPuzzle = puzzle2Solution;
        if (puzzleStarted && puzzle1Complete && puzzle2Complete && !puzzle3Complete)
            _currentPuzzle = puzzle3Solution;
        
        stageColliders.SetActive(true);
        outsideLights.SetActive(false);
        footsteps.Play();
        StartCoroutine(PlaySequence(initialSequence, SetObjects));
    }

    public void OnObjectPickedUp(GameEvent gameEvent)
    {
        objectOnHand = ((ObjectPickedUp) gameEvent).objectToMove;
        objectOnHand.transform.parent = player;
        
        objectOnHand.transform.position = placeForPickedUpObjects.position;
        objectOnHand.transform.rotation = placeForPickedUpObjects.rotation;
        objectInHand = true;
    }


    private void SetObjects()
    {
        foreach (var obj in objectsForPuzzle1)
        {
            obj.SetActive(puzzleStarted && !puzzle1Complete);
        }
        foreach (var obj in objectsForPuzzle2)
        {
            obj.SetActive(puzzle1Complete && !puzzle2Complete);
        }
        foreach (var obj in objectsForPuzzle3)
        {
            obj.SetActive(puzzle2Complete && !puzzle3Complete);
        }
    }

    private void PlayFail()
    {
        Services.EventManager.Fire( new DialogTriggered(fails[_failCounter].line, fails[_failCounter].screenTime, fails[_failCounter].clip));
        _failCounter++;
        if (_failCounter == fails.Length)
        {
            _failCounter = 0;
            
        }
    }

    private AudioSource GetCorrectSource(Dialog dialog)
    {
        AudioSource origin = null;
        if (dialog.source == "PC")
        {
            origin = pc;
        }
        if (dialog.source == "Audience")
        {
            origin = stageSounds;
        }
        if (dialog.source == "RadioHost")
        {
            origin = host;
        }
        if (dialog.source == "Baby")
        {
            origin = baby;
        }

        return origin;
    }

    private IEnumerator PlaySequence(Dialog[] sequence, Action afterSequence)
    {
        if (skipDialog)
        {
            yield return null;
            afterSequence.Invoke();
        }
        else
        {
            isPlayingSequence = true;
            foreach (Dialog dialog in sequence)
            {
                if (dialog.clip != null)
                {
                    Services.EventManager.Fire( new DialogTriggered(dialog.line, dialog.screenTime, dialog.clip,GetCorrectSource(dialog)));
                    yield return new WaitForSeconds(dialog.clip.length);
                    yield return new WaitForSeconds(timeBetweenResponses);
                }
            }
            isPlayingSequence = false;
            afterSequence.Invoke(); 
        }
    }
    
    private void PlayFiller()
    {
        Services.EventManager.Fire( new DialogTriggered(filler[_fillerCounter].line, filler[_fillerCounter].screenTime, filler[_fillerCounter].clip));
        _fillerCounter++;
    }

    public void PutObject()
    {
        _objectsInBlender.Add(objectOnHand);
        objectOnHand.SetActive(false);
        objectOnHand = null;
        objectInHand = false;
        if (_objectsInBlender.Count == _currentPuzzle.Length)
        {
            CheckObjects();
        }
    }

    private void MoveMonster()
    {
        StartCoroutine(PlaySequence(firstPuzzleSolvedpart2, SetObjects));
    }

    private void SetPuzzle3()
    {
        table.SetActive(false);
        blender.SetActive(false);
        cauldron.SetActive(true);
        SetObjects();
    }

    public void CheckObjects()
    {

        if (_currentPuzzle == null)
        {
            Debug.LogWarning("Checking against empty puzzle");
            return;
        }

        bool successesSum = true;
        foreach (var obj in _objectsInBlender)
        {
            successesSum = successesSum && _currentPuzzle.Any((s) => s == obj.name);
        }
        if (successesSum)
        {
            PlayClap();
            if (puzzleStarted && !puzzle1Complete)
            {
                puzzle1Complete = true;
                _currentPuzzle = puzzle2Solution;
                StartCoroutine(PlaySequence(firstPuzzleSolved, MoveMonster));
            }else if (puzzleStarted && puzzle1Complete && !puzzle2Complete)
            {
                puzzle2Complete = true;
                _currentPuzzle = puzzle3Solution;
                StartCoroutine(PlaySequence(secondPuzzleSolved, SetPuzzle3));
            } else if (puzzleStarted && puzzle1Complete && puzzle2Complete && !puzzle3Complete)
            {
                puzzle3Complete = true;
                // END OF SCENE
            }
        }
        else
        {
            Debug.Log("Puzzle Failed");
            PlayFail();
            foreach (var obj in _objectsInBlender)
            {
                obj.GetComponent<Pickupable>().Reset();
            }
        }
        _objectsInBlender.Clear();
    }

    private void Update()
    {
        if (!puzzleStarted) return;
        if (fillerTimer > _fillerTimerCounter)
        {
            _fillerTimerCounter += Time.deltaTime;
        }
        else
        {
            _fillerTimerCounter = 0;
            //PlayFiller();
        }
    }
}