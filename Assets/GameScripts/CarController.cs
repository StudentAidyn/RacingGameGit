using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CarController : MonoBehaviour
{
    //public GameObject lapDisplay;

    public int checkpointIndex;
    public int lapNumber;


    //CheckPoint

    public Transform CurrentCP;
    


    private Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;

    //public WheelParticles wheelParticles;
    //TextMeshProUGUI lapDisplayText;

    public float gasInput;
    public float brakeInput;
    public float steerInput;

    //public GameObject smokePrefab;

    public float horsePower;
    private float speed;
    public float brakeForce;
    public float slipAngle;
    public AnimationCurve steeringCurve;

    public float nitrous = 1;
    // Start is called before the first frame update
    void Start()
    {
        //text update
        //lapDisplayText = lapDisplay.GetComponent<TextMeshProUGUI>();

        //player controls - to be updated
        playerRB = gameObject.GetComponent<Rigidbody>();
        lapNumber = 1;
        checkpointIndex = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = playerRB.velocity.magnitude;
        CheckInput();
        motor();
        brake();
        steering();
        WheelMeshUpdate();

        //text update

        //lapDisplayText.text = lapNumber.ToString();
       
    }

    /* 
     //setup for later
    void InstantiateSmoke(){
        wheelParticles.FRWheel = Instantiate(smokePrefab, colliders.FRWheel.transform.position, Quaternion.identity)
            .GetComponent<ParticleSystem>();
        wheelParticles.FLWheel = Instantiate(smokePrefab, colliders.FLWheel.transform.position, Quaternion.identity)
            .GetComponent<ParticleSystem>();
        wheelParticles.BRWheel = Instantiate(smokePrefab, colliders.BRWheel.transform.position, Quaternion.identity)
            .GetComponent<ParticleSystem>();
        wheelParticles.BLWheel = Instantiate(smokePrefab, colliders.BLWheel.transform.position, Quaternion.identity)
            .GetComponent<ParticleSystem>();
    }
     
     */

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);
        if(slipAngle < 120.0f) {
            if (gasInput < 0) {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
    }

    void brake()
    {
        colliders.FRWheel.brakeTorque = brakeInput * brakeForce * 0.7f;
        colliders.FLWheel.brakeTorque = brakeInput * brakeForce * 0.7f;
                                                          
        colliders.BRWheel.brakeTorque = brakeInput * brakeForce * 0.5f;
        colliders.BLWheel.brakeTorque = brakeInput * brakeForce * 0.5f;
    }

    void motor()
    {
        colliders.BRWheel.motorTorque = horsePower * gasInput * speed * nitrous;
        colliders.BLWheel.motorTorque = horsePower * gasInput * speed * nitrous;
    }

    void steering()
    {
        float steeringAngle = steerInput * steeringCurve.Evaluate(speed);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }

    void WheelMeshUpdate()
    {
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.BRWheel, wheelMeshes.BRWheel);
        UpdateWheel(colliders.BLWheel, wheelMeshes.BLWheel);
    }
    void UpdateWheel(WheelCollider wColl, MeshRenderer wMesh)
    {
        //find alternative, to not use quaternion as it can look good without it.
        Quaternion quat; 
        Vector3 position;
        wColl.GetWorldPose(out position, out quat);
        quat = quat * Quaternion.Euler(new Vector3(0, 0, 90)); //https://forum.unity.com/threads/visual-wheels-rotating-around-wrong-axes-wheelcollider-tutorial.472469/
        wMesh.transform.position = position;
        wMesh.transform.rotation = quat;
    }

    void RespawnAtLastCP()
    {

    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider BRWheel;
    public WheelCollider BLWheel;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer BRWheel;
    public MeshRenderer BLWheel;
}
/* 
 Setup for later..

public class WheelParticles
{
    public ParticleSystem FRWheel;
    public ParticleSystem FLWheel;
    public ParticleSystem BRWheel;
    public ParticleSystem BLWheel;
}

 
 
 */

//https://www.youtube.com/watch?v=gEwNHUDc8uE&ab_channel=NanousisDevelopment