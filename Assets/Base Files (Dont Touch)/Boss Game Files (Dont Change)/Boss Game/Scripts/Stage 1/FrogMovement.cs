using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SecretPuddle
{
    public class FrogMovement : MonoBehaviour
    {
        private int currentPlatform = 1;
        public float platformSpacing;
        private Vector3 finalPos;
        
        private bool isMoving;
        private bool canMove = true;

        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            finalPos = transform.position;
            anim = GetComponent<Animator>();
            Stage1.instance.gameLost.AddListener(stopMoving);
        }

        private void stopMoving()
        {
            canMove = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                if (Input.GetKey(KeyCode.W) && currentPlatform < 2 && !isMoving)
                {
                    currentPlatform++;
                    transform.position = finalPos;
                    StartCoroutine(SwitchPlatforms(Vector3.up, Ease.OutBack));
                }
                else if (Input.GetKey(KeyCode.S) && currentPlatform > 0 && !isMoving)
                {
                    currentPlatform--;
                    transform.position = finalPos;
                    StartCoroutine(SwitchPlatforms(Vector3.down, Ease.InQuad));
                }
            }
            
        }

        private IEnumerator SwitchPlatforms(Vector3 dir, Ease ease)
        {
            BossGameManager.Instance.PlaySound("Jump");
            isMoving = true;
            anim.SetBool("Jumping", true);
            float duration = .4f;
            finalPos = transform.position + dir * platformSpacing;
            transform.DOMoveY(transform.position.y + dir.y * platformSpacing, duration).SetEase(ease);
            yield return new WaitForSeconds(duration - .04f);
            isMoving = false;
            anim.SetBool("Jumping", false);
        }
    }
}
