using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
    public GameObject firstItem;

    public GameObject secondItem;

    public AudioSource sound;

    public void Start()
    {
        firstItem.SetActive(true);
        secondItem.SetActive(false);
    }

    // Start is called before the first frame update
    public void Change()
    {
        firstItem.SetActive(false);
        if (sound)
            sound.Play();
        secondItem.SetActive(true);
    }
}