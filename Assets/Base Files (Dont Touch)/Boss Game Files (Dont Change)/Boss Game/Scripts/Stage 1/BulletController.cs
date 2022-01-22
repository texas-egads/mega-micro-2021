using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float lifetime = 5f;
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(speed, 0);

            StartCoroutine(bulletLifetime());
        }

        /// <summary>
        /// Destorys bullet after the lifetime of the bullet has elapsed
        /// </summary>
        private IEnumerator bulletLifetime()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            EnemyController enemyCont = other.gameObject.GetComponent<EnemyController>();
            // Check to see that an enemy has been hit
            if(enemyCont != null)
            {
                // damage enemy
                enemyCont.health--;               
                if(enemyCont.isDestoyable) BossGameManager.Instance.PlaySound("EnemyHit");
                
                // Destory the bullet on impact
                Destroy(gameObject);
            }
        }
    }
}

