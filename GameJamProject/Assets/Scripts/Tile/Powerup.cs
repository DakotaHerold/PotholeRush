using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<CarController>() != null)
            {
                if(other.gameObject.GetComponent<CarController>().PowerupCount < 3)
                {
                    other.gameObject.GetComponent<CarController>().AddPowerup();
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
}

