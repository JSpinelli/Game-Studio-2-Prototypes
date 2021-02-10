using UnityEngine;
using TMPro;
public class PlayerManager
{

    public float calmness;
    private float rateOfDecay;
    public TextMeshProUGUI calmnessMeter;

    public PlayerManager(float initial, float rate, TextMeshProUGUI calmnessMtr){
        calmness = initial;
        rateOfDecay = rate;
        calmnessMeter = calmnessMtr;
        float fillSanity = calmness/10;
        calmnessMeter.text= "";
        for (int i =0;  i < fillSanity; i++){
            calmnessMeter.text += "|";
        }
    }

    public void decreaseCalmness(){
        calmness -= rateOfDecay;
        string temp = calmnessMeter.text;
        calmnessMeter.text = temp.Substring(0,temp.Length - 1);
        Debug.Log("CALMNESS DECREASED NOW: "+calmness);
    }
}
