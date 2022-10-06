using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public abstract class Command
    {
        //Move and maybe save command
        public abstract void Execute(Rigidbody rb);

    }
    
    // public class Accelerate : Command{
    //     public override void Execute(Rigidbody rb)
    //     {
    //         if (throttle != 0 && (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(throttle)))
    //         {
    //             foreach (WheelCollider wheel in driveWheel)
    //             {
    //                 wheel.motorTorque = throttle * motorTorque.Evaluate(speed) * diffGearing / driveWheel.Length;
    //             }
    //         }
    //         else if(throttle != 0)
    //         {
    //             foreach (WheelCollider wheel in wheels)
    //             {
    //                 // commandBrake.Execute();
    //                 wheel.brakeTorque = Mathf.Abs(throttle) * brakeForce;
    //             }
    //         }
    //     }
    // }
    //
    // public class Brake : Command{
    //     public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
    //     {
    //         rb.AddForce(transform.forward * boostForce);
    //         
    //     }
    // }
    //
    // public class Drift : Command{
    //     public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
    //     {
    //         Vector3 driftForce = -transform.right;
    //         driftForce.y = 0.0f;
    //         driftForce.Normalize();
    //
    //         if (steering != 0)
    //             driftForce *= rb.mass * speed/7f * throttle * steering/steerAngle;
    //         Vector3 driftTorque = transform.up * 0.1f * steering/steerAngle;
    //             
    //         rb.AddForce(driftForce * driftIntensity, ForceMode.Force);
    //         rb.AddTorque(driftTorque * driftIntensity, ForceMode.VelocityChange);
    //         
    //     }
    // }
    //
    // public class Turn : Command{
    //     public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
    //     {
    //         rb.AddForce(transform.forward * boostForce);
    //         
    //     }
    // }
    //
    // public class Jump : Command{
    //     public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
    //     {
    //         rb.velocity += transform.up * jumpVel;
    //         
    //     }
    // }
    //
    // public class Boost : Command{
    //     public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
    //     {
    //         rb.AddForce(transform.forward * boostForce);
    //         
    //     }
    // }
    
    public class EmptyInput : Command
    {
        public override void Execute(Rigidbody rb)
        {
            // no key is binded
        }
    }
}

