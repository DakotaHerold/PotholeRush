using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Jam
{

    public class BestTimeManager : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;

        // Start is called before the first frame update
        void Start()
        {
            textMesh.text = "";
        }

        public void SetNewTime(CarController car, string newTime)
        {
            string finalStr = car.CarName + " - " + "Best Time: " + newTime;
            textMesh.text = finalStr;
            textMesh.color = car.CarColor; 
        }
    }
}
