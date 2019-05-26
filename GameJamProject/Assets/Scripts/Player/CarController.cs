using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class CarController : JamObject
    {
        public float acceleration;
        public float steering;
        public float stationaryTurnSpeed;
        [Space(10)]
        public float turnControl;

        private string carName;
        public string CarName { get => carName; set => carName = value; }

        private Color carColor; 
        public Color CarColor { get => carColor; set => carColor = value; }

        private CharacterController controller; 

        private Vector3 velocity;
        private Vector3 collisionForce; 
        private float angularChange;  
        private Vector3 force = new Vector3(1,0,0);
        private float rotation;

        private float currentAcceleration;

        private bool bouncing = false;

        public float baseBounceForce; 

        public float bounceTime; 
        private float bounceTimer;


        private float personalBestLapTime; 
        private float lapTime;
        public float LapTime { get { return lapTime; } }
        private int currentLap;

        private bool isLapActive;

        private List<Checkpoint> checkpoints;

        [Header("Game Properties")]
        public int NumCheckPoints; 

        private int powerupCount;
        public int PowerupCount { get { return powerupCount; } }

        

        protected override void Start()
        {
            base.Start();
            personalBestLapTime = float.MaxValue; 
            checkpoints = new List<Checkpoint>(); 
            controller = GetComponent<CharacterController>();
           // rb = GetComponent<Rigidbody>(); 
            rotation = 0.0f;
            powerupCount = 0;

            
        }

        public void EnableCar()
        {
            // TODO: Update if there's a countdown
            StartLap();
        }

        protected override void Update()
        {
            if (GameManager.Instance.CurrentState != GameManager.GAME_STATE.RUNNING)
                return; 
            base.Update();
            UpdateLap();
            AbilityUpdate(); 
        }

        void FixedUpdate()
        {
            if (GameManager.Instance.CurrentState != GameManager.GAME_STATE.RUNNING)
                return;
            UpdateCarMovement();
        }

        private void UpdateCarMovement()
        {
          

            Vector2 speed = transform.up * (InputHandler.Instance.VerticalAxis * acceleration);
            speed *= Time.fixedDeltaTime;
            Vector3 newSpeed = new Vector3(speed.x, speed.y, 0.0f);
            //rb.AddForce(speed);

            if (!bouncing)
                velocity += newSpeed;
            else 
                velocity -= newSpeed * baseBounceForce; 
            
            
            


            rotation = -InputHandler.Instance.HorizontalAxis;
            if (velocity.magnitude > 0)
                rotation *= steering * (velocity.magnitude / turnControl);
            else
                rotation *= stationaryTurnSpeed;

            //rotation = Mathf.Clamp(rotation, -maxTurnAngle, maxTurnAngle);
            //Debug.Log(rotation); 

            transform.Rotate(0, 0, rotation);


            Vector3 forward = new Vector3(0.0f, 0.5f, 0.0f);
            float steeringRightAngle;
            if (rotation > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }
            

            Vector3 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
            //Debug.DrawLine(transform.position, rightAngleFromForward, Color.green);

            float driftForce = Vector3.Dot(velocity, transform.right);
            Vector3 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);

            velocity += relativeForce;


            if (bouncing)
            {
                bounceTimer += Time.fixedDeltaTime; 

                if(bounceTimer >= bounceTime)
                {
                    // Reset 
                    bouncing = false;
                    bounceTimer = 0.0f;
                    velocity = Vector3.zero; 
                }
                

            }
            
            controller.Move(velocity);
            velocity = Vector3.zero;
        }

        private void UpdateLap()
        {
            if (!isLapActive)
                return;

            lapTime += Time.deltaTime; 
        }

        private void AbilityUpdate()
        {
            // TODO: Update key 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(powerupCount > 0)
                {
                    FixRoad();
                }
            }
        }

        public void StartLap()
        {
            isLapActive = true;
            lapTime = 0.0f;

            if(checkpoints != null)
                checkpoints.Clear(); 
            else
            {
                checkpoints = new List<Checkpoint>();
            }
        }

        public void LapComplete()
        {
            // Set best lap time
            if (lapTime < personalBestLapTime)
            {
                personalBestLapTime = lapTime;
                Debug.Log(carName + " ,New Personal Best Time: " + personalBestLapTime);
            }

            Debug.Log("Lap Time: " + lapTime);

            GameManager.Instance.RecordTime(lapTime, this); 

            // Update lap count
            currentLap++; 

            isLapActive = false;

            // TODO Check any game over state needed here 
            // TODO: Upgrade car 

            StartLap(); 
        }

        public void AddPowerup()
        {
            if (powerupCount >= 3)
                return; 

            powerupCount++;
            Debug.Log("Powerup Count: " + powerupCount);
            GameManager.Instance.PowerupAdded(this); 
        }

        private void FixRoad()
        {
            GameManager.Instance.roadManager.FixRoad(this); 
        }

        public void DecrementPowerupCount()
        {
            powerupCount--;
            if (powerupCount <= 0)
                powerupCount = 0;

            GameManager.Instance.PowerupRemoved(this); 
        }

        public void AddCheckpoint(Checkpoint newCheckpoint)
        {

            // If car is moving in reverse, cannot add checkpoint
            if (InputHandler.Instance.VerticalAxis < 0)
                return; 

            if (!newCheckpoint.isFinishLine)
            {
                if (checkpoints.Contains(newCheckpoint))
                    return;
            }

            
            
            checkpoints.Add(newCheckpoint); 

            if(checkpoints.Count >= NumCheckPoints && newCheckpoint.isFinishLine)
            {
                LapComplete(); 
            }
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null)
            {
                return;
            }

            if (!hit.gameObject.CompareTag("Barrier"))
            {
                return; 
            }

            bouncing = true;
        }

    }
}
