using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

namespace Jam
{
    public class CountdownTimer : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public float maxSize;
        public float minSize;

        public float counterDisplayTime;
        private float timer = 0.0f; 

        // Start is called before the first frame update
        void Awake()
        {
            if(text.gameObject.activeInHierarchy)
                text.gameObject.SetActive(false);
            timer = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartCountdownTimer()
        {
            text.gameObject.SetActive(true);
            timer = 0.0f; 
            StartCoroutine(CountdownRoutine()); 
        }

        IEnumerator CountdownRoutine()
        {
            text.gameObject.SetActive(true);
            text.text = "3";

            while(timer < counterDisplayTime)
            {
                text.fontSize = Mathf.Lerp(maxSize, minSize, timer);
                timer += Time.deltaTime;
                yield return null; 
            }

            timer = 0.0f;
            text.text = "2";

            while (timer < counterDisplayTime)
            {
                text.fontSize = Mathf.Lerp(maxSize, minSize, timer);
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0.0f;
            text.text = "1";

            while (timer < counterDisplayTime)
            {
                text.fontSize = Mathf.Lerp(maxSize, minSize, timer);
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0.0f;
            text.text = "GO!";

            Debug.Log("START RACING!");
            GameManager.Instance.StartGame(); 

            while (timer < counterDisplayTime)
            {
                text.fontSize = Mathf.Lerp(maxSize, minSize, timer);
                timer += Time.deltaTime;
                yield return null;
            }

            text.gameObject.SetActive(false);

            

        }
    }
}
