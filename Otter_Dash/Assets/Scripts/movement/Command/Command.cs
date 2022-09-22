using System;
using System.Collections;
using System.Collections.Generic;
using Parabox.Road;
using UnityEngine;

namespace Command
{
    public abstract class Command
    {
        //How far should the box move when we press a button
        protected static float forward = 2f;
        protected static float backward = forward;
        protected static float turn = 0;
        protected static float maxVelocity = 10f;
        protected static Vector3 flatVelocity;


        //Move and maybe save command
        public abstract void Execute(Rigidbody boxRB, Command command, int speedMulti);

        //Move the box
        public virtual void Move(Rigidbody boxRB, int speedMulti){}
    }

    public class MoveForward : Command
    {
        public override void Execute(Rigidbody boxRB, Command command, int speedMulti)
        {
            Move(boxRB, speedMulti);
        }

        public override void Move(Rigidbody boxRB, int speedMulti)
        {
            if (boxRB.velocity.magnitude > maxVelocity)
            {
                flatVelocity = new Vector3(boxRB.velocity.x, 0f, boxRB.velocity.z);
                Vector3 limitedVelocity = flatVelocity.normalized * maxVelocity;
                boxRB.velocity = new Vector3(limitedVelocity.x, boxRB.velocity.y, limitedVelocity.z);
            }
            else
            {
                boxRB.AddForce(boxRB.transform.forward * forward * speedMulti, ForceMode.Acceleration);
            }
            // boxRB.transform.rotation = Quaternion.Euler(0f, turn, 0f);
        }
    }

    public class MoveBackward : Command
    {
        public override void Execute(Rigidbody boxRB, Command command, int speedMulti)
        {
            Move(boxRB, speedMulti);
        }

        public override void Move(Rigidbody boxRB, int speedMulti)
        {
            if (boxRB.velocity.magnitude > maxVelocity)
            {
                flatVelocity = new Vector3(boxRB.velocity.x, 0f, boxRB.velocity.z);
                Vector3 limitedVelocity = flatVelocity.normalized * maxVelocity;
                boxRB.velocity = new Vector3(limitedVelocity.x, boxRB.velocity.y, limitedVelocity.z);
            }
            else
            {
                boxRB.AddForce(-boxRB.transform.forward * backward * speedMulti, ForceMode.Acceleration);
            }
            // boxRB.transform.rotation = Quaternion.Euler(0f, turn, 0f);
        }
    }

    public class MoveLeft : Command
    {
        public override void Execute(Rigidbody boxRB, Command command, int speedMulti)
        {
            
            IncreaseRotation(boxRB);
        }
        
        public void IncreaseRotation(Rigidbody boxRB)
        {
            Debug.Log(boxRB.angularVelocity);
            if (boxRB.angularVelocity.magnitude < 5)
            {
                boxRB.AddTorque(boxRB.transform.up * 8 * -1);
            }
            else
            {
                flatVelocity = new Vector3(0f, boxRB.angularVelocity.y, 0f);
                Vector3 limitedVelocity = flatVelocity.normalized * 5;
                boxRB.angularVelocity = new Vector3(0f, limitedVelocity.y, 0f);
            }
        }
    }

    public class MoveRight : Command
    {
        public override void Execute(Rigidbody boxRB, Command command, int speedMulti)
        {
            
            IncreaseRotation(boxRB);
            
        }
        
        public void IncreaseRotation(Rigidbody boxRB)
        {
            if (boxRB.angularVelocity.magnitude < 5)
            {
                boxRB.AddTorque(boxRB.transform.up * 8);
            }
            else
            {
                flatVelocity = new Vector3(0f, boxRB.angularVelocity.y, 0f);
                Vector3 limitedVelocity = flatVelocity.normalized * 5;
                boxRB.angularVelocity = new Vector3(0f, limitedVelocity.y, 0f);
            }
        }
    }

    public class EmptyInput : Command
    {
        public override void Execute(Rigidbody boxRB, Command command, int speedMulti)
        {
            // no key is binded
        }
    }
}

