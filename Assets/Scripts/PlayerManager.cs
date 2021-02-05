using UnityEngine;
using TMPro;
public class PlayerManager
{

    public float sanity;
    private float rateOfDecay;
    public TextMeshProUGUI sanityMeter;

    public PlayerManager(float initial, float rate, TextMeshProUGUI sanityMtr){
        sanity = initial;
        rateOfDecay = rate;
        sanityMeter = sanityMtr;
        float fillSanity = sanity/10;
        sanityMeter.text= "";
        for (int i =0;  i < fillSanity; i++){
            sanityMeter.text += "|";
        }
    }

    public void decreaseSanity(){
        sanity -= rateOfDecay;
        string temp = sanityMeter.text;
        sanityMeter.text = temp.Substring(0,temp.Length - 1);
        Debug.Log("SANITY DECREASED NOW: "+sanity);
    }
}
