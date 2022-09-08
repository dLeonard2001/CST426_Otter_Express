using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlisesPlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 3f;
    private float dirX, dirZ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * moveSpeed;
        dirZ = Input.GetAxis("Vertical") * moveSpeed;
    }

    private void FixedUpdate()
    {
        //if(dirZ != 0)
            rb.velocity = new Vector3(dirX, rb.velocity.y, dirZ);
    }
}
