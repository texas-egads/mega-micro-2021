using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Stage5 : StageController
    {
        public static StageController instance;
        public RocketHit rocketHit;
        public float stageLength;
        public bool finalRound;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            instance = this;
            StartCoroutine(EndStage(stageLength));
        }
        private IEnumerator EndStage(float delay)
        {
            yield return new WaitForSeconds(stageLength);
            gameWon.Invoke();
            if (rocketHit != null) {
                rocketHit.shouldPlay = false;
            }
            if (finalRound)
            {
                BossGameManager.Instance.bossGame.gameWin = true;
                BossGameManager.Instance.bossGame.gameOver = true;
            }
        }
    }
}
