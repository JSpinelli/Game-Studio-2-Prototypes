using UnityEngine;

public class CalmnessLoweringThing : MonoBehaviour
{
    private bool triggered = false;

    private float timer = 10f;
    private float timerCurrent = 10f;
    private bool timerActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.tag == "Player")
            {
                triggered = true;
                timerCurrent = timer;
                timerActive = true;
                Services.EventManager.Fire(new ChangeSpookyness(-10));
            }

        }
    }

    private void Update()
    {
        if (timerActive)
        {
            if (timerCurrent < 0)
            {
                timerCurrent = timer;
                triggered = false;
            }else{
                timerCurrent -= Time.deltaTime;
            }
        }
    }
}
