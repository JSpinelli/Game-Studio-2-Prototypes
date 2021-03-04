using UnityEngine;
using UnityEngine.UI;

public class InteractionDetection : MonoBehaviour
{
    
    private Image customImage;
    void OnMouseOver(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            customImage.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractableObject obj = other.GetComponent<InteractableObject>();
                obj.OnInteract();
            }
        }
        else
        {
            customImage.enabled = false;
        }
    } 
}
