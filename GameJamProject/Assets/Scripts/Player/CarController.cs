using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class CarController : MonoBehaviour
    {
        public float acceleration;
        public float steering;
        private Rigidbody2D rb;

        private Vector3 velocity;
        private Vector3 force = new Vector3(1,0,0);
        private float rotation;

        private float currentAcceleration; 

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rotation = 0.0f; 
        }

        void FixedUpdate()
        {
            UpdateCarMovement(); 
        }

        private void UpdateCarMovement()
        {
            Vector2 speed = transform.up * (InputHandler.Instance.VerticalAxis * acceleration);
            //rb.AddForce(speed);
            rb.velocity += speed * Time.fixedDeltaTime; 

            float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
            if (direction >= 0.0f)
            {
                rb.rotation += -InputHandler.Instance.HorizontalAxis * steering * (rb.velocity.magnitude / 5.0f);
            }
            else
            {
                rb.rotation -= -InputHandler.Instance.HorizontalAxis * steering * (rb.velocity.magnitude / 5.0f);
            }

            Vector2 forward = new Vector2(0.0f, 0.5f);
            float steeringRightAngle;
            if (rb.angularVelocity > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }

            Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
            Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

            float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

            Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);


            Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

            //rb.AddForce(rb.GetRelativeVector(relativeForce));
            rb.velocity += rb.GetRelativeVector(relativeForce) * Time.fixedDeltaTime;
        }
    }
}
