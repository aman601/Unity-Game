using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject target;
    public float speed;

    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        // Look - camera follows forward of car, z axis
        var newRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);
        

        // Move
        Vector3 newPosition = target.transform.position - target.transform.forward * offset.z - target.transform.up * offset.y;
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * speed);
    }




/*
// CAMERA ATTEMPT CAN BE USED FOR WALL
    public float smoothness;
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;

    public Rigidbody carBody;
    public float carSpeed;

    void Start()
    {
        //calculates initial offset
        initalOffset = transform.position - targetObject.position;
    }

    void FixedUpdate()
    {
        Camera.main.transform.localEulerAngles = new Vector3(0f,carBody.transform.localEulerAngles.y,0f);
        carSpeed = carBody.velocity.z;
        //keeps offset
        cameraPosition = targetObject.position + initalOffset + new Vector3(0,0,carSpeed/60);
        //follows target object smoothly
        transform.position = Vector3.Lerp(transform.position, cameraPosition, 9999999999999999999);//smoothness * Time.fixedDeltaTime);
        
    }
    \*/

}