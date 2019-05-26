using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class PowerupPanelManager : MonoBehaviour
    {
        public PowerupContainer[] containers;



        public void Awake()
        {
            foreach (PowerupContainer container in containers)
            {
                container.gameObject.SetActive(true);
                container.gameObject.SetActive(false);

            }
        }

        public void EnableContainers()
        {
            for (int iCar = 0; iCar < GameManager.Instance.cars.Count; ++iCar)
            {
                containers[iCar].SetCarController(GameManager.Instance.cars[iCar]);
                containers[iCar].gameObject.SetActive(true);
            }
        }

        public void AddPowerup(CarController car)
        {
            foreach(PowerupContainer container in containers)
            {
                if (container.Car == car)
                {
                    container.AddPowerup(); 
                    break; 
                }
            }
        }

        public void RemovePowerup(CarController car)
        {
            foreach (PowerupContainer container in containers)
            {
                if (container.Car == car)
                {
                    container.RemovePowerup();
                    break;
                }
            }
        }
    }
}