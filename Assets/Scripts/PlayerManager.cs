
public class PlayerManager
{

    private float sanity;
    private float rateOfDecay;

    public PlayerManager(float initial, float rate){
        sanity = initial;
        rateOfDecay = rate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseSanity(){
        sanity -= rateOfDecay;
    }
}
