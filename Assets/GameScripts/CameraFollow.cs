using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // camera will follow this object


 

    public Transform target;
    //camera transform
    private Rigidbody playerRB;
    // offset between camera and target
    public Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;
    public float speed;


    private void Start()
    {
        playerRB = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {

            Vector3 plForward = (playerRB.velocity + target.transform.forward).normalized;
            transform.position = Vector3.Lerp(transform.position,
                target.position + target.transform.TransformVector(Offset) + plForward * (-5f),
                speed * Time.deltaTime);

            // update rotation
            transform.LookAt(target);


    }
}
