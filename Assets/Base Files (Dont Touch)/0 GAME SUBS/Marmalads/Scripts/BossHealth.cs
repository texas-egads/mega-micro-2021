using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Marmalads
{
    public class BossHealth : SingletonMonobehaviour<BossHealth>
    {
        
        // [SerializeField] private List<Sprite> _healthSprites;
        // [SerializeField] private Animator _playerAnimator;
        // [SerializeField] private SpriteRenderer _myHealthSpriteRenderer;
        [SerializeField] private Slider _myHealthSlider;
        [SerializeField] private Slider _myHealthSliderTrail;
        private Transform _player;
        private int _healthLost = 0;
        public float _trailTimer = 5;
        public float _smoothingTrail = 1;
        protected override void Awake()
        {
            base.Awake();
        }
        void Start()
        {
            // _myHealthSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _myHealthSlider.value = _myHealthSlider.maxValue;
            _player = PlayerAttacks.Instance.transform;
            StartCoroutine(HealthbarFollow());
        }
        public void TakeDamage()
        {
            _myHealthSlider.transform.parent.GetComponent<ShakeObject>().Shake();
            _myHealthSlider.value -= 1;
            StartCoroutine(SmoothingCoroutine());
            if(_myHealthSlider.value == 0)
            {
                MinibossScript.Instance._passedPhaseTwo = true;;
            }
        }        

        IEnumerator SmoothingCoroutine()
        {
            float timeElapsed = 0;
            while (timeElapsed < _trailTimer)
            {
                _myHealthSliderTrail.value = Mathf.Lerp(_myHealthSliderTrail.value, _myHealthSlider.value, _smoothingTrail * Time.deltaTime);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
        private IEnumerator HealthbarFollow()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 offset = _myHealthSlider.transform.parent.position - _player.position;
            while(true)
            {
                _myHealthSlider.transform.parent.position = Vector3.SmoothDamp(_myHealthSlider.transform.parent.position, _player.position + offset, ref velocity, .01f);
                yield return null;
            }
        }
    }
}
