using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class UIManager : MonoBehaviour
    {

        public BestTimeManager BestTimeManager;
        public PowerupPanelManager PowerupPanelManager;
        public LapPanelManager LapPanelManager; 
        

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupLapTimers()
        {
            LapPanelManager.EnableTimers(); 
        }

        public void SetupPowerupUI()
        {
            PowerupPanelManager.EnableContainers(); 
        }

        public void SetBestTime(float time, CarController car)
        {
            string formattedTime = FormatTime(time);
            BestTimeManager.SetNewTime(car, formattedTime);
        }

        public string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            float milliseconds = time * 1000; 
            string formattedTime = string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            return formattedTime; 
        }

        public void PowerupAdded(CarController car)
        {
            PowerupPanelManager.AddPowerup(car); 
        }

        public void PowerupRemoved(CarController car)
        {
            PowerupPanelManager.RemovePowerup(car); 
        }
    }
}

