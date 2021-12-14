using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CriticalBass
{
    public class InputHandler : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public bool isMoving;
        public bool cooldown;
        public bool canMove;

        public Rigidbody2D rb;

        public GameObject headPoint;

        Vector2 movement;
        Vector2 targetPos;

        void Start()
        {
            isMoving = false;
            cooldown = false;
            canMove = true;
        }

        void Update()
        {
            if (!MinigameManager.Instance.minigame.gameWin)
            {
                movement.y = Input.GetAxis("Vertical");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    canMove = false;
                    Snap();
                }
            }
        }

        void FixedUpdate()
        {
            if(!canMove)
            {
                return;
            }
            targetPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

            if(targetPos.y <= -1.7f )
            {
                targetPos = new Vector2(targetPos.x, -1.7f);
            }

            else if(targetPos.y >= 2.5f)
            {
                targetPos = new Vector2(targetPos.x, 2.5f);
            }

            rb.MovePosition(targetPos);
        }

        void Snap()
        {
            if(cooldown == false)
            {
                GameObject clone;
                clone = Instantiate(headPoint, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                Invoke("ResetCooldown", .6f);
                cooldown = true;
            }
        }

        void ResetCooldown()
        {
            cooldown = false;
            canMove = true;
        }
    }
}
