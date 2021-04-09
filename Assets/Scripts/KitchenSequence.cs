using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KitchenSequence : MonoBehaviour
{
    public Transform player;
    public Transform placeForPickedUpObjects;
    
    public Dialog[] sequence;
    public Dialog[] fails;

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

    private int failCounter;
    private int sequenceCounter;

    private string[] currentPuzzle;

    private List<GameObject> objectsInBlender;

    public GameObject oldKitchen;
    public GameObject newKitchen;

    private GameObject objectOnHand;
    public bool objectInHand = false;

    public void StartSequence()
    {
        SetObjects();
        Services.EventManager.Register<ObjectPickedUp>(OnObjectPickedUp);
        Services.KitchenSequence = this;
        // oldKitchen.SetActive(false);
        // newKitchen.SetActive(true);
        failCounter = 0;
        sequenceCounter = 0;
        puzzleStarted = true;
        objectsInBlender = new List<GameObject>();
        currentPuzzle = null;
        if (puzzleStarted && !puzzle1Complete)
            currentPuzzle = puzzle1Solution;
        if (puzzleStarted && puzzle1Complete && !puzzle2Complete)
            currentPuzzle = puzzle2Solution;
        if (puzzleStarted && puzzle1Complete && puzzle2Complete && !puzzle3Complete)
            currentPuzzle = puzzle3Solution;
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
            obj.SetActive(puzzleStarted);
        }
        foreach (var obj in objectsForPuzzle2)
        {
            obj.SetActive(puzzle1Complete);
        }
        foreach (var obj in objectsForPuzzle3)
        {
            obj.SetActive(puzzle2Complete);
        }
    }

    private void PlayFail()
    {
        Services.EventManager.Fire( new DialogTriggered(fails[failCounter].line, fails[failCounter].screenTime, fails[failCounter].clip));
        failCounter++;
    }    
    
    private void PlaySequence()
    {
        Services.EventManager.Fire( new DialogTriggered(sequence[sequenceCounter].line, sequence[sequenceCounter].screenTime, sequence[sequenceCounter].clip));
        sequenceCounter++;
    }

    public void PutObject()
    {
        objectsInBlender.Add(objectOnHand);
        objectOnHand.SetActive(false);
        objectOnHand = null;
        objectInHand = false;
        if (objectsInBlender.Count == currentPuzzle.Length)
        {
            CheckObjects();
        }
    }

    public void CheckObjects()
    {

        if (currentPuzzle == null)
        {
            Debug.LogWarning("Checking against empty puzzle");
            return;
        }

        bool succesSum = true;
        foreach (var obj in objectsInBlender)
        {
            succesSum = succesSum && currentPuzzle.Any((name) => { return name == obj.name; });
        }

        if (succesSum)
        {
            Debug.Log("Puzzle Completed");
            PlaySequence();
        }
        else
        {
            PlayFail();
            foreach (var obj in objectsInBlender)
            {
                obj.GetComponent<Pickupable>().Reset();
            }
            
        }
    }
}