using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] objectsInRoom;

    private GameObject[] changedObjects;

    public Light[] roomLights;

    public bool flickering = false;
    public float minflickerSpeed = 0.2f;
    public float maxflickerSpeed = 0.8f;
    public float timer = 0.7f;

    private void Start()
    {
        Services.EventManager.Register<BorednessChange>(OnBorednessChange);
        timer = Random.Range(minflickerSpeed,maxflickerSpeed);
    }

    private void Update()
    {
        if (flickering)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = Random.Range(minflickerSpeed,maxflickerSpeed);
                foreach (var light in roomLights)
                {
                    light.enabled = !light.enabled;
                }
            }
        }
    }

    private void OnBorednessChange(GameEvent e)
    {
        
    }
}
