using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Team14
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Pie : MonoBehaviour
    {
        public enum State { Held, Fire, Disabled }

        public Action OnFire = () => { };
        public Action OnReset = () => { };

        [SerializeField] private GameObject _controls;

        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _fireSpeed = 5;

        private SpriteRenderer _sr;
        private Animator _animator;
        private Collider2D _collider;

        private float _minX;
        private float _maxX;
        private float _maxY;

        private Vector2 _startPos;

        private State _pieState;

        private Coroutine _routine;
        public State PieState 
        { 
            get { return _pieState; }
            set 
            {
                if (_routine != null) StopCoroutine(_routine);
                _pieState = value;
                switch (_pieState)
                {
                    case State.Fire:
                        OnFire();
                        break;
                }
            } 
        }

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            Vector2 bl = Camera.main.ViewportToWorldPoint(Vector2.zero);
            Vector2 ur = Camera.main.ViewportToWorldPoint(Vector2.one);

            _minX = bl.x;
            _maxX = ur.x;
            _maxY = ur.y;

            _startPos = transform.position;
        }

        private void Update()
        {
            HandleControls();
        }

        private void HandleControls()
        {
            if (PieState != State.Held) return;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                Fire();
                _controls.SetActive(false);
            }

            float x = Input.GetAxis("Horizontal");
            if (x != 0) _controls.SetActive(false);

            Vector3 translation = new Vector3(x * _moveSpeed * Time.deltaTime, 0, 0);
            if (InBounds(transform.position + translation))
                transform.Translate(translation);
        }

        private void Fire()
        {
            PieState = State.Fire;
            MinigameManager.Instance.PlaySound("Pie Throw");

            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(FireRoutine());
        }

        public void Stop()
        {
            if (_routine != null)
                StopCoroutine(_routine);
            _animator.Play("PieExplode");
        }

        private IEnumerator FireRoutine()
        {
            _animator.Play("PieThrow");

            while (InBounds(transform.position))
            {
                transform.Translate(new Vector3(0, _fireSpeed * Time.deltaTime, 0));
                yield return null;
            }
            Reset();
        }

        private bool InBounds(Vector2 pos)
        {
            return pos.x <= _maxX 
                && pos.x >= _minX 
                && pos.y <= _maxY;
        }

        public void Reset()
        {
            transform.position = _startPos;
            PieState = State.Held;
            _animator.Play("PieIdle");
            OnReset();
        }

        private void OnDestroy()
        {
            OnFire = null;
            OnReset = null;
        }
    }
}
