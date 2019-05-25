using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Tile : MonoBehaviour
    {

        [ReadOnly]
        [SerializeField]
        public int row;
        [ReadOnly]
        [SerializeField]
        public int col;

        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            
        }
    }
}
