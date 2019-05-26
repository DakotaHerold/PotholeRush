using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class LapPanelManager : MonoBehaviour
    {
        public UICarLapTimer[] carTimers;

        private bool isActive = false; 

        public void Awake()
        {
            isActive = false; 
            foreach (UICarLapTimer timer in carTimers)
                timer.gameObject.SetActive(false); 
        }

        public void EnableTimers()
        {
            for(int iCar = 0; iCar < GameManager.Instance.cars.Count; ++iCar)
            {
                carTimers[iCar].SetBackgroundColor(GameManager.Instance.cars[iCar]);
                carTimers[iCar].SetTime(GameManager.Instance.cars[iCar], GameManager.Instance.UIManager.FormatTime(GameManager.Instance.cars[iCar].LapTime));
                carTimers[iCar].gameObject.SetActive(true);
            }
            isActive = true; 
        }

        public void DisableTimers()
        {
            isActive = false; 
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentState != GameManager.GAME_STATE.RUNNING)
                return;

            if (!isActive)
                return; 

            for (int iCar = 0; iCar < GameManager.Instance.cars.Count; ++iCar)
            {
                carTimers[iCar].SetTime(GameManager.Instance.cars[iCar], GameManager.Instance.UIManager.FormatTime(GameManager.Instance.cars[iCar].LapTime));
            }
        }
    }
}
