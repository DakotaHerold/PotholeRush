using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Checkpoint : MonoBehaviour
    {
        public enum Direction
        {
            NONE, 
            TOP, 
            RIGHT, 
            BOTTOM,
            LEFT
        }

        public bool isFinishLine;
        public bool isVertical;

        public Direction intendedDirection; 

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

                    //Vector3 direction = transform.position - other.transform.position; 

                    //Debug.DrawLine(transform.position, other.transform.position, Color.blue, 2f);

                    // Raycast
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, LayerMask.NameToLayer("Player")))
                    {
                        Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow, 2f);


                       


                        Vector3 localPoint = hit.transform.InverseTransformPoint(hit.point); 
                        Vector3 localDir = localPoint.normalized;
                        float upDot = Vector3.Dot(localDir, Vector3.up);
                        float rightDot = Vector3.Dot(localDir, Vector3.right);

                        //float upPower = Mathf.Abs(upDot);
                        //float rightPower = Mathf.Abs(rightDot);

                        Direction currentDirection = Direction.NONE;

                        // Determine furthest 
                        if (isVertical)
                        {
                            if (upDot < 0)
                            {
                                // Down
                                //Debug.Log("Down");
                                currentDirection = Direction.BOTTOM; 
                            }
                            else if (upDot > 0)
                            {
                                // Up 
                                currentDirection = Direction.TOP;
                                //Debug.Log("Up");
                            }
                            else
                            {
                                currentDirection = intendedDirection;
                            }
                            Debug.Log(upDot); 
                        }
                        else 
                        {
                            // NOTE: Left and right are flipped because it's entry direction
                            if (rightDot < 0)
                            {
                                // Left
                                //Debug.Log("Left");
                                currentDirection = Direction.RIGHT; 
                            }
                            else if (rightDot > 0)
                            {
                                // Right 
                                //Debug.Log("Right");
                                currentDirection = Direction.LEFT; 
                            }
                            else
                            {
                                currentDirection = intendedDirection;
                            }
                            Debug.Log(rightDot); 
                        }



                        controller.AddCheckpoint(this, currentDirection);

                    }
                }
            }
        }

        
    }
}
