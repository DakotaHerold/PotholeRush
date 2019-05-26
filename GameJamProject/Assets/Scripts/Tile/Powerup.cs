using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Powerup : MonoBehaviour
    {

        public bool isActive = false;

        private float frequency = 10f;
        private float amplitude = 1.5f;
        public Vector3 _startPosition;

        void Start()
        {
            //isActive = false; 
            //_startPosition = transform.position;
        }

        void Update()
        {
            if (!isActive)
                return;

            transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time * frequency) * Time.deltaTime * amplitude, 0.0f);
        }

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

