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
                other.gameObject.GetComponent<CarController>().AddPowerup();
                Destroy(this.gameObject);
            }
        }
    }
}

