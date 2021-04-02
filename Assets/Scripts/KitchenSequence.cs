using System.Linq;
using UnityEngine;

public class KitchenSequence : MonoBehaviour
{
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

    private void Start()
    {
        SetObjects();
        failCounter = 0;
        sequenceCounter = 0;
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

    public void CheckObject(Pickupable obj)
    {
        string[] currentPuzzle = null;
        if (puzzleStarted && !puzzle1Complete)
            currentPuzzle = puzzle1Solution;
        if (puzzleStarted && puzzle1Complete && !puzzle2Complete)
            currentPuzzle = puzzle2Solution;
        if (puzzleStarted && puzzle1Complete && puzzle2Complete && !puzzle3Complete)
            currentPuzzle = puzzle3Solution;

        if (currentPuzzle == null)
        {
            Debug.LogWarning("Checking against empty puzzle");
            return;
        }

        if (currentPuzzle.Any((name) => { return name == obj.name; }))
        {
            Debug.Log("Sucess");
        }
        else
        {
            obj.Reset();
        }
    }
}