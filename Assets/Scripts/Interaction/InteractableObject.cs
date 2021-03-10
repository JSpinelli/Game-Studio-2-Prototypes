using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public float interactRadius;
    public Vector3 interactSpherePosition;
    private SphereCollider _interactCollider;
    public void Start()
    {
        _interactCollider = gameObject.AddComponent<SphereCollider>();
        _interactCollider.center = interactSpherePosition; // the center must be in local coordinates
        _interactCollider.radius = interactRadius;
        _interactCollider.isTrigger = true;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactSpherePosition, interactRadius);
    }

    public abstract bool CanInteract();

    public abstract void OnInteract();
}
