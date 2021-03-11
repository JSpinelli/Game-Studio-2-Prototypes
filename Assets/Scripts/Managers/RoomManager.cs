using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public GameObject[] objectsInRoom;

    private GameObject[] changedObjects;

    public Light[] roomLights;

    public bool flickering = false;
    public float minflickerSpeed = 0.2f;
    public float maxflickerSpeed = 0.8f;
    public float timer = 0.7f;

    public float floatEffect;

    private bool effectActive = false;
    private Vector3 origialPosition;
    private bool goingUp = true;

    private bool wasLooking = false;
    private bool switched = false;

    private MeshRenderer[] childrenMeshes;

    public float SpookyTreshold = 50;

    private delegate void DelegateAction();

    private DelegateAction actionToDo;

    public bool objectsFloat;
    public bool objectsFlip;
    public bool objectsShoot;

    private bool _playerInside = false;

    private void Start()
    {
        Services.EventManager.Register<SpookyMeterChange>(OnSpookyMeterChange);
        timer = Random.Range(minflickerSpeed, maxflickerSpeed);
        origialPosition = gameObject.transform.position;
        childrenMeshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (objectsFlip)
        {
            actionToDo += Flipping;
        }

        if (objectsFloat)
        {
            actionToDo += Levitate;
        }

        if (objectsShoot)
        {
            actionToDo += Shooting;
        }
    }

    private void Update()
    {
        if (!Services.gameManager.IsBabyActive()) return;
        if (_playerInside && Services.gameManager.ActiveSpookySpike())
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = Random.Range(minflickerSpeed, maxflickerSpeed);
                foreach (var light in roomLights)
                {
                    light.enabled = !light.enabled;
                }
            }
        }

        if (effectActive)
        {
            actionToDo?.Invoke();
        }
    }

    private void OnSpookyMeterChange(GameEvent e)
    {
        SpookyMeterChange smc = (SpookyMeterChange) e;
        if (smc.CurrentSpookyValue >= SpookyTreshold)
        {
            effectActive = true;
        }
        else if (smc.CurrentSpookyValue < SpookyTreshold)
        {
            effectActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInside = false;
    }

    private void Shooting()
    {
        bool oneIsVisible = false;
        foreach (MeshRenderer meshes in childrenMeshes)
        {
            oneIsVisible = oneIsVisible || meshes.isVisible;
        }

        if (oneIsVisible)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                Services.gameManager.playerPos.transform.position, Time.deltaTime / 5);
        }
        else
        {
            effectActive = false;
            gameObject.transform.position = origialPosition;
        }
    }

    private void Levitate()
    {
        bool oneIsVisible = false;
        foreach (MeshRenderer meshes in childrenMeshes)
        {
            oneIsVisible = oneIsVisible || meshes.isVisible;
        }

        if (oneIsVisible)
        {
            if (Vector3.Distance(gameObject.transform.localPosition, new Vector3(0, 2, 0)) < 0.1f && goingUp)
            {
                goingUp = false;
            }

            if (Vector3.Distance(gameObject.transform.localPosition, new Vector3(0, 2, 0)) > 1.9f && !goingUp)
            {
                goingUp = true;
            }

            if (goingUp)
            {
                gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,
                    new Vector3(0, 2, 0), Time.deltaTime * floatEffect);
            }
            else
            {
                gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,
                    new Vector3(0, 0, 0), Time.deltaTime * floatEffect);
            }
        }
    }

    private void Flipping()
    {
        bool oneIsVisible = false;
        foreach (MeshRenderer meshes in childrenMeshes)
        {
            oneIsVisible = oneIsVisible || meshes.isVisible;
        }

        if (!oneIsVisible && wasLooking)
        {
            if (!switched)
            {
                gameObject.transform.localScale = new Vector3(1, -1, 1);
                gameObject.transform.localPosition = new Vector3(0, -4, 0);
                switched = true;
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                switched = false;
            }
        }

        wasLooking = oneIsVisible;
    }
}