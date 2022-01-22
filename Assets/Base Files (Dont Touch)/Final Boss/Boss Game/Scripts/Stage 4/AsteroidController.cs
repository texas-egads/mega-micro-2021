using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidController : MonoBehaviour
    {
        public float speed;
        public Vector2 RPMRange;
        public ParticleSystem destructionParticles;
        [SerializeField] private int _health;
        public int health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    destructionParticles.Play();
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;

                    camShake.shakeCamera(.1f, .2f);

                    Destroy(this.gameObject, destructionParticles.startLifetime); 
                    BossGameManager.Instance.PlaySound("EnemyHit");
                }
            }
        }

        private Rigidbody2D rb;
        private bool shouldRotate = true;
        private CameraShake camShake;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -speed);
            camShake = Camera.main.GetComponent<CameraShake>();

            StartCoroutine(rotation());
        }

        private IEnumerator rotation()
        {
            float rotationAmount = Random.Range(RPMRange.x, RPMRange.y);
            int rotationDirection = Random.Range(0, 2) == 0 ? 1 : -1;
            //Debug.Log("rotation direction: " + rotationDirection);
            while(shouldRotate)
            {
                // keep rotating at a constant rate in the given direction;
                transform.Rotate(0, 0, rotationAmount * rotationDirection * Time.deltaTime, Space.World);

                yield return null;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.tag == "Player")
            {
                Stage4.instance.LoseGame();
                Debug.Log("Player died");
            }
        }
    }
}

