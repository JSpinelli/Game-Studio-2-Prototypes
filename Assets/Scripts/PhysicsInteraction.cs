using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsInteraction : MonoBehaviour
{
    private Camera cam;

    public Interactable focus;

    [SerializeField] private Image hoverPointer;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray point = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit interact;

        if (Physics.Raycast(point, out interact, 100) && gameObject.tag == "Interactable")
        {
            hoverPointer.enabled = true;
        }
        else
        {
            hoverPointer.enabled = false;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            //Ray Created
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // if the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
              Interactable interactable = hit.collider.GetComponent<Interactable>();
              if (interactable != null)
              {
                  SetFocus(interactable);
              }
            }
        } 
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }
}
