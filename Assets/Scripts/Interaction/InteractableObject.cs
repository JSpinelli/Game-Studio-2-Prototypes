using System;
using cakeslice;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public float interactRadius;
    public Vector3 interactSpherePosition;
    private SphereCollider _interactCollider;
    public Outline outline;
    public void Start()
    {
        _interactCollider = gameObject.AddComponent<SphereCollider>();
        _interactCollider.center = interactSpherePosition; // the center must be in local coordinates
        _interactCollider.radius = interactRadius;
        _interactCollider.isTrigger = true;
        if (outline) outline.enabled = false;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactSpherePosition, interactRadius);
    }

    public void EnableOutline()
    {
        if (outline)
        {
            outline.enabled = true;
        }
    }
    public void DisableOutline()
    {
        if (outline)
        {
            outline.enabled = false;
        }
    }

    public abstract bool CanInteract();

    public abstract void OnInteract();
}
