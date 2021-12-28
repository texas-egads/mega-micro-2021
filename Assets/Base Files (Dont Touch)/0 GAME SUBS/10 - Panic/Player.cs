using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Panic
{
    public class Player : MonoBehaviour
    {
        public float speed = 10.0f;
        private bool canMove;

        private void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            canMove = true;
        }

        private void Update()
        {
            if (canMove)
            {
                float translation = Input.GetAxis("Horizontal") * speed;
                translation *= Time.deltaTime;
                transform.Translate(translation, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Object 1")
            {
                canMove = false;

                if (!MinigameManager.Instance.minigame.gameWin)
                {
                    MinigameManager.Instance.minigame.gameWin = true;
                    //MinigameManager.Instance.PlaySound("win");
                }
            }
        }
    }
}
