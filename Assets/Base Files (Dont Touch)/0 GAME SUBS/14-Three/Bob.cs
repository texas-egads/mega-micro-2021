using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team14
{
    public class Bob : MonoBehaviour
    {
        private float _offset;
        [SerializeField] private AnimationCurve _bobCurve;
        [SerializeField] private float _bobSpeed;
        [SerializeField] private float _bobScale;

        private Coroutine _routine;

        private IEnumerator BobRoutine()
        {
            float initY = transform.position.y;
            float t = 0;
            while (true)
            {
                while (t < 1)
                {
                    float newY = (_bobCurve.Evaluate((t + _offset) % 1) - .5f) * _bobScale  + initY;
                    transform.position = new Vector2(transform.position.x, newY);
                    t += Time.deltaTime * _bobSpeed;
                    yield return null;
                }
                t = 0;
            }
        }

        public void Stop()
        {
            if (_routine != null)
                StopCoroutine(_routine);
        }

        public void Start()
        {
            _offset = Random.value;
            _routine = StartCoroutine(BobRoutine());
        }
    }
}
