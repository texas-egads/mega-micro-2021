using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BeeNice
{
    public class RocketHit : MonoBehaviour
    {
        public Animator explode;
        private Transform transform;
        public SideToSideMovement movementScript;
        private float shakeDuration = 0f;
        private float shakeMagnitude = 0.7f;
        private float dampingSpeed = 1.0f;
        private bool gameOver;
        Vector3 initialPosition;

        public bool shouldPlay = true;
        private IEnumerator PlayLoopingSound(string soundName)
        {
            while (shouldPlay)
            {
                BossGameManager.Instance.PlaySound(soundName);
                yield return new WaitForSeconds(10.188f);
            }
        }
        void Awake()
        {
            if (transform == null)
            {
                transform = GetComponent(typeof(Transform)) as Transform;
            }
            BossGameManager.Instance.PlaySound("rocketlaunch");
            StartCoroutine(PlayLoopingSound("rocketloop"));
        }

        void OnEnable()
        {
            initialPosition = transform.localPosition;
        }
        public void RocketIsHit()
        {
            explode.SetTrigger("Explode");
            Destroy(movementScript);
            gameOver = true;
            shakeDuration = 0.5f;
        }

        void Update()
        {

            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition;
                transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                //transform.localPosition = initialPosition;
                initialPosition = transform.localPosition;
                if (gameOver)
                {
                    Stage5.instance.LoseGame();
                }
            }
        }
    }
}
    
