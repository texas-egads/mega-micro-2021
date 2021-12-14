using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class SideMovement : MonoBehaviour
    {
        private float horizontal;
        public float moveSpeed;
        public Rigidbody2D rb;
        public float dampenFactor = 1.2f;
        private bool stunned;
        public float stunTime;
        public Animator anim;

        [Header("Cup Collision")]
        private int ingredientsGotten;
        public int requiredIngredients = 5;

        private bool gameOver;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var possibleIngredient = collision.gameObject.GetComponent<Ingredient>();
            if(possibleIngredient != null)
            {
                GotIngredient(possibleIngredient);
            }
        }

        private void FixedUpdate()
        {
            if (!stunned)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    horizontal = -1;
                    var newVel = rb.velocity;
                    newVel.x = horizontal * moveSpeed;
                    rb.velocity = newVel;
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    horizontal = 1;
                    var newVel = rb.velocity;
                    newVel.x = horizontal * moveSpeed;
                    rb.velocity = newVel;
                }

                if (Mathf.Abs((Input.GetAxisRaw("Horizontal"))) == 0 || !Input.anyKey)
                {
                    var dampVel = rb.velocity / dampenFactor;
                    rb.velocity = dampVel;
                }
            }
            else
            {
                var dampVel = rb.velocity / dampenFactor;
                rb.velocity = dampVel;
            }
        }

        public void GotIngredient(Ingredient ingredient)
        {
            if (ingredient.dangerous)
            {
                BossGameManager.Instance.PlaySound("badfood");
                Stun();
            }
            else
            {
                BossGameManager.Instance.PlaySound("goodfood");
                ingredientsGotten++;
                if(ingredientsGotten == requiredIngredients && !gameOver)
                {
                    Stage1.instance.gameWon.Invoke();
                    gameOver = true;
                }
            }
            Destroy(ingredient.gameObject);
        }
        private void Stun()
        {
            if (stunned == false)
            {
                stunned = true;
                anim.SetBool("Stunned", stunned);
                StartCoroutine(ResetStunned(stunTime));
            }
        }
        private IEnumerator ResetStunned(float delay)
        {
            yield return new WaitForSeconds(delay);
            stunned = false;
            anim.SetBool("Stunned", stunned);
        }
    }
}
