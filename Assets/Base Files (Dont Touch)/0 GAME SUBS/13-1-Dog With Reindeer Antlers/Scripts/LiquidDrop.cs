using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace DogWithReindeerAntlers
{
    public class LiquidDrop : MonoBehaviour
    {
        public LiquidType startingType;
        [HideInInspector]
        public Dictionary<LiquidType, float> typePercentDict = new Dictionary<LiquidType, float>();
        private Dictionary<LiquidType, float> mixPercentDict = new Dictionary<LiquidType, float>();

        public float scale = 1;

        public float mixSpeed = .05f;
        public float maxDeformSpeed = 7.5f;
        private int mixCount = 0;

        public SpriteRenderer colorSprite;

        private float currentTransparency;
        private Color currentColor;
        private float currentSurfaceTension;

        private Rigidbody2D dropRB;
        private PointEffector2D pointEffector;

        private List<LiquidDrop> connectedDroplets = new List<LiquidDrop>();
        bool contacting = false;

        public AudioSource impactSource;
        public AudioLowPassFilter impactLowPass;
        public AudioClip[] impactClips;
        public AudioSource stagnantSource;
        public AudioClip[] stagnantClips;
        private float maxImpactVel = 2f;
        private float maxStagnantVel = 4f;
        private float minStagnantVel = .1f;
        private bool playingStagnant = false;

        private float maxPitchShift = .25f;
        private float minPitchShift = 0f;
        private float currentVel = 0;
        private float prevVel = 0;
        private float velDiff = 0;
        private float velThreshold = 2;

        Collider2D[] surroundingDrops;
        float dampLevel = 0;
        float maxDampLevel = 5;
        private float minLowPass = 6500;
        private float maxLowPass = 22000;

        void Start()
        {
            transform.localScale = Vector3.one * scale;
            foreach (KeyValuePair<LiquidType, LiquidProperties> keyValuePair in LiquidTypes.liquidTypeDict)
            {
                typePercentDict.Add(keyValuePair.Key, 0);
            }
            typePercentDict[startingType] = 1;
            colorSprite.color = LiquidTypes.liquidTypeDict[startingType].color;
            float startingTransparency = LiquidTypes.liquidTypeDict[startingType].transparency;
            pointEffector = GetComponentInChildren<PointEffector2D>();
            dropRB = GetComponentInChildren<Rigidbody2D>();
            pointEffector.forceMagnitude = LiquidTypes.liquidTypeDict[startingType].surfaceTension;

        }

        void FixedUpdate()
        {
            mixPercentDict = new Dictionary<LiquidType, float>(typePercentDict);

            mixCount = 1;
            foreach (LiquidDrop connectedLD in connectedDroplets)
            {
                foreach (KeyValuePair<LiquidType, float> keyValuePair in connectedLD.typePercentDict)
                {
                    mixPercentDict[keyValuePair.Key] += keyValuePair.Value;
                }
                mixCount++;
            }


            currentColor = Color.black;
            currentTransparency = 0;
            currentSurfaceTension = 0;
            foreach (KeyValuePair<LiquidType, float> keyValuePair in mixPercentDict)
            {
                typePercentDict[keyValuePair.Key] = Mathf.Lerp(typePercentDict[keyValuePair.Key], mixPercentDict[keyValuePair.Key] / mixCount, mixSpeed);

                currentColor += LiquidTypes.liquidTypeDict[keyValuePair.Key].color * typePercentDict[keyValuePair.Key];
                currentTransparency += LiquidTypes.liquidTypeDict[keyValuePair.Key].transparency * typePercentDict[keyValuePair.Key];
                currentSurfaceTension += LiquidTypes.liquidTypeDict[keyValuePair.Key].surfaceTension * typePercentDict[keyValuePair.Key];
            }

            colorSprite.color = currentColor;

            pointEffector.forceMagnitude = currentSurfaceTension;

            currentVel = dropRB.velocity.magnitude;
            velDiff = Mathf.Abs(currentVel - prevVel);
            if (velDiff > velThreshold)
            {
                StopAllCoroutines();
                stagnantSource.Stop();
                playingStagnant = false;
                dampLevel = CalcDamp();

                impactSource.PlayOneShot(impactClips[UnityEngine.Random.Range(0, impactClips.Length - 1)]);
                impactSource.pitch = Mathf.Lerp(1, .7f, dampLevel) + (UnityEngine.Random.Range(minPitchShift, maxPitchShift) * -1);
                //impactSource.volume = Mathf.Lerp(1, .7f, dampLevel) * scale * Mathf.Clamp((velDiff - velThreshold) / maxImpactVel, 0, 1);
                impactSource.volume = Mathf.Lerp(1, .7f, dampLevel) * Mathf.Clamp((velDiff - velThreshold) / maxImpactVel, 0, 1);
                impactLowPass.cutoffFrequency = Mathf.Lerp(maxLowPass, minLowPass, dampLevel);

            }
            else if (!playingStagnant && currentVel <= maxStagnantVel && currentVel >= minStagnantVel && contacting)
            {
                StartCoroutine(PlayStagnant());
            }

            prevVel = currentVel;

            connectedDroplets.Clear();
            contacting = false;
        }

        private float CalcDamp()
        {
            surroundingDrops = Physics2D.OverlapCircleAll(transform.position, scale, 1 << LayerMask.NameToLayer("Ground"));
            dampLevel = 0;
            foreach (CircleCollider2D collider in surroundingDrops)
            {
                dampLevel += 1 - Mathf.Clamp(Vector2.Distance(transform.position, collider.transform.position) / scale, 0, 1);
            }

            return Mathf.Clamp(dampLevel, 0, maxDampLevel) / maxDampLevel;
        }

        IEnumerator PlayStagnant()
        {
            playingStagnant = true;
            stagnantSource.PlayOneShot(stagnantClips[UnityEngine.Random.Range(0, stagnantClips.Length - 1)]);
            while (stagnantSource.isPlaying)
            {
                dampLevel = CalcDamp();

                stagnantSource.pitch = Mathf.Lerp(1.2f, .6f, dampLevel);
                stagnantSource.volume = contacting ? Mathf.Clamp(Mathf.Lerp(0, .5f, Mathf.PingPong(Mathf.Clamp(currentVel, 0, maxStagnantVel * 2) / maxStagnantVel, 1)) - (dampLevel / 4), 0, 1) : 0;

                yield return new WaitForFixedUpdate();
            }

            playingStagnant = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Default") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                contacting = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.tag == "Ground")
            {
                LiquidDrop currentLD = collider.GetComponent<LiquidDrop>();
                if (currentLD != null)
                {
                    connectedDroplets.Add(currentLD);
                }
            }
        }
    }
}
