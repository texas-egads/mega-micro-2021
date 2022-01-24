using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class BossClaw : MonoBehaviour
    {
        private BoxCollider2D _myCollider;
        // Start is called before the first frame update
        void Start()
        {
            _myCollider = gameObject.GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Object 1")
            {
                Debug.Log(gameObject.name + " collided with punch");
                MinibossAudioManager.Instance.PlayBossHitSFX();
                BossHealth.Instance.TakeDamage();
            }
            else if(other.tag == "Player")
            {
                if(MinibossScript.Instance._currentClawAttackStage != ClawAttackStage.Attacking)
                    PlayerHealth.Instance.TakeDamage(transform.position);
            }
            else if(other.tag == "Object 3")
            {
                MinibossScript.Instance.SetClawAttackState(ClawAttackStage.Attacking);
            }
        }
        
        public void SetColliderState(bool active)
        {
            _myCollider.enabled = active;
        }
    }
}