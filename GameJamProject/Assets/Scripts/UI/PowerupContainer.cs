using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Jam
{
    public class PowerupContainer : MonoBehaviour
    {
        public Image background; 

        public GameObject[] powerupIcons;

        CarController car; 
        public CarController Car { get { return car; } }

        private void Awake()
        {
            for(int i = 0; i < powerupIcons.Length; ++i)
            {
                powerupIcons[i].gameObject.SetActive(false); 
            }
        }

        public void SetCarController(CarController newCar)
        {
            car = newCar;
            SetBackgroundColor(car); 
        }

        public void SetBackgroundColor(CarController car)
        {
            Color currentColor = background.color;
            Color newColor = car.CarColor;
            newColor.a = currentColor.a;
            background.color = newColor;
        }

        public void AddPowerup()
        {
            int index = car.PowerupCount-1;

            if (index < powerupIcons.Length)
                powerupIcons[index].SetActive(true); 
        }

        public void RemovePowerup()
        {
            for(int iIcon = 0; iIcon < powerupIcons.Length; ++iIcon)
            {
                if (iIcon > car.PowerupCount)
                    powerupIcons[iIcon].SetActive(false); 
            }
        }
    }
}
