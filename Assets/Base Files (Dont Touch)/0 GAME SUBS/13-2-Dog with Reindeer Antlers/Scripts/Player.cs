using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers2
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        public float speed = 10000;
        public float maxSpeed = 10;
        public float jumpForce = 220000;

        private bool run = true;
        private bool grounded = false;
        private bool jumpPressed = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            MinigameManager.Instance.minigame.gameWin = true;
        }

        private void Update()
        {
            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                jumpPressed = true;
            }
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
                if (jumpPressed)
                {
                    rb.AddForce(Vector2.up * (jumpForce * Time.fixedDeltaTime));

                    jumpPressed = false;
                }
            }

            grounded = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Object 1")
            {
                run = false;
                rb.velocity = Vector2.zero;
                animator.SetTrigger("confused");
                MinigameManager.Instance.PlaySound("hit");
                MinigameManager.Instance.minigame.gameWin = false;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                grounded = true;
            }
        }
    }
}
