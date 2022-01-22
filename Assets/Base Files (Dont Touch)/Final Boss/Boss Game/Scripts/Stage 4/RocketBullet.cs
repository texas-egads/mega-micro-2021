using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RocketBullet : MonoBehaviour
    {
        public float speed;
        public float bulletLifetime;

        private Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, speed);
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            AsteroidController asteriod = other.gameObject.GetComponent<AsteroidController>();
            if (asteriod != null)
            {
                // damage asteriod
                asteriod.health--;

                // destory bullet
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Destory this bullet after its lifetime has elapsed
        /// </summary>
        private IEnumerator startLifetime()
        {
            yield return new WaitForSeconds(bulletLifetime);
            Destroy(this);
        }
    }
}

