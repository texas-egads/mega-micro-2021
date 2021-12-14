using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Stage3 : StageController
    {
        public static StageController instance;
        public float endDelay;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            instance = this;
        }
        public void DelayEnd(bool gameWin)
        {
            StartCoroutine(Delay(gameWin));
        }
        private IEnumerator Delay(bool gameWin)
        {
            yield return new WaitForSeconds(endDelay);
            if (!gameWin)
            {
                LoseGame();
            }
            else
            {
                gameWon.Invoke();
            }
        }
    }
}
