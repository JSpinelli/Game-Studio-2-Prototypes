using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public PlayerManager player_manager;


    public TextMeshProUGUI calmnessMeter;

    public float initialCalmness;
    public float calmnessDecreaseRate;

    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;
    // Start is called before the first frame update
    void Start()
    {
        player_manager = new PlayerManager(initialCalmness, calmnessDecreaseRate, calmnessMeter);
    }

    // Update is called once per frame
    void Update()
    {
        if ( baby1.active && player_manager.calmness < 70){
            baby1.SetActive(false);
            baby2.SetActive(true);
        }
        if (baby2.active && player_manager.calmness < 30){
            baby2.SetActive(false);
            baby3.SetActive(true);
        }
    }
}
