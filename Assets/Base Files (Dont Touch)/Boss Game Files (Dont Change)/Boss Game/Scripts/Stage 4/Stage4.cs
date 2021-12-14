using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Stage4 : StageController
    {
        public static StageController instance;
        public KillZone killZone;
        public float stageLength;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            instance = this;
            StartCoroutine(EndStage(stageLength));
        }
        private IEnumerator EndStage(float delay)
        {
            yield return new WaitForSeconds(delay);
            if(killZone.killCount > 0)
            {
                LoseGame();
            }
            else
            {
                gameWon.Invoke();
                Destroy(killZone);
            }
        }
        private void Update()
        {
            if(killZone.killCount > 0)
            {
                StopAllCoroutines();
                LoseGame();
            }
        }
    }
}
