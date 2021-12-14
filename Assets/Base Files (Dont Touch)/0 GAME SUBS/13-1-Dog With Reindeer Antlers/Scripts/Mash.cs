using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DogWithReindeerAntlers
{
    public class Mash : MonoBehaviour
    {
        public WinLose winLose;
        public Slider slider;
        public BlenderLid blenderLid;
        private float totalAmount = 10f;
        private float currentAmount = 10f;
        //decreaseRate is in percentage per second
        private float decreaseRate = .5f;
        //what percentage to add to the slider
        private float addAmount = .10f;

        private bool blending = true;

        float maxTime = 4.5f;
        float timer = 0;

        void Start()
        {
            totalAmount = slider.maxValue;
            currentAmount = slider.maxValue;
        }

        void Update()
        {
            if (blending)
            {
                timer += Time.deltaTime;
                
                if(timer >= maxTime)
                {
                    winLose.Win();
                    blending = false;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    winLose.Win();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentAmount += totalAmount * addAmount;
                    currentAmount = Mathf.Clamp(currentAmount, 0, totalAmount);
                }

                currentAmount -= ((totalAmount * decreaseRate) * Time.deltaTime);
                currentAmount = Mathf.Clamp(currentAmount, 0, totalAmount);

                slider.value = currentAmount;

                blenderLid.UpdateProgress(totalAmount - currentAmount);
                if (currentAmount == 0)
                {
                    winLose.Lose();
                    blending = false;
                }
            }
        }
    }
}
