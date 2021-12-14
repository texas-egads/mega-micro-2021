using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BeekeeperKeepers {
    public class Ingredient : MonoBehaviour {
        [HideInInspector] public IngredientType myType;
        public float fallSpeed;

        public SpriteList fallingSprites;
        public SpriteList restingSprites;
        private SpriteRenderer myRenderer;
        private WinChecker winChecker;
        private Message message;
        private bool falling = true;

        private void Awake() {
            myRenderer = GetComponent<SpriteRenderer>();
            winChecker = FindObjectOfType<WinChecker>();
            message = FindObjectOfType<Message>();
        }

        // Update is called once per frame
        void Update() {
            if (falling) {
                float newY = transform.position.y - fallSpeed * Time.deltaTime * 30f;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            // Debug.Log("oof");
            if (collision.gameObject.CompareTag("Object 1"))
            {
                MinigameManager.Instance.PlaySound("place");
                falling = false;
                gameObject.tag = "Object 1"; // marks this as part of the sandwich
                myRenderer.sprite = restingSprites.sprites[(int)myType]; // spaghetti taste so good
                transform.SetParent(collision.gameObject.transform);
            }
            // else (sorry friend)
            if (myType == IngredientType.BUN) 
            {
                bool hasWon = winChecker.hasWon();
                message.DisplayMessage(hasWon);
                Debug.Log("hasWon: " + hasWon);
                if (hasWon)
                {
                    MinigameManager.Instance.PlaySound("win");
                }
                else 
                {
                    MinigameManager.Instance.PlaySound("lose");
                }
                MinigameManager.Instance.minigame.gameWin = hasWon;
            }
        }

        public void Initialize(IngredientType newType, SpawnerManager sm) {
            myType = newType;
            // Debug.Log("smanager is " + smanager );
            // Debug.Log("myRenderer is " + myRenderer);
            myRenderer.sprite = fallingSprites.sprites[(int)myType];
        }
    }
}