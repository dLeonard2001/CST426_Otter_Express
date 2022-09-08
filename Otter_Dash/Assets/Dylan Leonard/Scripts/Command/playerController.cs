using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Command
{
    public class playerController : MonoBehaviour
    {
        [Header("Player Object")] 
        public InputManager inputManager;
        public Rigidbody boxTrans;
        public int speedMultiplier;
        public static List<Command> pressedCommands = new List<Command>();
        private Vector2 movement;
        private Command moveForward, moveBackward, moveLeft, moveRight;
        private float rotate;
        private bool playerMoving;

        [Header("Replay")]
        public static bool startReplay;
        private Vector3 startPos;
        private Coroutine replayCoroutine;
        private bool isReplaying;

        [Header("Camera")] 
        public Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            inputManager = InputManager.createInstance();
            startPos = boxTrans.position;
            isReplaying = false;
            
            moveForward = new MoveForward();
            moveBackward = new MoveBackward();
            moveLeft = new MoveLeft();
            moveRight = new MoveRight();
            rotate = 0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!isReplaying)
            {
                GetInput();
            }
        }

        public void GetInput()
        {
            // transform.rotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
            if (inputManager.Forward())
            {
                moveForward.Execute(boxTrans, moveForward, speedMultiplier, rotate, playerMoving);
                playerMoving = true;
            }

            if (inputManager.Backward())
            {
                moveBackward.Execute(boxTrans, moveBackward, speedMultiplier, rotate, playerMoving);
                playerMoving = true;
            }
            
            if (!inputManager.Forward() && !inputManager.Forward())
                playerMoving = false;

            if (inputManager.Left())
            {
                moveLeft.Execute(boxTrans, moveBackward, speedMultiplier, rotate, playerMoving);
            }

            if (inputManager.Right())
            {
                moveRight.Execute(boxTrans, moveBackward, speedMultiplier, rotate, playerMoving);
            }

            
        }
    } 
}

