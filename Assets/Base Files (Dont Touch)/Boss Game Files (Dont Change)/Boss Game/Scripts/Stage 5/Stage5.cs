using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class Stage5 : StageController
    {
        public static StageController instance;
        public float stageLength;
        public bool finalRound;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            instance = this;
            StartCoroutine(EndStage(stageLength));
            if (!finalRound)
            {
                BossGameManager bossMan = BossGameManager.Instance;
                SoundAsset currSong = bossMan.getCurrSong();
                BossGameManager.Instance.PlaySound("Song_Open");
            }
        }
        private IEnumerator EndStage(float delay)
        {
            yield return new WaitForSeconds(stageLength - 1.5f);
            if (finalRound)
            {
                BossGameManager.Instance.PlaySound("victory");
            }
            yield return new WaitForSeconds(1.5f);
            gameWon.Invoke();

            if (finalRound)
            {
                BossGameManager.Instance.bossGame.gameWin = true;
                BossGameManager.Instance.bossGame.gameOver = true;
            }
        }
    }
}
