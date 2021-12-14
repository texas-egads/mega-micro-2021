using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Timer : MonoBehaviour
    {
        public Animator needleAnim;
        public GameObject cake;
        public GameObject fire;
        private float animLength;
        public Sprite[] cakeSprites;
        private float timeElapsed;

        private bool gameOver;
        // Start is called before the first frame update
        void Start()
        {
            BossGameManager.Instance.PlaySound("timer");
            animLength = needleAnim.GetCurrentAnimatorStateInfo(0).length;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetAxisRaw("Space") > 0 && !gameOver)
            {
                
                TakeOutCake();
            }
        }

        private void FixedUpdate()
        {
            if (!gameOver)
            {
                timeElapsed += Time.deltaTime;
                if(timeElapsed >= 12 * animLength / 13)
                {
                    fire.SetActive(true);
                }
                if(timeElapsed >= animLength)
                {
                    TakeOutCake();
                }
            }
        }

        private void TakeOutCake()
        {
            BossGameManager.Instance.PlaySound("ready");
            gameOver = true;
            Destroy(needleAnim);
            SpriteRenderer sprtrend = cake.GetComponent<SpriteRenderer>();
            if(timeElapsed >= 0 && timeElapsed < 11 * animLength / 13 - .2f)
            {
                sprtrend.sprite = cakeSprites[0];
                ((Stage3)Stage3.instance).DelayEnd(false);
            }
            else if (timeElapsed >= 11 * animLength / 13 - .2f && timeElapsed < 12 * animLength / 13)
            {
                sprtrend.sprite = cakeSprites[1];
                ((Stage3)Stage3.instance).DelayEnd(true);
            }
            else if(timeElapsed >= 12 * animLength / 13)
            {
                sprtrend.sprite = cakeSprites[2];
                ((Stage3)Stage3.instance).DelayEnd(false);
            }
            sprtrend.color = Color.white;
            cake.GetComponent<Animator>().SetBool("isDone", true);
        }
    }
}
