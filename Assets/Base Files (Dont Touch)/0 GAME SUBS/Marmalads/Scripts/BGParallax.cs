using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public enum ParallaxPosition {left, middle, right}
    public class BGParallax : SingletonMonobehaviour<BGParallax>
    {

        [SerializeField] List<Transform> _bgElementTransformsList;
        [SerializeField] List<float> _bgElementsRightOffset;
        [SerializeField] private AnimationCurve _parallaxMovementCurve;
        [SerializeField][Range(0f, 10f)] private float _parallaxMovementTime;
        [SerializeField] private Camera _camera;
        [SerializeField] Transform _player;
        public ParallaxPosition _currentParallaxPosition;
        private Coroutine _currentCoroutine = null;
        private Vector3[] _bgLeftPositions;
        private Vector3[] _bgMiddlePositions;
        private Vector3[] _bgRightPositions;
        private Vector3 _rightPlayerPlacement;
        private Vector3 _leftPlayerPlacement;
        private Vector3 _initialPlayerPlacement;

        protected override void Awake() 
        {
            base.Awake();
        }
        void Start()
        {
            _initialPlayerPlacement = _player.transform.position;
            _rightPlayerPlacement = new Vector3(_initialPlayerPlacement.x + 8.15f, _initialPlayerPlacement.x);
            _leftPlayerPlacement = new Vector3(_initialPlayerPlacement.x - 8.15f, _initialPlayerPlacement.x);
            _bgLeftPositions = new Vector3[10];
            _bgMiddlePositions = new Vector3[10];
            _bgRightPositions = new Vector3[10];
            _currentParallaxPosition = ParallaxPosition.middle;
            for(int i = 0; i < _bgLeftPositions.Length; ++i)
            {
                _bgLeftPositions[i] = new Vector3(_bgElementsRightOffset[i],0);
                _bgMiddlePositions[i] = Vector3.zero;
                _bgRightPositions[i] = new Vector3(-_bgElementsRightOffset[i],0);
            }
            StartCoroutine(CameraFollow());
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                MoveLeft();
            }
            else if(Input.GetKeyDown(KeyCode.P))
            {
                MoveRight();
            }
        }

        public void MoveRight()
        {
            if(_currentCoroutine == null)
            {
                if(_currentParallaxPosition == ParallaxPosition.left)
                {
                    // Debug.Log("Moving from left to middle");
                    _currentCoroutine = StartCoroutine(ParallaxMove(_bgLeftPositions, _bgMiddlePositions, ParallaxPosition.middle));
                    StartCoroutine(MovePlayer(false));
                }
                else if(_currentParallaxPosition == ParallaxPosition.middle)
                {
                    // Debug.Log("Moving from middle to right");
                    _currentCoroutine = StartCoroutine(ParallaxMove(_bgMiddlePositions, _bgRightPositions, ParallaxPosition.right));
                    StartCoroutine(MovePlayer(false));
                }
            }
        }
        public void MoveLeft()
        {
            if(_currentCoroutine == null)
            {
                if(_currentParallaxPosition == ParallaxPosition.middle)
                {
                    // Debug.Log("Moving from middle to left");
                    _currentCoroutine = StartCoroutine(ParallaxMove(_bgMiddlePositions, _bgLeftPositions, ParallaxPosition.left));
                    StartCoroutine(MovePlayer(true));
                }
                else if(_currentParallaxPosition == ParallaxPosition.right)
                {
                    // Debug.Log("Moving from right to middle");
                    _currentCoroutine = StartCoroutine(ParallaxMove(_bgRightPositions, _bgMiddlePositions, ParallaxPosition.middle));
                    StartCoroutine(MovePlayer(true));
                }
            }
        }

        private IEnumerator MovePlayer(bool _left)
        {
            float timePassed = 0;
            Vector3 initialPos = _player.position;
            while(timePassed <= _parallaxMovementTime)
            {
                timePassed+=Time.deltaTime;

                float t = _parallaxMovementCurve.Evaluate(timePassed/_parallaxMovementTime);
                if(_left)
                {
                    _player.position = Vector3.Lerp(initialPos, initialPos + _leftPlayerPlacement, t);
                }
                else
                {
                    _player.position = Vector3.Lerp(initialPos, initialPos + _rightPlayerPlacement, t);
                }
                yield return null;
            }
        }
        private IEnumerator ParallaxMove(Vector3[] source, Vector3[] destination, ParallaxPosition endPosition)
        {
            float timePassed = 0;
            int numElements = _bgElementTransformsList.Count;
            while(timePassed <= _parallaxMovementTime)
            {
                timePassed+=Time.deltaTime;
                for(int i = 0; i < numElements; ++i)
                {
                    float t = _parallaxMovementCurve.Evaluate(timePassed/_parallaxMovementTime);
                    _bgElementTransformsList[i].position = Vector3.Lerp(source[i], destination[i], t);;
                }
                yield return null;
            }
            for(int i = 0; i < numElements; ++i)
            {
                _bgElementTransformsList[i].position = destination[i];
            }
            _currentParallaxPosition = endPosition;
            _currentCoroutine = null;
            // Debug.Log("Current Position: " + _currentParallaxPosition.ToString());
        }

        private IEnumerator CameraFollow()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 offset = new Vector3(0, -3.1f, 10);
            while(true)
            {
                _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, _player.position - offset, ref velocity, .1f);
                yield return null;
            }
        }
    }
}
