using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class CameraShake : SingletonMonobehaviour<CameraShake>
    {
        [SerializeField] private AnimationCurve _shakeEase;
        [SerializeField] private float _defaultIntensity;
        [SerializeField] private float _defaultDuration;
        private Coroutine _currentShake;
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {

                ShakeCamera(_defaultIntensity, _defaultDuration);
            }
        }
        public void ShakeCamera(float intensity, float duration)
        {
            if(_currentShake != null)
            {
                StopCoroutine(_currentShake);
            }
            _currentShake = StartCoroutine(DoCameraShake(intensity, duration));
        }

        private IEnumerator DoCameraShake(float intensity, float duration)
        {
            float timePassed = 0;
            while(timePassed <= duration)
            {
                timePassed+=Time.deltaTime;
                float intensityMultiplier = intensity * _shakeEase.Evaluate(timePassed/duration);
                transform.localPosition = transform.localPosition + Random.insideUnitSphere * intensity * intensityMultiplier;
                yield return null;
            }
        }
    }
}
