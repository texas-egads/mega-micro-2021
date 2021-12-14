using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum inHand { nothing, drink, fries, borgar, open}
namespace karina
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 50f;

        public Rigidbody2D rb;

        Vector2 movement;

        public GameObject drink;
        public GameObject fries;
        public GameObject borgar;

        public List<Sprite> handSprites;
        public inHand myStage;
        SpriteRenderer render;

        public Sprite none;

        public SpriteRenderer customerRenderer;
        public Sprite customerAngry;
        public Sprite customerHappy;

        private bool gameOver;
        private bool hasItem;
        private bool inputReady;

        private float horizontal;
        private float vertical;



        void Start()
        {
            myStage = inHand.nothing;
            render = GetComponent<SpriteRenderer>();
            render.sprite = handSprites[(int)myStage];

            MinigameManager.Instance.minigame.gameWin = false;

            MinigameManager.Instance.PlaySound("ding");
        }

        void Update()
        {
            /*movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);*/

            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            if (Input.GetKeyDown("space")) inputReady = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if ((hasItem == false) && (other.gameObject.tag != "Object 4"))
            {
                myStage = inHand.open;
                render.sprite = handSprites[(int)myStage];
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if ((hasItem == false) && (other.gameObject.tag != "Object 4"))
            {
                myStage = inHand.nothing;
                render.sprite = handSprites[(int)myStage];
            }
        }

        //pick up item in hand and give to customer
        void OnTriggerStay2D(Collider2D other)
        {
            if (!gameOver && inputReady)
            {
                inputReady = false;
                //pick up drink and/or put down other item
                if (other.gameObject.tag == "Object 1")
                {
                               
                    fries.GetComponent<SpriteRenderer>().enabled = true;
                    borgar.GetComponent<SpriteRenderer>().enabled = true;

                    myStage = inHand.drink;
                    render.sprite = handSprites[(int)myStage];
                    //print(myStage);
                    drink.GetComponent<SpriteRenderer>().enabled = false;
                    hasItem = true;

                    MinigameManager.Instance.PlaySound("pop");
                }

                //pick up fries and/or put down other item
                else if (other.gameObject.tag == "Object 2")
                {
                    drink.GetComponent<SpriteRenderer>().enabled = true;
                    borgar.GetComponent<SpriteRenderer>().enabled = true;

                    myStage = inHand.fries;
                    render.sprite = handSprites[(int)myStage];
                    //print(myStage);
                    fries.GetComponent<SpriteRenderer>().enabled = false;
                    hasItem = true;

                    MinigameManager.Instance.PlaySound("pop");
                }

                //pick up borgar and/or put down other item
                else if (other.gameObject.tag == "Object 3")
                {
                    drink.GetComponent<SpriteRenderer>().enabled = true;
                    fries.GetComponent<SpriteRenderer>().enabled = true;

                    myStage = inHand.borgar;
                    render.sprite = handSprites[(int)myStage];
                    //print(myStage);
                    borgar.GetComponent<SpriteRenderer>().enabled = false;
                    hasItem = true;

                    MinigameManager.Instance.PlaySound("pop");
                }


                //give to customer
                if ((other.gameObject.tag == "Object 4") && myStage == inHand.drink)
                {
                    if ((OrderManager.customerOrder == "drink") && (myStage == inHand.drink))
                    {
                        //Debug.Log("win");
                        MinigameManager.Instance.minigame.gameWin = true;
                        customerRenderer.sprite = customerHappy;

                        MinigameManager.Instance.PlaySound("win");
                    }
                    else
                    {
                        //Debug.Log("lose");
                        //print(OrderManager.customerOrder);
                        MinigameManager.Instance.minigame.gameWin = false;
                        customerRenderer.sprite = customerAngry;
                        //print("inside else");

                        MinigameManager.Instance.PlaySound("lose");
                    }
                    //print("drink order given");
                    myStage = inHand.nothing;
                    render.sprite = handSprites[(int)myStage];
                    //print(myStage);
                    gameOver = true;
                    hasItem = false;

                    MinigameManager.Instance.PlaySound("pop");
                }

                else if ((other.gameObject.tag == "Object 4") && myStage == inHand.fries)
                {
                    if ((OrderManager.customerOrder == "fries") && (myStage == inHand.fries))
                    {
                        //Debug.Log("win");
                        MinigameManager.Instance.minigame.gameWin = true;
                        customerRenderer.sprite = customerHappy;

                        MinigameManager.Instance.PlaySound("win");
                    }
                    else
                    {
                        //Debug.Log("lose");
                        MinigameManager.Instance.minigame.gameWin = false;
                        customerRenderer.sprite = customerAngry;

                        MinigameManager.Instance.PlaySound("lose");
                    }

                    myStage = inHand.nothing;
                    render.sprite = handSprites[(int)myStage];
                    //print(myStage);
                    gameOver = true;
                    hasItem = false;

                    MinigameManager.Instance.PlaySound("pop");
                }

                else if ((other.gameObject.tag == "Object 4") && myStage == inHand.borgar)
                {
                    if ((OrderManager.customerOrder == "borgar") && (myStage == inHand.borgar))
                    {
                        //Debug.Log("win");
                        MinigameManager.Instance.minigame.gameWin = true;
                        customerRenderer.sprite = customerHappy;

                        MinigameManager.Instance.PlaySound("win");
                    }
                    else
                    {
                        //Debug.Log("lose");
                        MinigameManager.Instance.minigame.gameWin = false;
                        customerRenderer.sprite = customerAngry;

                        MinigameManager.Instance.PlaySound("lose");
                    }

                    myStage = inHand.nothing;
                    render.sprite = handSprites[(int)myStage];
                    //print(myStage);
                    gameOver = true;
                    hasItem = false;

                    MinigameManager.Instance.PlaySound("pop");
                }
            }
        }
    }
}