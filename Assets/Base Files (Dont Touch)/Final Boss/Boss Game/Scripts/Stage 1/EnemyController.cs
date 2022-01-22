using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        public bool isDestoyable;
        public float speed = 3f;

        [SerializeField]
        private int maxHealth = 3;
        private int _health;
        public int health
        {
            get
            {
                return _health;
            }
            set
            {
                if(value < _health && isDestoyable){
                    StartCoroutine(pushBack());
                }

                _health = Mathf.Clamp(value, 0, maxHealth);
                if(value <= 0 && isDestoyable){
                    Destroy(gameObject);
                }
            }
        }
        public ParticleSystem hitParticles;
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            health = maxHealth;
            rb = GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2(-speed, 0);

            Stage1.instance.gameLost.AddListener(stopMoving);
        }

        private void stopMoving(){
            rb.velocity = new Vector2(0, 0);
        }

        private IEnumerator pushBack(){
            hitParticles.Play();
            rb.velocity = new Vector2(speed / 2, 0);
            yield return new WaitForSeconds(0.05f);
            rb.velocity = new Vector2(-speed, 0);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Lose game if hit player
            if(other.gameObject.tag == "Player")
            {
                BossGameManager.Instance.PlaySound("PlayerHit");
                Stage1.instance.LoseGame();
            }

        }
    }
}
