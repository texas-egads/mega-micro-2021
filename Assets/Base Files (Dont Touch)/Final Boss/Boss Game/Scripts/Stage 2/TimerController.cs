using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SecretPuddle
{
    public class TimerController : MonoBehaviour
    {
        public Slider timerSlider;
        public float timerSpeed;

        // Start is called before the first frame update
        void Start()
        {
            float stageDuration = Stage2.instance.stageLength;
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

            Stage2.instance.LoseGame();
        }



        
    }
}

