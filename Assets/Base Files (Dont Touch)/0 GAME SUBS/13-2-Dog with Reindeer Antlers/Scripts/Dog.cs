using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers2
{
    public class Dog : MonoBehaviour
    {
        private Rigidbody2D rb;
        public GameObject fightCloud;
        public float speed = 10000;
        public float maxSpeed = 10;
        private bool run = true;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (run)
            {
                rb.AddForce(Vector2.right * (speed * Time.fixedDeltaTime));
                if (rb.velocity.x > maxSpeed)
                {
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                MinigameManager.Instance.PlaySound("fight");
                fightCloud.SetActive(true);
                rb.velocity = Vector2.zero;
                run = false;
            }
        }
    }
}
