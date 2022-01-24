using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class TutorialEnemy : MonoBehaviour
    {
        [SerializeField] private float _timeToDie;
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private GameObject _nextTutorialObject;
        private bool _isDead;
        private Collider2D _myCollider;
        public bool _clawHitbox = false;
        public bool _leftHitbox = false;
        void Start()
        {
            _myCollider = gameObject.GetComponent<Collider2D>();
        }
        void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.tag ==  "Object 1")
            {
                // Debug.Log("enemy collider enter " + other.name);
                KillEnemy();
            }
        }
        private IEnumerator Die()
        {
            if(!_isDead)
            {
                _isDead = true;
                MinibossAudioManager.Instance.PlayGroundEnemyHitSFX();
                PlayerAttacks.Instance.PlayHitParticles();
                _myAnimator.enabled = true;
                yield return new WaitForSeconds(_timeToDie);
                if(_clawHitbox)
                {
                    Marmalads.Tutorial.Instance.FreeClaw(_leftHitbox);                    
                }
                if(_nextTutorialObject != null)
                {
                    _nextTutorialObject.SetActive(true);
                }
                else
                {
                    Marmalads.Tutorial.Instance.EndTutorial();
                }
                Destroy(this.gameObject);
            }
        }

        public void KillEnemy()
        {
            _myCollider.enabled = false;
            //MinibossScript.Instance.EnemyDie(this);
            StartCoroutine(Die());
        }

        public void StartDieCoroutine()
        {
            StartCoroutine(Die());
        }
    }
}
