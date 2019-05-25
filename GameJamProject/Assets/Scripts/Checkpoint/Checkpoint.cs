using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Checkpoint : MonoBehaviour
    {
        public enum Direction
        {
            TOP, 
            RIGHT, 
            BOTTOM,
            LEFT
        }

        public bool isFinishLine; 

        private void Awake()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            // TODO check direction is correct 

            if (other.gameObject.CompareTag("Player"))
            {
                CarController controller = other.gameObject.GetComponent<CarController>();

                if (controller != null)
                {
                    Vector3 direction = other.transform.position - transform.position;

                    Debug.DrawLine(transform.position, other.transform.position, Color.blue, 2f);



                    Vector3 localPoint = other.transform.InverseTransformPoint(transform.position);
                    Vector3 localDir = localPoint.normalized;
                    float upDot = Vector3.Dot(localDir, Vector3.up);
                    float rightDot = Vector3.Dot(localDir, Vector3.right);

                    float upPower = Mathf.Abs(upDot);
                    float rightPower = Mathf.Abs(rightDot);

                    // Determine furthest 
                    if(upPower > rightPower )
                    {
                        if(upDot < 0)
                        {
                            // Down
                            Debug.Log("Down");
                        }
                        else if (upDot > 0)
                        {
                            // Up 
                            Debug.Log("Up"); 
                        }
                        else
                        {
                            // Equal 
                        }
                    }
                    else if(rightPower > upPower)
                    {
                        if (rightDot < 0)
                        {
                            // Left
                            Debug.Log("Left");
                        }
                        else if (rightDot > 0)
                        {
                            // Right 
                            Debug.Log("Right");
                        }
                        else
                        {
                            // Equal 
                        }
                    }



                    controller.AddCheckpoint(this);
                }
            }
        }

        private void OnTriggerEnter(Collision collision)
        {
            
        }
    }
}
