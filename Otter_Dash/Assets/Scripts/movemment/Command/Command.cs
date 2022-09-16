using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public abstract class Command
    {
        //How far should the box move when we press a button
        protected static float forward = 2f;
        protected static float backward = forward * 0.5f;
        protected static float turn = 0;
        protected static float maxVelocity = 15f;
        protected static Vector3 flatVelocity;


        //Move and maybe save command
        public abstract void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction);

        //Move the box
        public virtual void Move(Rigidbody boxRB, int speedMulti, float rotate){}
    }

    public class MoveForward : Command
    {
        public override void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction)
        {
            Move(boxRB, speedMulti, rotate);
        }

        public override void Move(Rigidbody boxRB, int speedMulti, float rotate)
        {
            if (boxRB.velocity.magnitude > maxVelocity)
            {
                flatVelocity = new Vector3(boxRB.velocity.x, 0f, boxRB.velocity.z);
                Vector3 limitedVelocity = flatVelocity.normalized * maxVelocity;
                boxRB.velocity = new Vector3(limitedVelocity.x, boxRB.velocity.y, limitedVelocity.z);
            }
            else
            {
                boxRB.AddForce(boxRB.transform.forward * forward * speedMulti, ForceMode.Impulse);
            }
            boxRB.transform.rotation = Quaternion.Euler(0f, turn, 0f);
        }
    }

    public class MoveBackward : Command
    {
        public override void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction)
        {
            Move(boxRB, speedMulti, rotate);
        }

        public override void Move(Rigidbody boxRB, int speedMulti, float rotate)
        {
            if (boxRB.velocity.magnitude > maxVelocity)
            {
                flatVelocity = new Vector3(boxRB.velocity.x, 0f, boxRB.velocity.z);
                Vector3 limitedVelocity = flatVelocity.normalized * maxVelocity;
                boxRB.velocity = new Vector3(limitedVelocity.x, boxRB.velocity.y, limitedVelocity.z);
            }
            else
            {
                boxRB.AddForce(-boxRB.transform.forward * backward * speedMulti, ForceMode.Impulse);
            }
            boxRB.transform.rotation = Quaternion.Euler(0f, turn, 0f);
        }
    }

    public class MoveLeft : Command
    {
        public override void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction)
        {
            
            IncreaseRotation(boxRB, direction);
        }
        
        public void IncreaseRotation(Rigidbody boxRB, bool direction)
        {
            Debug.Log(direction);
            if (boxRB.velocity.magnitude > 1f && turn <= 800f && direction)
            {
                turn -= Time.deltaTime * 50;
                Debug.Log(turn);
            }
        }
    }

    public class MoveRight : Command
    {
        public override void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction)
        {
            
            IncreaseRotation(boxRB, direction);
            
        }
        
        public void IncreaseRotation(Rigidbody boxRB, bool direction)
        {
            Debug.Log(direction);
            if (boxRB.velocity.magnitude > 1f && turn <= 800f && direction)
            {
                turn += Time.deltaTime * 50;
                Debug.Log(turn);
            }
        }
    }

    public class PullOutPhone : Command
    {
        public override void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction)
        {
            movePhone(rect);
        }
        public void movePhone(RectTransform boxTrans)
        {
            // 0 -> on screen
            // -600 -> off screen
            if (boxTrans.position.y < 0)
            {
                while (boxTrans.position.y < 0)
                {
                    boxTrans.position += new Vector3(0, 50 * Time.deltaTime, 0);
                }
            }

        }
    }

    public class EmptyInput : Command
    {
        public override void Execute(Rigidbody boxRB, RectTransform rect, Command command, int speedMulti, float rotate, bool direction)
        {
            // no key is binded
        }
    }
}

