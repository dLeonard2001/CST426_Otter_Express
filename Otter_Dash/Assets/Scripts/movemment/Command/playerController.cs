using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Command
{
    public class playerController : MonoBehaviour
    {
        [Header("Player Object")] 
        public InputManager inputManager;
        public Rigidbody boxTrans;
        public RectTransform playerPhone;
        public int speedMultiplier;
        public static List<Command> pressedCommands = new List<Command>();
        private Vector2 movement;
        private Command moveForward, moveBackward, moveLeft, moveRight, movePhone;
        private float rotate;
        private bool playerMoving;
        private bool phoneMoving;

        [Header("Replay")]
        public static bool startReplay;
        private Vector3 startPos;
        private Coroutine replayCoroutine;
        private bool isReplaying;

        [Header("Camera")] 
        public Camera mainCamera;
        public CameraController camController;

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
            movePhone = new PullOutPhone();
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
            // unlock the player
            if (inputManager.UnlockMouse())
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (inputManager.Forward())
            {
                moveForward.Execute(boxTrans, playerPhone, moveForward, speedMultiplier, rotate, playerMoving);
                playerMoving = true;
            }

            if (inputManager.Backward())
            {
                moveBackward.Execute(boxTrans, playerPhone, moveBackward, speedMultiplier, rotate, playerMoving);
                playerMoving = true;
            }
            
            if (!inputManager.Forward() && !inputManager.Backward())
                playerMoving = false;

            if (inputManager.Left())
            {
                moveLeft.Execute(boxTrans, playerPhone, moveBackward, speedMultiplier, rotate, playerMoving);
            }

            if (inputManager.Right())
            {
                moveRight.Execute(boxTrans, playerPhone, moveBackward, speedMultiplier, rotate, playerMoving);
            }
            
            if (inputManager.PullOutPhone() && !phoneMoving)
            {
                movePhone.Execute(boxTrans, playerPhone, moveForward, speedMultiplier, rotate, playerMoving);
                phoneMoving = true;
            }

            if (phoneMoving)
            {
                if (playerPhone.position.y == 0)
                {
                    phoneMoving = true;
                }else if (playerPhone.position.y == -600)
                {
                    phoneMoving = true;
                }
            }
            
        }
    } 
}

