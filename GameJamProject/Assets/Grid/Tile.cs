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

        public bool isCurvedRoad; 
        public Sprite brokenSprite;
        public GameObject powerupPrefab; 
        public Transform[] powerupSpawns;
        public GameObject barrierPrefab;
        public Transform[] barrierSpawns; 
        private Sprite defaultSprite; 
        private bool isBroken; 
        public bool IsBroken { get { return isBroken; } }

        

        private float carDist;
        public float CarDist { get => carDist; set => carDist = value; }

        private SpriteRenderer spriteRenderer;

        private GameObject activeBarrier;

        public BrokenCollider trigger;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSprite = spriteRenderer.sprite; 
        }

        public void BreakRoad()
        {
            // TODO spawn barrier
            spriteRenderer.sprite = brokenSprite;
            SpawnBarrier();
            isBroken = true; 
        }

        public void FixRoad()
        {
            spriteRenderer.sprite = defaultSprite;
            Destroy(activeBarrier.gameObject);
            activeBarrier = null;
            isBroken = false;
        }

        public Vector3 GetBarrierLocation()
        {
            if (activeBarrier == null)
                return Vector3.zero;

            return activeBarrier.transform.position; 
        }

        private void SpawnBarrier()
        {
            if (barrierPrefab == null || barrierSpawns.Length < 1)
                return; 

            Vector3 pos = barrierSpawns[Random.Range(0, barrierSpawns.Length)].position;

            activeBarrier = GameObject.Instantiate(barrierPrefab);
            activeBarrier.transform.position = pos; 
        }

        public void SpawnPowerup()
        {
            if (powerupPrefab == null || powerupSpawns.Length < 1)
                return; 
            Vector3 pos = powerupSpawns[Random.Range(0, powerupSpawns.Length)].position;

            GameObject newPowerup = Instantiate(powerupPrefab);
            newPowerup.transform.position = pos; 
        }

        private void Update()
        {
            
        }

       
    }
}
