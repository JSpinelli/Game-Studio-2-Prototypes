using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Services.EventManager.Register<SpookyMeterChange>(OnSpookyMeterChange);
    }

    // Update is called once per frame
    void OnSpookyMeterChange(GameEvent e)
    {
        
    }
}
