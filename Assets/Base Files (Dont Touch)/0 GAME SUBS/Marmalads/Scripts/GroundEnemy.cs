using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class GroundEnemy : MonoBehaviour
    {
        public bool _walkRight;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _timeToDie;
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Collider2D _playerDetector;
        private bool _isDead;
        private Collider2D _myCollider;
        public bool _isTouchingPlayer = false;
        void Start()
        {
            _myCollider = gameObject.GetComponent<Collider2D>();
        }

        void Update()
        {
            if(!_isDead && !_isTouchingPlayer)
            {
                // if (_walkRight)
                // {
                    transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
                    _playerDetector.transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
                // }
                // else
                // {
                //     transform.Translate(Vector3.left * _walkSpeed * Time.deltaTime);
                // }
            }
        }

        void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.tag ==  "Object 1")
            {
                // Debug.Log("enemy collider enter " + other.name);
                KillEnemy();
            }
            else if(other.gameObject.tag ==  "Player")
            {
                Debug.Log("PLAYER TAKE DAMAGE FROM " + gameObject.name + Time.time);
                PlayerHealth.Instance.TakeDamage(transform.position);
                CameraShake.Instance.ShakeCamera(0.35f, 1f);
                MinibossAudioManager.Instance.PlayFrogHitSFX();
            }
        }

        // void OnTriggerExit2D(Collider2D other) 
        // {
        //     if(other.gameObject.tag ==  "Object 1")
        //     {
        //         Debug.Log("enemy collider exit " + other.name);
        //         // KillEnemy();
        //     }
        // }

        private IEnumerator Die()
        {
            if(!_isDead)
            {
                _isDead = true;
                //CameraShake.Instance.ShakeCamera(0.25f, 1f);
                MinibossAudioManager.Instance.PlayGroundEnemyHitSFX();
                PlayerAttacks.Instance.PlayHitParticles();
                _myAnimator.SetTrigger("Die");
                yield return new WaitForSeconds(_timeToDie);
                Destroy(this.transform.parent.gameObject);
            }
        }

        public void KillEnemy()
        {
            _myCollider.enabled = false;
            MinibossScript.Instance.EnemyDie(this);
            StartCoroutine(Die());
        }

        public void StartDieCoroutine()
        {
            StartCoroutine(Die());
        }
    }
}
