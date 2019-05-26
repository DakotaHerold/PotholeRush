using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GAME_STATE
        {
            MAIN_MENU, 
            COUNTING_DOWN,
            PAUSED,
            RUNNING,
            COMPLETE
        }

        private GAME_STATE currentState; 
        public GAME_STATE CurrentState { get { return currentState; } }

        public GameObject carPrefab;
        public Transform[] carSpawns; 
        [HideInInspector]
        public List<CarController> cars;
        public List<Color> carColors; 
        public RoadManager roadManager;
        public UIManager UIManager;
        public CountdownTimer countdownManager;
        public Pause pauseManager;

        [HideInInspector]
        public float bestTime;

        int numberOfCars; 

        // Start is called before the first frame update
        void Start()
        {
            currentState = GAME_STATE.MAIN_MENU;
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupGame(int numPlayers)
        {
            currentState = GAME_STATE.COUNTING_DOWN; 
            SetNumCars(numPlayers);

            if (!countdownManager.gameObject.activeInHierarchy)
                countdownManager.gameObject.SetActive(true); 
            countdownManager.StartCountdownTimer(); 
        }


        public void SetNumCars(int numCars)
        {
            numberOfCars = numCars;
            cars = new List<CarController>(); 

            for(int i = 0; i < numberOfCars; ++i)
            {
                GameObject newCar = Instantiate(carPrefab);
                newCar.transform.position = carSpawns[i].position;
                newCar.transform.rotation = carSpawns[i].rotation;
                cars.Add(newCar.GetComponent<CarController>()); 
            }
        }

        public void StartGame()
        {
            Debug.Log("Game Started");
            currentState = GAME_STATE.RUNNING;
            // TODO, Fill me in based on the game!


            bestTime = float.MaxValue;
            for(int iCar = 0; iCar < cars.Count; ++iCar)
            {
                int playerIndex = iCar + 1;
                cars[iCar].CarName = "Player " + (playerIndex).ToString();

                if (iCar < carColors.Count)
                    cars[iCar].CarColor = carColors[iCar];

                // For Rewired input 
                cars[iCar].PlayerID = iCar;
                cars[iCar].EnableCar();
            }

            // Setup UI 
            UIManager.SetupLapTimers();
            UIManager.SetupPowerupUI(); 

            roadManager.Activate();

        }

        public void PauseGame(CarController car)
        {
            if(currentState == GAME_STATE.RUNNING)
            {
                currentState = GAME_STATE.PAUSED;
                pauseManager.PauseGame();
            }
            else if(currentState == GAME_STATE.PAUSED)
            {
                UnpauseGame(); 
            }
          
        }

        public void UnpauseGame()
        {
            pauseManager.UnpauseGame();
            currentState = GAME_STATE.RUNNING;
        }

        public void RecordTime(float time, CarController car)
        {
            if(time < bestTime)
            {
                // New best time
                bestTime = time;
                UIManager.SetBestTime(bestTime, car); 
            }
        }

        public void StunCars(CarController immuneCar)
        {
            foreach(CarController car in cars)
            {
                if(car != immuneCar)
                {
                    car.StunCar(); 
                }
            }
        }


        public void PowerupAdded(CarController car)
        {
            UIManager.PowerupAdded(car); 
        }

        public void PowerupRemoved(CarController car)
        {
            UIManager.PowerupRemoved(car); 
        }

        public void ResetGame()
        {
            Debug.Log("Game Reset"); 
            // TODO, Fill me in based on the game!
        }

        public void Quit()
        {
            //If we are running in a standalone build of the game
        #if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
        #endif

            //If we are running in the editor
        #if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }
}

