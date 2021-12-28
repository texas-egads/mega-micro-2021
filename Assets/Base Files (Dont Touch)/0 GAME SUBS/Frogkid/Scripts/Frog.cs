using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FROGKID
{

    public class Frog : MonoBehaviour
    {
        Rigidbody2D body;

        float horizontal;
        float vertical;

        public float runSpeed = 7.0f;

        [SerializeField]
        SeekMinigame seekMinigame;

        [SerializeField]
        Animator animator;

        [SerializeField]
        Transform transform;

        [SerializeField]
        Collider2D collider;

        [SerializeField]
        float gravity = 0.1f;

        Transform homePlanet;

        bool lose;
        bool gameover;

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            body.freezeRotation = true;
            lose = false;
            gameover = false;
        }

        void Update()
        {
            if (!gameover)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
            } else if(lose)
            {
                // lose
                transform.localScale = transform.localScale * 0.99f;
            } else
            {
                // win 
                transform.localScale = transform.localScale * 0.99f;
                body.velocity = (Vector2) (gravity * (homePlanet.transform.position - transform.position));

            }

        }

        private void FixedUpdate()
        {
            //body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
            body.AddForce(new Vector2(horizontal * runSpeed, vertical * runSpeed));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Planet planet = collision.gameObject.GetComponent<Planet>();
            if (planet != null && !gameover)
            {
                if (planet.getIsHome())
                {
                    animator.SetTrigger("win");
                    gameover = true;
                    homePlanet = planet.transform;
                    //body.constraints = RigidbodyConstraints2D.FreezePosition;
                    collider.enabled = false;
                    seekMinigame.doWin();
                } else
                {
                    gameover = true;
                    lose = true;
                    animator.SetTrigger("lose");
                    body.AddForce(Vector3.left * 10.0f);
                    //body.AddForce(transform.TransformDirection(Vector3.left));
                    collider.enabled = false;
                    body.freezeRotation = false;
                    body.AddTorque(200.0f);
                    seekMinigame.doLose();
                    
                }
            }
                
        }

    }

}