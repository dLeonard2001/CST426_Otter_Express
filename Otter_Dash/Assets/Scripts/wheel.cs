using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel : MonoBehaviour
{

    [Header("Wheel Colliders")] 
    public Rigidbody carRB;
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    [Header("Wheel Meshes")]
    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform backLeftTransform;
    public Transform backRightTransform;
    private float frontRight_start_angle;
    private float frontLeft_start_angle;
    private float backRight_start_angle;
    private float backLeft_start_angle;
    private bool lowSpeed;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle;

    private void Start()
    {
        frontRight_start_angle = frontRightTransform.rotation.eulerAngles.y;
        frontLeft_start_angle = frontLeftTransform.rotation.eulerAngles.y;
        backRight_start_angle = backRightTransform.rotation.eulerAngles.y;
        backLeft_start_angle = backLeftTransform.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        if (carRB.velocity.magnitude > 30)
        {
            maxTurnAngle = 10;
        }
        else
        {
            maxTurnAngle = 30;
        }
        
        float verticalInput = acceleration * Input.GetAxis("Vertical");
        float horizontalInput = maxTurnAngle * Input.GetAxis("Horizontal");
        
        currentAcceleration = verticalInput;
        
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        currentTurnAngle = horizontalInput;
        
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;





    }

}


