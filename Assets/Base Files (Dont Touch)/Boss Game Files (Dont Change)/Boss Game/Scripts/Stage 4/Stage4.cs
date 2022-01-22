using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SecretPuddle
{
    public class Stage4 : StageController
    {
        public static Stage4 instance;
        public float stageLength;
        public float screenShakeIntensity = .022f;
        
        
        [HideInInspector]
        public UnityEvent gameLose;
        [HideInInspector]
        public UnityEvent stageOver;

        private void Awake() 
        {
            if(gameLose == null)    
            {
                gameLose = new UnityEvent();
            }

            if(stageOver == null)
            {
                stageOver = new UnityEvent();
            }

            instance = this;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            SoundAsset currSong = BossGameManager.Instance.getCurrSong();
            if (currSong == null || currSong.soundName != "Song_3")
            {
                BossGameManager.Instance.PlaySong("Song_3", 88, true, 1.5f, true);
            }
            
            StartCoroutine(EndStage(stageLength));
        }
        
        private IEnumerator EndStage(float delay)
        {
            Camera.main.GetComponent<CameraShake>().continousShake(delay, screenShakeIntensity);
            yield return new WaitForSeconds(1.5f);
            BossGameManager.Instance.PlaySong("Song_4", 88, true, delay - 1.5f, true);
            yield return new WaitForSeconds(delay - 1.5f);
            stageOver.Invoke();
        }
        
        public override void LoseGame()
        {
            gameLose.Invoke();
            StartCoroutine(failStage(.5f));
        }

        private IEnumerator failStage(float delay)
        {
            yield return new WaitForSeconds(delay);
            base.LoseGame();
        }
    }
}
