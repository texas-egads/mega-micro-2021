using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ApplesOrBees
{
    public class AttackBee : MonoBehaviour
    {
        public Vector2 target;
        public float speed;
        private Vector2 position;
        //private bool soundShouldPlay; // set this elsewhere in code

        // Start called before first frame update
        void Start()
        {
            target = new Vector2(3, 3);
            position = gameObject.transform.position;
            speed = 3.0f;
            gameObject.GetComponent<Renderer>().enabled = false;
            MinigameManager.Instance.minigame.gameWin = false;
            MinigameManager.Instance.PlaySound("lofibackground");
        }

        //Update called once per frame
        void Update()
        {
            if (transform.position.y == 3)
            {
                target = new Vector2(3, -2);
            }
            else if (transform.position.y == -2)
            {
                target = new Vector2(3, 3);
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            if (Input.GetKey("space"))
            {
                speed = 0.0f;
                gameObject.GetComponent<Renderer>().enabled = true;
                if (transform.position.y <= 1 && transform.position.y >= -1)
                {
                    MinigameManager.Instance.minigame.gameWin = true;
                }
            }
        }

    }
}
