using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Object_Chase : MonoBehaviour
{

    public Transform targ;
    public float offset;
    public float speed;


    void Update()
    {
        transform. position = Vector3. Lerp(transform. position, targ. position, speed * Time. deltaTime);
        transform. rotation = Quaternion. Slerp(transform.rotation, targ. rotation, speed * Time. deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, offset );
    }
}
