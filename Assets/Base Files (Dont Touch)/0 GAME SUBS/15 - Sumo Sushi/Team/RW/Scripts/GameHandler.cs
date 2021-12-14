
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace t15
{
    public class GameHandler : MonoBehaviour
    {
        public Text scoreText;
        public Text winText;

        private int score = 0;

        // Start is called before the first frame update
        void Start()
        {
            MinigameManager.Instance.PlaySound("tsuzumi");
            MinigameManager.Instance.minigame.gameWin = false;
            winText.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (score >= 3)
            {
                if (!MinigameManager.Instance.minigame.gameWin)
                {
                    MinigameManager.Instance.minigame.gameWin = true;
                    winText.enabled = true;
                }
            }
            scoreText.text = "Plates: " + score;
        }

        public void incScore()
        {
            score++;
        }

        public void decScore()
        {
            if(score>0 && score<3)
                score--;
        }

        public void eatSound()
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    MinigameManager.Instance.PlaySound("slurp");
                    break;
                case 1:
                    MinigameManager.Instance.PlaySound("lowChomp");
                    break;
                case 2:
                    MinigameManager.Instance.PlaySound("highChomp");
                    break;
                case 3:
                    MinigameManager.Instance.PlaySound("Mmm");
                    break;
            }
        }

        public void boomSound()
        {
            MinigameManager.Instance.PlaySound("boom");
        }

        public void swing1Sound()
        {
            MinigameManager.Instance.PlaySound("swing1");
        }

        public void swing2Sound()
        {
            MinigameManager.Instance.PlaySound("swing1");
        }
    }
}
