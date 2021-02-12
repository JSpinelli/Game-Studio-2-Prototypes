using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public PlayerManager player_manager;


    public TextMeshProUGUI calmnessMeter;
    public TextMeshProUGUI dialogBox;

    public float initialCalmness;
    public float calmnessDecreaseRate;

    public GameObject baby1;
    public GameObject baby2;
    public GameObject baby3;

    public float textOnScreenTime = 8f;

    private string[] dialogs = {
        "ads",
        "dsada"
    };
    // Start is called before the first frame update
    void Start()
    {
        player_manager = new PlayerManager(initialCalmness, calmnessDecreaseRate, calmnessMeter);
        StartCoroutine(Countdown("Something is at my door! I hope is not a demon baby"));
    }

    // Update is called once per frame
    void Update()
    {
        if (baby1.activeSelf && player_manager.calmness < 70)
        {
            baby1.SetActive(false);
            baby2.SetActive(true);
        }
        if (baby2.activeSelf && player_manager.calmness < 30)
        {
            baby2.SetActive(false);
            baby3.SetActive(true);
        }
    }

    private IEnumerator Countdown(string textToDisplay)
    {
        float duration = textOnScreenTime;
        float normalizedTime = 0;
        dialogBox.text = textToDisplay;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        dialogBox.text = "";
    }
}
