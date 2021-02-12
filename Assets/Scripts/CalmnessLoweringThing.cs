using UnityEngine;

public class CalmnessLoweringThing : MonoBehaviour
{
    private bool triggered = false;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.tag == "Player")
            {
                triggered = true;
                gameManager.player_manager.decreaseCalmness();
            }

        }

    }
}
