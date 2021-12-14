using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team14
{
    public class Pulsator : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _scale;
        [SerializeField] private float _speed =3;
        private Coroutine _routine;
        private Vector2 _originalScale;

        public void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void Start()
        {
            Stop();
            _routine = StartCoroutine(PulseRoutine());
        }

        public void Stop()
        {
            if (_routine != null) StopCoroutine(_routine);
        }

        IEnumerator PulseRoutine()
        {
            float t = 0;
            while (true)
            {
                while (t < 1)
                {
                    transform.localScale = _originalScale * _scale.Evaluate(t);
                    yield return null;
                    t = Mathf.Clamp01(t + Time.deltaTime * _speed);
                }
                t = 0;
            }
        }
    }
}
