using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel : MonoBehaviour
{
    
    [Header("Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    [Header("Wheel Meshes")]
    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform backLeftTransform;
    public Transform backRightTransform;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle;

    private void FixedUpdate()
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        
        
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

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        
        updateWheel(frontRight, frontRightTransform);
        updateWheel(frontLeft, frontLeftTransform);
        updateWheel(backLeft, backLeftTransform);
        updateWheel(backRight, backRightTransform);
    }

    public void updateWheel(WheelCollider wheel, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        
        wheel.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }
}


