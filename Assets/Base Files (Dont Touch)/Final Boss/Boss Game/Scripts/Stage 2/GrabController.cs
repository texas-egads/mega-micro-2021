using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SecretPuddle
{
    public class GrabController : MonoBehaviour
    {
        public BatterySpawner batterySpawner;
        public int batteryGoal = 20;

        private bool isGrabbing;
        private int _batteryCount;
        public int batteryCount
        {   
            get
            {
                return _batteryCount;
            }
            set
            {
                _batteryCount = value;
                if(_batteryCount >= batteryGoal){
                    // win game
                    Stage2.instance.gameWon.Invoke();
                    FindObjectOfType<TimerController>().StopAllCoroutines();
                }
            }
        }

        private bool isGameOver;

        private void Start() {
            // if the battery goal is larger than the available batteries
            // spawn more batteries
            if(batteryGoal > batterySpawner.initialBatteryCount)
            {
                batterySpawner.spawnBatteries(batteryGoal - batterySpawner.initialBatteryCount);
            }

            Stage2.instance.gameLost.AddListener(stopGrabbing);
        }

        // Update is called once per frame
        void Update()
        {
            // Can only grab with space if a grab is not occuring, game
            // is not over, and battery goal has not been reached
            if(Input.GetKeyDown(KeyCode.Space) && !isGrabbing && batteryCount < batteryGoal && !isGameOver)
            {
                StartCoroutine(grabBattery(batterySpawner.getRandomBattery()));
            }
        }

        /// <summary>
        /// Prevents the player from grabbing any more batteries
        /// </summary>
        private void stopGrabbing()
        {
            isGameOver = true;
        }

        /// <summary>
        /// Performs a grab motion to grab the given battery
        /// </summary>
        /// <param name="battery">A battery game object</param>
        private IEnumerator grabBattery(GameObject battery)
        {
            // Initiate grab and remove battery from available batteries list
            isGrabbing = true;
            batterySpawner.removeBattery(battery);

            // make hand movement
            Vector2 initialPos = transform.position;
            transform.position = new Vector2(battery.transform.position.x, 0);

            float duration = .11f;
            transform.DOMoveY(battery.transform.position.y, duration / 2).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(duration / 2);
            BossGameManager.Instance.PlaySound("Grab");
            battery.transform.parent = transform;
            transform.DOMoveY(initialPos.y, duration / 2).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(duration / 2);

            // Destory battery and end grab
            Destroy(battery);
            batteryCount++;
            isGrabbing = false;
            yield return null;
        }
    }
}

