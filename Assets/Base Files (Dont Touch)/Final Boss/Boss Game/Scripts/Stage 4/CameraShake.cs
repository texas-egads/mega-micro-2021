using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class CameraShake : MonoBehaviour
    {
        public Vector3 originalPos;

        private int numShaking;

        void Start() 
        {
            originalPos = transform.position;
        }

        public void shakeCamera(float duration, float strength)
        {
            StartCoroutine(startShake(duration, strength));
        }

        private IEnumerator startShake(float duration, float strength)
        {
            float timeElapsed = 0;
            Vector3 newPos = originalPos;
            numShaking++;
            while(timeElapsed < duration)
            {
                newPos = originalPos;
                float deltaX = Random.Range(-strength, strength);
                float deltaY = Random.Range(-strength, strength);
                newPos.x += deltaX;
                newPos.y += deltaY;

                transform.position = newPos;

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = originalPos;
            numShaking--;
        }

        public void continousShake(float duration, float strength)
        {
            StartCoroutine(startContinousShake(duration, strength));
        }

        private IEnumerator startContinousShake(float duration, float strength)
        {
            float timeElapsed = 0;
            Vector3 newPos = originalPos;
            while(timeElapsed < duration)
            {
                if (numShaking <= 0)
                {
                    newPos = originalPos;
                    float deltaX = Random.Range(-strength, strength);
                    float deltaY = Random.Range(-strength, strength);
                    newPos.x += deltaX;
                    newPos.y += deltaY;

                    transform.position = newPos;
                }

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = originalPos;
        }
    }
}

