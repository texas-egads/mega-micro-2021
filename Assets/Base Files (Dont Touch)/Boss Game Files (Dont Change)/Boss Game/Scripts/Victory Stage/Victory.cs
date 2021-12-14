using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Victory : MonoBehaviour
    {
        private Transform transform;
        private float shakeDuration = 0f;
        private float shakeMagnitude = 0.7f;
        private float dampingSpeed = 1.0f;
        Vector3 initialPosition;

        void Awake()
        {
            if (transform == null)
            {
                transform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            initialPosition = transform.localPosition;
        }
        void Start()
        {
            shakeDuration = 9f;
        }

        void Update()
        {
            if (shakeDuration < 0f)
            {

            }
            else if (shakeDuration < 3f)
            {
                shakeMagnitude = 0.7f;
            }
            else if (shakeDuration < 6f)
            {
                shakeMagnitude = 0.3f;
            }
            else if (shakeDuration < 9f)
            {
                shakeMagnitude = 0.1f;
            }

            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }
}
    
