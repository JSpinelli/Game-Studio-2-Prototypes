using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
    public GameObject[] itemProgression;
    public AudioSource[] sounds;

    private int currentItem = 0;
    public void Start()
    {
        foreach (var item in itemProgression)
        {
            item.SetActive(false);
        }

        itemProgression[0].SetActive(true);
    }

    // Start is called before the first frame update
    public void Change()
    {
        if (currentItem < itemProgression.Length - 1)
        {
            itemProgression[currentItem].SetActive(false);
            itemProgression[currentItem+1].SetActive(true);
        }

        if (currentItem < sounds.Length)
        {
            if (sounds[currentItem])
                sounds[currentItem].Play();
        }
    }

    public bool IsOver()
    {
        return currentItem >= itemProgression.Length;
    }
}