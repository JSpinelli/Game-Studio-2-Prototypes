using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public float interactRadius;

    private SphereCollider _interactCollider;

    public void Start()
    {
        _interactCollider = gameObject.AddComponent<SphereCollider>();
        _interactCollider.center = Vector3.zero; // the center must be in local coordinates
        _interactCollider.radius = interactRadius;
        _interactCollider.isTrigger = true;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    public abstract void OnInteract();
}
