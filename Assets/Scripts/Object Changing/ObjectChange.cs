using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectChange : MonoBehaviour
{
    public bool startAtPlay;
    protected bool triggered;
    
    public abstract void Change();

    public abstract void Reset();
}
