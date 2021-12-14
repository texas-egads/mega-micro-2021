using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalBass
{
    public class MiniManager : MonoBehaviour
    {

        TailMovement tailMovement;
        GameObject twinkle;
        GameObject head;

        public Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            tailMovement = GameObject.Find("Fish").GetComponent<TailMovement>();
            twinkle = GameObject.Find("Twinkle");
            twinkle.GetComponent<SpriteRenderer>().enabled = false;
            head = GameObject.Find("PlayerHead");
        }

        // Update is called once per frame
        void Update()
        {
            if(tailMovement.isEat)
            {
                if (MinigameManager.Instance.minigame.gameWin == false)
                {
                    MinigameManager.Instance.minigame.gameWin = true;
                    animator.SetBool("isWin", true);
                    MinigameManager.Instance.PlaySound("VictorySound");
                    head.GetComponent<SpriteRenderer>().enabled = true;
                    twinkle.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
}
