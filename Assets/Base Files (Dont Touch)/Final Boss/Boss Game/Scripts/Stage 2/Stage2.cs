using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SecretPuddle {
    public class Stage2 : StageController
    {
        public static Stage2 instance;
        public float stageLength; 

        [HideInInspector]
        public UnityEvent gameLost;

        void Awake() 
        {
            instance = this;
            if (gameLost == null)
            {
                gameLost = new UnityEvent();
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            BossGameManager bossMan = BossGameManager.Instance;
            SoundAsset currSong = bossMan.getCurrSong();
            if (currSong == null || currSong.soundName != "Song_2")
            {
                BossGameManager.Instance.PlaySong("Song_2", 88, true, 1.5f, true);
            }
        }

        public override void LoseGame()
        {
            gameLost.Invoke();
            base.LoseGame();
        }
    }
}
