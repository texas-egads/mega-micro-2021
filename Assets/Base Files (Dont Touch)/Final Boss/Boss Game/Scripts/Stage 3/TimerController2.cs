using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SecretPuddle
{
    public class TimerController2 : MonoBehaviour
    {
        public Slider timerSlider;
        public float timerSpeed;

        // Start is called before the first frame update
        void Start()
        {
            float stageDuration = 7f;
            timerSlider.maxValue = stageDuration;
            timerSlider.value = stageDuration;

            StartCoroutine(startTimer());
        }

        private IEnumerator startTimer()
        {
            while(timerSlider.value > 0)
            {
                timerSlider.value -= Time.deltaTime / 100 * timerSpeed;
                yield return null;
            }

            Stage3.instance.LoseGame();
        }



        
    }
}

