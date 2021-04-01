using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionDetection : MonoBehaviour
{
    
    public float MaxDistance;
    public Vector3 offsetFromCenter;
    public Vector3 scale;
    
    private Camera _cam;
    private bool _mHitDetect;
    private RaycastHit _mHit;
    private InteractableObject[] _currentInteractable;
    private bool _interactableActive;

    void Start()
    {
        _cam = Camera.main;
        _interactableActive = false;
    }

    void FixedUpdate()
    {
        byte layerToHit = 9;
        _mHitDetect = Physics.BoxCast(
            transform.position + offsetFromCenter,
            scale,
            transform.forward,
            out _mHit,
            transform.rotation,
            MaxDistance,
            ~layerToHit);
        if (_mHitDetect && _mHit.collider.gameObject.CompareTag("Interactable"))
        {
            if (!_interactableActive || _currentInteractable[0].name != _mHit.collider.gameObject.name)
                _currentInteractable = _mHit.collider.gameObject.GetComponents<InteractableObject>();
            int i = 0;
            bool found = false;
            while (i < _currentInteractable.Length && !found)
            {
                if (_currentInteractable[i].CanInteract())
                {
                    found = true;
                    _currentInteractable[i].EnableOutline();
                    _interactableActive = true;
                }
            }
            if (i >= _currentInteractable.Length)
            {
                _currentInteractable[i].DisableOutline();
                _interactableActive = false;  
            }
        }
        else
        {
            if (_interactableActive)
            {
                _currentInteractable[0].DisableOutline();
                _interactableActive = false;
            }
        }
    }

    private void Update()
    {
        if (_interactableActive && Input.GetKeyDown(KeyCode.E))
        {
            foreach (var interactable in _currentInteractable)
            {
                if (interactable.CanInteract())
                {
                    interactable.OnInteract();
                }
            }
            
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (_mHitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * _mHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + offsetFromCenter + transform.forward * _mHit.distance, scale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + offsetFromCenter + transform.forward * MaxDistance, scale);
        }
    }
}