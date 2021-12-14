using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DogWithReindeerAntlers
{
    public class WinLose : MonoBehaviour
    {
        public BlenderBlade blenderBlade;
        public BlenderLid blenderLid;
        public Animator holderAnim;
        public Animator blenderAnim;
        public Animator lidAnim;

        public Animator UIAnim;
        public Animator backgroundAnim;

        public void Win()
        {
            blenderBlade.TurnOff();
            UIAnim.SetTrigger("win");
            holderAnim.SetTrigger("win");
            blenderAnim.SetTrigger("win");
            lidAnim.SetTrigger("win");
            backgroundAnim.SetTrigger("win");
            MinigameManager.Instance.minigame.gameWin = true;
            MinigameManager.Instance.PlaySound("win");
        }

        public void Lose()
        {
            blenderLid.Lose();
            lidAnim.SetTrigger("lose");
            UIAnim.SetTrigger("lose");
            backgroundAnim.SetTrigger("lose");
            MinigameManager.Instance.PlaySound("lose");
            //MinigameManager.Instance.minigame.gameWin = false;
        }
    }
}
