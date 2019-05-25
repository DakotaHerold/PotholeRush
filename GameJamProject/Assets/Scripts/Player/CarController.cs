﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class CarController : MonoBehaviour
    {
        public float acceleration;
        public float steering;
        public float stationaryTurnSpeed;
        [Space(10)]
        public float turnControl; 
  


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

        

        void Start()
        {
            controller = GetComponent<CharacterController>();
           // rb = GetComponent<Rigidbody>(); 
            rotation = 0.0f; 
        }

        private void Update()
        {
            collisionForce = Vector3.zero;
        }

        void FixedUpdate()
        {
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


        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null)
            {
                return;
            }

            //if(hit.gameObject.CompareTag("Wall"))
            //{
            //    Debug.DrawRay(hit.point, hit.normal, Color.green, 0.3f); // draw green normal
            //    Debug.DrawRay(hit.point, -velocity, Color.red, 0.3f); // draw red "in" velocity
            //    velocity = Vector3.Reflect(velocity, hit.normal); // reflect the velocity
            //    velocity *= 5f * Time.fixedDeltaTime; 
            //    controller.Move(velocity); 
            //    Debug.DrawRay(hit.point, velocity, Color.blue, 0.3f); // draw blue "out" velocity
            //    return; 
            //}

            if (!hit.gameObject.CompareTag("Barrier"))
            {
                return; 
            }

            bouncing = true;
        }

    }
}
