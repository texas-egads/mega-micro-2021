using System.Collections;
using UnityEngine;


namespace DogWithReindeerAntlers
{
    public class BlenderBlade : MonoBehaviour
    {
        public float blendForce = 100;

        public float powerSpeed = .25f;
        private float currentPower = 0;

        private float minVol = 0;
        private float maxVol = .5f;
        private float currentMaxVol = 0;
        private float currentVol = 0;

        private float minPitch = .5f;
        private float maxPitch = 1;
        private float currentMaxPitch = .5f;
        private float currentPitch = .5f;

        private float minLowPass = 8500;
        private float maxLowPass = 22000;
        private float currentMaxLowPass = 22000;
        private float currentLowPass = 8500;

        [HideInInspector]
        public bool running = false;

        private float maxFruitPitch = 1.2f;
        private float minFruitPitch = .8f;
        public AudioSource fruitAudio;
        public AudioClip[] fruitSounds;
        public AudioSource bladeAudio;
        public AudioLowPassFilter bladeLowPass;

        float dampRad = 2f;
        Collider2D[] colliders;
        Vector2 bladeVector;
        Vector2 liquidVector;
        float xProd;

        float dampLevel = 0;
        float maxDampLevel = 15f;
        float maxVolumeDamp = .1f;
        float maxPitchDamp = .15f;

        private void Start()
        {
            TurnOn();
        }

        private void Update()
        {
            // Should also change low pass if the lid is on the blender

            colliders = Physics2D.OverlapCircleAll(transform.position, dampRad, 1 << LayerMask.NameToLayer("Ground"));

            bladeVector = (transform.right - transform.position);
            dampLevel = 0;
            foreach (CircleCollider2D collider in colliders)
            {
                liquidVector = (collider.transform.position - transform.position);
                xProd = (bladeVector.x * liquidVector.y) - (bladeVector.y * liquidVector.x);
                if (xProd < 0)
                {
                    dampLevel += 1 - Mathf.Clamp(Vector2.Distance(transform.position, collider.transform.position) / dampRad, 0, 1);
                }
            }
            dampLevel = Mathf.Clamp(dampLevel, 0, maxDampLevel) / maxDampLevel;

            currentPitch = Mathf.Clamp(currentMaxPitch - (dampLevel * maxPitchDamp), minPitch, currentMaxPitch);
            bladeAudio.pitch = currentPitch;
            currentVol = Mathf.Clamp(currentMaxVol - (dampLevel * maxVolumeDamp), minVol, currentMaxVol);
            bladeAudio.volume = currentVol;
            currentLowPass = Mathf.Lerp(currentMaxLowPass, minLowPass, dampLevel);
            bladeLowPass.cutoffFrequency = currentLowPass;
        }

        public void TurnOn()
        {
            running = true;
            //StopAllCoroutines();
            StartCoroutine(ChangePower());
        }

        public void TurnOff()
        {
            running = false;
            //StopAllCoroutines();
            StartCoroutine(ChangePower());
        }

        IEnumerator ChangePower()
        {
            bool direction = running;
            float startingVolume = bladeAudio.volume;
            float startingPitch = bladeAudio.pitch;
            float goalVolume = direction ? maxVol : minVol;
            float goalPitch = direction ? maxPitch : minPitch;
            float timer = direction ? currentPower : 1 - currentPower;
            bool complete = false;
            while (!complete)
            {
                if (direction != running)
                {
                    break;
                }
                complete = timer >= 1;
                currentMaxPitch = Mathf.Lerp(startingPitch, goalPitch, timer);
                currentMaxVol = Mathf.Lerp(startingVolume, goalVolume, timer);
                currentPower = Mathf.Clamp(direction ? timer : 1 - timer, 0, 1);
                timer += Time.deltaTime / powerSpeed;
                yield return null;
            }
        }

        private void ChopFruit()
        {
            fruitAudio.PlayOneShot(fruitSounds[Random.Range(0, fruitSounds.Length - 1)], Random.Range(.5f, 1));
            fruitAudio.pitch = Random.Range(minFruitPitch, maxFruitPitch);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (running)
            {
                if (collision.tag == "Object 1")
                {
                    collision.GetComponent<Fruit>().SpawnJuice();
                    ChopFruit();
                }

                if (collision.tag == "Ground")
                {
                    collision.attachedRigidbody.AddForce(Vector2.up * blendForce);
                }
            }
        }
    }
}
