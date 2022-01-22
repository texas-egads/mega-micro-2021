using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SecretPuddle
{
    public class MazeMovement : MonoBehaviour
    {
        public MazeRoom currentRoom;
        private bool isMoving;
        
        public float moveDuration;
        public float moveLength;
        public GameObject bombObj;

        private int placedBombs;
        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<MazeRoom>())
            {
                print("Room entered");
                currentRoom = other.gameObject.GetComponent<MazeRoom>();
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.GetComponent<MazeRoom>())
            {
                print("Room entered");
                currentRoom = other.gameObject.GetComponent<MazeRoom>();
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isMoving)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(Move(new Vector2(0, 1)));
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    StartCoroutine(Move(new Vector2(0, -1)));
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    StartCoroutine(Move(new Vector2(-1, 0)));
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine(Move(new Vector2(1, 0)));
                }
                else if (Input.GetKeyDown(KeyCode.Space) && currentRoom.roomType == MazeRoom.RoomType.Bomb && !currentRoom.hasBomb)
                {
                    var spawnedBomb = Instantiate(bombObj, currentRoom.transform);
                    currentRoom.hasBomb = true;
                    placedBombs++;
                    if (placedBombs >= 2)
                    {
                        Stage3.instance.gameWon.Invoke();
                    }
                }
            }
        }

        private IEnumerator Move(Vector2 moveDir)
        {
            transform.up = moveDir;
            if (currentRoom.CanMove(moveDir))
            {
                isMoving = true;
                transform.DOLocalMove(transform.localPosition + (Vector3) moveDir * moveLength, moveDuration);
                yield return new WaitForSeconds(moveDuration);
                isMoving = false;
            }
            else
            {
                isMoving = true;
                transform.DOLocalMove(transform.localPosition + (Vector3) moveDir * moveLength / 6, moveDuration / 2).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(moveDuration / 2);
                transform.DOLocalMove(transform.localPosition - (Vector3) moveDir * moveLength / 6, moveDuration / 2);
                yield return new WaitForSeconds(moveDuration / 2);
                isMoving = false;
            }
        }
    }
}