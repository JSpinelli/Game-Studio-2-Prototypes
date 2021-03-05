using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionDetection : MonoBehaviour
{
    public Image customImage;
    private Camera _cam;

    public float m_MaxDistance;
    public float m_Speed;
    public Vector3 offsetFromCenter;
    public Vector3 scale;
    bool m_HitDetect;
    RaycastHit m_Hit;
    private InteractableObject currentInteractable;

    void Start()
    {
        _cam = Camera.main;
        customImage.enabled = false;
    }

    void FixedUpdate()
    {
        byte layerToHit = 9;
        m_HitDetect = Physics.BoxCast(
            transform.position + offsetFromCenter,
            scale,
            transform.forward,
            out m_Hit,
            transform.rotation,
            m_MaxDistance,
            ~layerToHit);
        if (m_HitDetect)
        {
            customImage.transform.position = _cam.WorldToScreenPoint(m_Hit.point);
            currentInteractable = m_Hit.collider.gameObject.GetComponent<InteractableObject>();
            customImage.enabled = true;
        }
        else
        {
            customImage.enabled = false;
        }
    }

    private void Update()
    {
        if (customImage.enabled && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.OnInteract();
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + offsetFromCenter + transform.forward * m_Hit.distance, scale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + offsetFromCenter + transform.forward * m_MaxDistance, scale);
        }
    }
}