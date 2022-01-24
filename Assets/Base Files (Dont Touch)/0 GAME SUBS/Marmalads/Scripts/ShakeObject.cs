using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{

    public class ShakeObject : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _shakeEase;
        [SerializeField] private float _defaultIntensity;
        [SerializeField] private float _defaultDuration;
        private Coroutine _currentShake;
        private Vector3 _initialPos;
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {

                Shake(_defaultIntensity, _defaultDuration);
            }
        }
        public void Shake(float intensity, float duration)
        {
            if(_currentShake != null)
            {
                StopCoroutine(_currentShake);
                transform.localPosition = _initialPos;
            }
            _initialPos = transform.localPosition;
            _currentShake = StartCoroutine(DoObjectShake(intensity, duration));
        }

        public void Shake()
        {
            if(_currentShake != null)
            {
                StopCoroutine(_currentShake);
                transform.localPosition = _initialPos;
            }
            _initialPos = transform.localPosition;
            _currentShake = StartCoroutine(DoObjectShake(_defaultIntensity, _defaultDuration));
        }

        private IEnumerator DoObjectShake(float intensity, float duration)
        {
            float timePassed = 0;
            while(timePassed <= duration)
            {
                timePassed+=Time.deltaTime;
                float intensityMultiplier = intensity * _shakeEase.Evaluate(timePassed/duration);
                transform.localPosition = transform.localPosition + Random.insideUnitSphere * intensity * intensityMultiplier;
                yield return null;
            }
            // transform.localPosition = _initialPos;
        }
    }
}
