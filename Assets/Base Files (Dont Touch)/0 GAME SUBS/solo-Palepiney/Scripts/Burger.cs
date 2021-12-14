using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palepiney
{
    public class Burger : MonoBehaviour
    {
        Rigidbody2D rb;
        private SpriteRenderer rend;
        private Sprite norm, honey;
        bool movingRight = true;
        bool moving = true;

        // Start is called before the first frame update
        void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            rb = GetComponent<Rigidbody2D>();
            rend = GetComponent<SpriteRenderer>();
            norm = Resources.Load<Sprite>("Pancakes");
            honey = Resources.Load<Sprite>("Pancakes with honey");
            rend.sprite = norm;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x > 5)
                movingRight = false;
            if (transform.position.x < -5)
                movingRight = true;
        }

        void FixedUpdate()
        {
            if (moving)
            {
                if (movingRight)
                    moveRight();
                else
                    moveLeft();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        void OnTriggerEnter2D(Collider2D ketch)
        {
            if(ketch.tag == "Object 1")
            {
                moving = false;
                rend.sprite = honey;

                if (!MinigameManager.Instance.minigame.gameWin)
                {
                    MinigameManager.Instance.minigame.gameWin = true;
                    MinigameManager.Instance.PlaySound("win");
                }
            }
        }

        void moveRight()
        {
            rb.velocity = new Vector2(5, 0);
        }

        void moveLeft()
        {
            rb.velocity = new Vector2(-5, 0);
        }
    }
}