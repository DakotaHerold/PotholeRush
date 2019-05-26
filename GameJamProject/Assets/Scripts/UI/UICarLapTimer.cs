using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Jam
{
    public class UICarLapTimer : MonoBehaviour
    {
        public Image background;
        public TextMeshProUGUI text;

        public void SetBackgroundColor(CarController car)
        {
            Color currentColor = background.color;
            Color newColor = car.CarColor;
            newColor.a = currentColor.a;
            background.color = newColor; 
        }

        public void SetTime(CarController car, string formattedTime)
        {
            text.text = car.CarName + " - " + formattedTime; 
        }
    }
}
