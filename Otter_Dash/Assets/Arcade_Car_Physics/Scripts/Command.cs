using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public abstract class Command
    {
        //Move and maybe save command
        public abstract void Execute(Rigidbody rb, Transform transform, float boostForce, float boost);

    }
    
    
    public class Boost : Command{
        public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
        {
            rb.AddForce(transform.forward * boostForce);

            boost -= Time.fixedDeltaTime;
            if (boost < 0f) { boost = 0f; }
            
        }
    }
    
    
    

    public class EmptyInput : Command
    {
        public override void Execute(Rigidbody rb, Transform transform, float boostForce, float boost)
        {
            // no key is binded
        }
    }
}

