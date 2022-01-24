using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class PlayerAttacks : SingletonMonobehaviour<PlayerAttacks>
    {
        [SerializeField][Range(0f,10f)] private float _playerSpeed;
        private float _horizontal;
        private float _vertical;
        private bool _space;
        [SerializeField][Range(0f,10f)] private float _simpleAttackTime;
        [SerializeField] private Collider2D _rightAttackHitbox;
        [SerializeField] private Collider2D _leftAttackHitbox;
        [SerializeField][Range(0f,10f)] private float _grappleTime;
        [SerializeField] private Collider2D _grappleAttackHitbox;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _hitParticlesObject;
        [SerializeField] private ParticleSystem _hitParticles;
        private Coroutine _simpleAttackCoroutine;
        private Coroutine _grappleCoroutine;
        private bool _facingLeft = true;
        private int attackDiff = 0;
        protected override void Awake()
        {
            base.Awake();
        }
        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            _space = Input.GetKeyDown(KeyCode.Space);
            if(Input.GetKey(KeyCode.Space))
            {
                if(_grappleCoroutine == null)
                {
                    
                    if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && BGParallax.Instance._currentParallaxPosition != ParallaxPosition.right)
                    {
                        _grappleCoroutine = StartCoroutine(Grapple(false));
                        if(_simpleAttackCoroutine != null)
                        {
                            StopCoroutine(_simpleAttackCoroutine);
                            _simpleAttackCoroutine = null;
                        }
                    }
                    else if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && BGParallax.Instance._currentParallaxPosition != ParallaxPosition.left)
                    {
                        _grappleCoroutine = StartCoroutine(Grapple(true));
                        if(_simpleAttackCoroutine != null)
                        {
                            StopCoroutine(_simpleAttackCoroutine);
                            _simpleAttackCoroutine = null;
                        }
                    }
                }
            }
            else if(_simpleAttackCoroutine == null && _grappleCoroutine == null)
            {
                if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _simpleAttackCoroutine = StartCoroutine(SimpleAttack(true));
                }
                else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _simpleAttackCoroutine = StartCoroutine(SimpleAttack(false));
                }
            }
        }
        private IEnumerator SimpleAttack(bool left)
        {
            MinibossAudioManager.Instance.PlayFrogAttackSFX();

            if (attackDiff%2 == 0)
            {
                _animator.SetTrigger("Punch");
                attackDiff++;
            }
            else
            {
                _animator.SetTrigger("Kick");
                attackDiff++;
            }
            if(left)
            {
                FlipSide(true);
                // _leftAttackHitbox.gameObject.SetActive(true);
                yield return new WaitForSeconds(_simpleAttackTime);
                // _leftAttackHitbox.gameObject.SetActive(false);
            }
            else
            {
                FlipSide(false);
                // _rightAttackHitbox.gameObject.SetActive(true);
                yield return new WaitForSeconds(_simpleAttackTime);
                // _rightAttackHitbox.gameObject.SetActive(false);
            }
            _simpleAttackCoroutine = null;
        }

        private IEnumerator Grapple(bool left)
        {
            MinibossAudioManager.Instance.PlayFrogGrappleSFX();

            // DisableSimpleAttackHitboxes();
            // _grappleAttackHitbox.enabled = true;
            if (left)
            {
                FlipSide(true);
                _animator.SetTrigger("Grapple");
                BGParallax.Instance.MoveLeft();
                yield return new WaitForSeconds(_grappleTime);
            }
            else
            {
                FlipSide(false);
                _animator.SetTrigger("Grapple");
                BGParallax.Instance.MoveRight();
                yield return new WaitForSeconds(_grappleTime);
            }
            // _grappleAttackHitbox.enabled = false;
            _grappleCoroutine = null;
        }


        public void FlipSide(bool left)
        {
            if(left)
            {
                if (!_facingLeft)
                {
                    _spriteRenderer.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    _hitParticlesObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                    _facingLeft = true;
            }
            else
            {
                if(_facingLeft)
                {
                    _spriteRenderer.gameObject.transform.rotation = new Quaternion(0,180,0,0);
                    _hitParticlesObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                }
                _facingLeft = false;
            }
        }

        private void DisableSimpleAttackHitboxes()
        {
            _rightAttackHitbox.gameObject.SetActive(false);
            _leftAttackHitbox.gameObject.SetActive(false);
        }

        public void PlayHitParticles()
        {
            _hitParticles.Play();
        }

        public void Win()
        {
            _animator.SetBool("Win", true);
            StopAllCoroutines();
            
        }
    }
}