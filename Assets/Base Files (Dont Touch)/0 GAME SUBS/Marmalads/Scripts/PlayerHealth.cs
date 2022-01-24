using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Marmalads
{
    public class PlayerHealth : SingletonMonobehaviour<PlayerHealth>
    {
        [SerializeField] private List<Sprite> _healthSprites;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private SpriteRenderer _myHealthSpriteRenderer;
        private int _healthLost = 0;
        protected override void Awake()
        {
            base.Awake();
        }
        void Start()
        {
            // _myHealthSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        public void TakeDamage(Vector3 enemyPos)
        {
            _healthLost++;
            DamageFlip(enemyPos);
            if(_healthLost >= _healthSprites.Count - 1)
            {
                _myHealthSpriteRenderer.sprite = _healthSprites[3];
                _playerAnimator.SetTrigger("Hurt");
                _playerAnimator.SetBool("Dead", true);
                MinibossScript.Instance.Lose();
            }
            else
            {
                _myHealthSpriteRenderer.sprite = _healthSprites[_healthLost];
                _playerAnimator.SetTrigger("Hurt");
            }
        }
        
        private void DamageFlip(Vector3 enemyPos) {
            // Debug.Log("ouch");
            // if(other.gameObject.tag == "Enemy")
            // {
                //Check if enemy is on the right side and flip player if player is looking left
                if(enemyPos.x > transform.position.x)
                {
                    PlayerAttacks.Instance.FlipSide(false);
                }
                else
                {
                    //Otherwise, flip player left if player is looking right
                    PlayerAttacks.Instance.FlipSide(true);
                }

        }
    }

}
