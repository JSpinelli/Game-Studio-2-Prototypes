using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerManager player_manager;

    public float initialSanity;
    public float sanityDecreaseRate;
    // Start is called before the first frame update
    void Start()
    {
        player_manager = new PlayerManager(initialSanity, sanityDecreaseRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
