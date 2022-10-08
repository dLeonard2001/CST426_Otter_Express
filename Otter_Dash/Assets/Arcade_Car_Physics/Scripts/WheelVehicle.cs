/*
 * This code is part of Arcade Car Physics for Unity by Saarg (2018)
 * 
 * This is distributed under the MIT Licence (see LICENSE.md for details)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

#if MULTIOSCONTROLS
    using MOSC;
#endif

[assembly: InternalsVisibleTo("VehicleBehaviour.Dots")]

namespace VehicleBehaviour {
    [RequireComponent(typeof(Rigidbody))]
    public class WheelVehicle : MonoBehaviour {
        
        [Header("Inputs")]
    #if MULTIOSCONTROLS
        [SerializeField] PlayerNumber playerId;
    #endif
        // If isPlayer is false inputs are ignored
        [SerializeField] bool isPlayer = true;
        public bool IsPlayer { get => isPlayer;
            set => isPlayer = value;
        } 

        // Input names to read using GetAxis
        [SerializeField] internal VehicleInputs m_Inputs;
        string throttleInput => m_Inputs.ThrottleInput;
        string brakeInput => m_Inputs.BrakeInput;
        string turnInput => m_Inputs.TurnInput;
        string jumpInput => m_Inputs.JumpInput;
        string driftInput => m_Inputs.DriftInput;
	    string boostInput => m_Inputs.BoostInput;

        public Image nitroTank;
        public Text speedText;
        private bool isBoosting;
        private bool isReplaying;
        private bool readyToAddNitro;
        private bool mouseUnlocked;

        private Command.Command commandAccelerate, commandBrake, commandTurnLeft, commandDrift, commandJump, commandBoost;
        private List<float> steeringDirections = new List<float>();
        private List<float> throttleDirections = new List<float>();
        private List<Command.Command> commands = new List<Command.Command>();

        /* 
         *  Turn input curve: x real input, y value used
         *  My advice (-1, -1) tangent x, (0, 0) tangent 0 and (1, 1) tangent x
         */
        [SerializeField] AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

        [Header("Wheels")]
        [SerializeField] WheelCollider[] driveWheel = new WheelCollider[0];
        public WheelCollider[] DriveWheel => driveWheel;
        [SerializeField] WheelCollider[] turnWheel = new WheelCollider[0];

        public WheelCollider[] TurnWheel => turnWheel;

        // This code checks if the car is grounded only when needed and the data is old enough
        bool isGrounded = false;
        int lastGroundCheck = 0;
        public bool IsGrounded { get {
            if (lastGroundCheck == Time.frameCount)
                return isGrounded;

            lastGroundCheck = Time.frameCount;
            isGrounded = true;
            foreach (WheelCollider wheel in wheels)
            {
                if (!wheel.gameObject.activeSelf || !wheel.isGrounded)
                    isGrounded = false;
            }
            return isGrounded;
        }}

        [Header("Behaviour")]
        /*
         *  Motor torque represent the torque sent to the wheels by the motor with x: speed in km/h and y: torque
         *  The curve should start at x=0 and y>0 and should end with x>topspeed and y<0
         *  The higher the torque the faster it accelerate
         *  the longer the curve the faster it gets
         */
        [SerializeField] AnimationCurve motorTorque = new AnimationCurve(new Keyframe(0, 200), new Keyframe(50, 300), new Keyframe(200, 0));

        // Differential gearing ratio
        [Range(2, 16)]
        [SerializeField] float diffGearing = 4.0f;
        public float DiffGearing { get => diffGearing;
            set => diffGearing = value;
        }

        // Basicaly how hard it brakes
        [SerializeField] float brakeForce = 1500.0f;
        public float BrakeForce { get => brakeForce;
            set => brakeForce = value;
        }

        // Max steering hangle, usualy higher for drift car
        [Range(0f, 50.0f)]
        [SerializeField] float steerAngle = 30.0f;
        public float SteerAngle { get => steerAngle;
            set => steerAngle = Mathf.Clamp(value, 0.0f, 50.0f);
        }

        // The value used in the steering Lerp, 1 is instant (Strong power steering), and 0 is not turning at all
        [Range(0.001f, 1.0f)]
        [SerializeField] float steerSpeed = 0.2f;
        public float SteerSpeed { get => steerSpeed;
            set => steerSpeed = Mathf.Clamp(value, 0.001f, 1.0f);
        }

        // How hight do you want to jump?
        [Range(1f, 1.5f)]
        [SerializeField] float jumpVel = 1.3f;
        public float JumpVel { get => jumpVel;
            set => jumpVel = Mathf.Clamp(value, 1.0f, 1.5f);
        }

        // How hard do you want to drift?
        [Range(0.0f, 2f)]
        [SerializeField] float driftIntensity = 1f;
        public float DriftIntensity { get => driftIntensity;
            set => driftIntensity = Mathf.Clamp(value, 0.0f, 2.0f);
        }

        // Reset Values
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        /*
         *  The center of mass is set at the start and changes the car behavior A LOT
         *  I recomment having it between the center of the wheels and the bottom of the car's body
         *  Move it a bit to the from or bottom according to where the engine is
         */
        [SerializeField] Transform centerOfMass = null;

        // Force aplied downwards on the car, proportional to the car speed
        [Range(0.5f, 10f)]
        [SerializeField] float downforce = 1.0f;

        public float Downforce
        {
            get => downforce;
            set => downforce = Mathf.Clamp(value, 0, 5);
        }     

        // When IsPlayer is false you can use this to control the steering
        float steering;
        public float Steering { get => steering;
            set => steering = Mathf.Clamp(value, -1f, 1f);
        } 

        // When IsPlayer is false you can use this to control the throttle
        float throttle;
        public float Throttle { get => throttle;
            set => throttle = Mathf.Clamp(value, -1f, 1f);
        } 

        // Like your own car handbrake, if it's true the car will not move
        [SerializeField] bool handbrake;
        public bool Handbrake { get => handbrake;
            set => handbrake = value;
        } 
        
        // Use this to disable drifting
        [HideInInspector] public bool allowDrift = true;
        bool drift;
        public bool Drift { get => drift;
            set => drift = value;
        }         

        // Use this to read the current car speed (you'll need this to make a speedometer)
        [SerializeField] float speed = 0.0f;
        public float Speed => speed;

        [Header("Particles")]
        // Exhaust fumes
        [SerializeField] ParticleSystem[] gasParticles = new ParticleSystem[0];

        [Header("Boost")]
        // Disable boost
        [HideInInspector] public bool allowBoost = true;

        // Maximum boost available
        [SerializeField] float maxBoost = 100f;
        // public float MaxBoost { get => maxBoost;
        //     set => maxBoost = 100f;
        // }

        // Current boost available
        [SerializeField] float boost = 10f;
        // public float Boost { get => boost;
        //     set => boost = Mathf.Clamp(value, 0f, maxBoost);
        // }

        // Regen boostRegen per second until it's back to maxBoost
        [Range(0f, 1f)]

        /*
         *  The force applied to the car when boosting
         *  NOTE: the boost does not care if the car is grounded or not
         */
        [SerializeField] public float boostForce = 5000;
        // public float BoostForce { get => boostForce;
        //     set => boostForce = value;
        // }

        // Use this to boost when IsPlayer is set to false
        public bool boosting = false;
        // Use this to jump when IsPlayer is set to false
        public bool jumping = false;

        // Boost particles and sound
        [SerializeField] ParticleSystem[] boostParticles = new ParticleSystem[0];
        [SerializeField] AudioClip boostClip = default;
        [SerializeField] AudioSource boostSource = default;
        
        // Private variables set at the start
        Rigidbody rb = default;
        internal WheelCollider[] wheels = new WheelCollider[0];

        // Init rigidbody, center of mass, wheels and more
        void Start() {
#if MULTIOSCONTROLS
            Debug.Log("[ACP] Using MultiOSControls");
#endif
            if (boostClip != null) {
                boostSource.clip = boostClip;
            }
            
		    boost = maxBoost;
            nitroTank.fillAmount = boost / maxBoost;

            rb = GetComponent<Rigidbody>();
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            if (rb != null && centerOfMass != null)
            {
                rb.centerOfMass = centerOfMass.localPosition;
            }

            wheels = GetComponentsInChildren<WheelCollider>();

            // Set the motor torque to a non null value because 0 means the wheels won't turn no matter what
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0001f;
            }
        }

        // Visual feedbacks and boost regen
        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.LeftAlt) && !mouseUnlocked)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                mouseUnlocked = true;
            }else if (Input.GetKeyDown(KeyCode.LeftAlt) && mouseUnlocked)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                mouseUnlocked = false;
            }
            speedText.text = "Speed: " + Math.Floor(rb.velocity.magnitude);
            foreach (ParticleSystem gasParticle in gasParticles)
            {
                gasParticle.Play();
                ParticleSystem.EmissionModule em = gasParticle.emission;
                em.rateOverTime = handbrake ? 0 : Mathf.Lerp(em.rateOverTime.constant, Mathf.Clamp(150.0f * throttle, 30.0f, 100.0f), 0.1f);
            }
        }
        
        // Update everything
        void FixedUpdate () {
            
            if (isReplaying)
            {
                return;
            }

            
            // Mesure current speed
            speed = transform.InverseTransformDirection(rb.velocity).z * 3.6f;

            // Get all the inputs!
            if (isPlayer) {
                // Accelerate & brake
                if (throttleInput != "" && throttleInput != null)
                {
                    throttle = GetInput(throttleInput) - GetInput(brakeInput);
                }
                // Boost
                boosting = (GetInput(boostInput) > 0.5f);
                // Turn
                steering = turnInputCurve.Evaluate(GetInput(turnInput)) * steerAngle;
                // Dirft
                drift = GetInput(driftInput) > 0 && rb.velocity.sqrMagnitude > 100;
                // Jump
                jumping = GetInput(jumpInput) != 0;
            }

            // Direction
            foreach (WheelCollider wheel in turnWheel)
            {
                commands.Add(commandTurnLeft);
                steeringDirections.Add(steering);
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, steerSpeed);
            }

            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0.0001f;
                wheel.brakeTorque = 0;
            }

            // Handbrake
            if (handbrake)
            {
                foreach (WheelCollider wheel in wheels)
                {
                    // Don't zero out this value or the wheel completly lock up
                    wheel.motorTorque = 0.0001f;
                    wheel.brakeTorque = brakeForce;
                }
            }
            else if (throttle != 0 && (Mathf.Abs(speed) < 4 || Mathf.Sign(speed) == Mathf.Sign(throttle)))
            {
                commands.Add(commandAccelerate);
                throttleDirections.Add(throttle);
                foreach (WheelCollider wheel in driveWheel)
                {
                    commands.Add(commandAccelerate);
                    wheel.motorTorque = throttle * motorTorque.Evaluate(speed) * diffGearing / driveWheel.Length;
                }
            }
            else if (throttle != 0)
            {
                throttleDirections.Add(throttle);
                foreach (WheelCollider wheel in wheels)
                {
                    // commandBrake.Execute();
                    wheel.brakeTorque = Mathf.Abs(throttle) * brakeForce;
                }
            }

            // Jump
            if (jumping && isPlayer) {
                // turn this
                if (!IsGrounded)
                    return;
                commands.Add(commandJump);
                rb.velocity += transform.up * jumpVel;
                // into 
                
                // commandJump.Execute();
            }

            // Boost
            if (boosting && hasNitro()) {
                // turn this
                Debug.Log(boost);
                if (!isBoosting)
                {
                    commands.Add(commandBoost);
                    StartCoroutine(useNitro());
                }
                // boost -= Time.fixedDeltaTime;
                // if (boost < 0f) { boost = 0f; }
                
                // into 
                
                // commandBoost.Execute();
                
                
                if (boostParticles.Length > 0 && !boostParticles[0].isPlaying && hasNitro()) {
                    foreach (ParticleSystem boostParticle in boostParticles) {
                        boostParticle.Play();
                    }
                }

                if (boostSource != null && !boostSource.isPlaying && hasNitro()) {
                    boostSource.Play();
                }
            } else {
                if (boostParticles.Length > 0 && boostParticles[0].isPlaying) {
                    foreach (ParticleSystem boostParticle in boostParticles) {
                        boostParticle.Stop();
                    }
                }

                if (boostSource != null && boostSource.isPlaying) {
                    boostSource.Stop();
                }
            }

            // Drift
            if (drift && allowDrift) {
                // turn this
                Vector3 driftForce = -transform.right;
                driftForce.y = 0.0f;
                driftForce.Normalize();

                if (steering != 0)
                    driftForce *= rb.mass * speed/7f * throttle * steering/steerAngle;
                Vector3 driftTorque = transform.up * 0.1f * steering/steerAngle;
                
                rb.AddForce(driftForce * driftIntensity, ForceMode.Force);
                rb.AddTorque(driftTorque * driftIntensity, ForceMode.VelocityChange);
                // into this
                
                commands.Add(commandDrift);
                // commandDrift.Execute();
            }
            
            // Downforce
            // for command pattern need to apply downward force the entire time during replay
            rb.AddForce(-transform.up * speed * downforce);
            
        }

        // Reposition the car to the start position
        public void ResetPos() {
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void ToogleHandbrake(bool h)
        {
            handbrake = h;
        }

        // NITRO BOOST FOR PLAYER
        #region NitroBoost

        public IEnumerator useNitro()
        {
            isBoosting = true;
            float boostToLose = 10;
            if (boost > 0)
            {
                boost -= boostToLose;
                float currentNitroTankAmount = nitroTank.fillAmount;
                while (nitroTank.fillAmount > currentNitroTankAmount - 1/boostToLose)
                {
                    if (nitroTank.fillAmount == 0)
                    {
                        isBoosting = false;
                        StopAllCoroutines();
                    }
                    
                    rb.AddForce(transform.forward * boostForce * 3);
                    
                    nitroTank.fillAmount -= 1 / boostToLose * Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                Debug.Log("no more nitro to use, must buy more");
            }

            isBoosting = false;

        }

        public bool hasNitro()
        {
            return boost > 0;
        }

        public bool isNitroFull()
        {
            return Mathf.CeilToInt(boost) == Mathf.CeilToInt(maxBoost);
        }

        public void addNitro(float nitroToAdd)
        {
            StopAllCoroutines();
            boost += nitroToAdd;
            Debug.Log(boost);
            nitroTank.fillAmount = boost / maxBoost;
        }

        public void replayDelivery()
        {
            isReplaying = true;
            ResetPos();

            // for (int i = 0; i < commands.Count; i++)
            // {
            //     commands[i].Execute();
            // }
        }

        // public IEnumerator AddNitroCoroutine(float nitroToAdd)
        // {
        //     while(Time.timeScale == 0)
        //     {
        //         yield return null;
        //     }
        //     
        //     if (boost < maxBoost)
        //     {
        //         boost += nitroToAdd;
        //         float currentNitroTankAmount = nitroTank.fillAmount;
        //         while (nitroTank.fillAmount < currentNitroTankAmount + 1/nitroToAdd)
        //         {
        //             if (nitroTank.fillAmount == 1)
        //             {
        //                 readyToAddNitro = false;
        //                 StopCoroutine(nameof(AddNitroCoroutine));
        //             }
        //             Debug.Log(nitroTank.fillAmount);
        //             nitroTank.fillAmount += 1 / nitroToAdd * Time.deltaTime;
        //             yield return null;
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("nitro tank is full! Go drive fast!");
        //     }
        //
        //     readyToAddNitro = false;
        // }
        #endregion

        // MULTIOSCONTROLS is another package I'm working on ignore it I don't know if it will get a release.
#if MULTIOSCONTROLS
        private static MultiOSControls _controls;
#endif

        // Use this method if you want to use your own input manager
        private float GetInput(string input) {
#if MULTIOSCONTROLS
        return MultiOSControls.GetValue(input, playerId);
#else
        return Input.GetAxis(input);
#endif
        }
    }
    
    
    
}
