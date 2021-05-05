// original by Eric Haines (Eric5h5)
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/FPSWalkerEnhanced

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class PlayerManager : MonoBehaviour
{
    public float walkSpeed = 6.0f;

    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    private bool limitDiagonalSpeed = true;

    public float gravity = 10.0f;

    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;

    private Vector3 _moveDirection = Vector3.zero;
    private Transform _myTransform;
    private float _speed;
    private CharacterController _controller;

    public Transform cam;
    
    public bool invertY = false;
	
    public float sensitivityX = 10F;
    public float sensitivityY = 9F;
 
    public float minimumX = -360F;
    public float maximumX = 360F;
 
    public float minimumY = -85F;
    public float maximumY = 85F;
 
    float rotationX = 0F;
    float rotationY = 0F;
 
    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;	
 
    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;
 
    public float framesOfSmoothing = 5;
 
    Quaternion originalRotation;
    Quaternion camOriginalRotation;

    public AudioSource footsteps;
    public float footstepTreshold;

    void Start()
    {
        _myTransform = transform;
        _speed = walkSpeed;
        _controller = GetComponent<CharacterController>();
        
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            Debug.Log("Rotation Freezed");
        }
		
        originalRotation = transform.localRotation;
        camOriginalRotation = cam.localRotation;
        
        Services.InputManager.PushRangeAction(Move);
        Services.InputManager.PushRangeActionCamera(Camera);
    }

    void Move(float inputX, float inputY)
    {
        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;
        _moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
        _moveDirection = _myTransform.TransformDirection(_moveDirection) * _speed;
        // Apply gravity
        _moveDirection.y -= gravity * Time.deltaTime;
        if (_moveDirection.magnitude > footstepTreshold)
        {
            if (!footsteps.isPlaying)
                footsteps.Play();
        }
        else
        {
            if (footsteps.isPlaying)
                footsteps.Stop();
        }

        _controller.Move(_moveDirection * Time.deltaTime);
    }


    void Camera(float inputX, float inputY)
    {
        
        // X Axis
        rotAverageX = 0f;

        rotationX += inputX * sensitivityX * Time.timeScale;

        rotArrayX.Add(rotationX);

        if (rotArrayX.Count >= framesOfSmoothing)
        {
            rotArrayX.RemoveAt(0);
        }

        for (int i = 0; i < rotArrayX.Count; i++)
        {
            rotAverageX += rotArrayX[i];
        }

        rotAverageX /= rotArrayX.Count;
        rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
        transform.localRotation = originalRotation * xQuaternion;
        
        // Y Axis
        
        rotAverageY = 0f;

        float invertFlag = 1f;
        if (invertY)
        {
            invertFlag = -1f;
        }

        rotationY += inputY * sensitivityY * invertFlag * Time.timeScale;

        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        rotArrayY.Add(rotationY);

        if (rotArrayY.Count >= framesOfSmoothing)
        {
            rotArrayY.RemoveAt(0);
        }

        for (int j = 0; j < rotArrayY.Count; j++)
        {
            rotAverageY += rotArrayY[j];
        }

        rotAverageY /= rotArrayY.Count;

        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        cam.localRotation = camOriginalRotation * yQuaternion;
    }
    public static float ClampAngle (float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F)) {
            if (angle < -360F) {
                angle += 360F;
            }
            if (angle > 360F) {
                angle -= 360F;
            }			
        }
        return Mathf.Clamp (angle, min, max);
    }
}