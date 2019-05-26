using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class BrokenCollider : MonoBehaviour
    {
        public Tile parentTile;

        public BoxCollider box;

        public bool containsPlayer; 

        // Start is called before the first frame update
        void Awake()
        {
            box = GetComponent<BoxCollider>(); 
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                
                containsPlayer = true;

                if(parentTile.IsBroken)
                {
                    // TODO: Slow player 
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                containsPlayer = true; 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                containsPlayer = false;

                // TODO: Unslow player 
            }
        }
    }
}
