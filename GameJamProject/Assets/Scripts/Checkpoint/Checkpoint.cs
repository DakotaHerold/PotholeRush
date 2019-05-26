using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Checkpoint : MonoBehaviour
    {


        public bool isFinishLine;
  

        private void Awake()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
                CarController controller = other.gameObject.GetComponent<CarController>();

                if (controller != null)
                {
                    controller.AddCheckpoint(this); 
                }
            }
        }

        
    }
}
