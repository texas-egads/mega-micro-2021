using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SecretPuddle
{
    public class Stage1 : StageController
    {
        public static Stage1 instance;
        public float stageLength;
        public UnityEvent gameLost;
        private AudioSource mainMusic;

        private bool stagePassed;

        private void Awake() 
        {
            if(gameLost == null)
            {
                gameLost = new UnityEvent();
            }
            instance = this;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            mainMusic = GameObject.Find("Main Music").GetComponent<AudioSource>();
            mainMusic.Play();

            BossGameManager.Instance.PlaySong("Song_1", 88, true, 1.5f, true);
 
            StartCoroutine(gameLifetime());
        }

        public override void LoseGame(){
            if (!stagePassed)
            {
                gameLost.Invoke();
                base.LoseGame();
            }
        }
        
        /// <summary>
        /// Wait for the stage length to pass before letting the player win
        /// the stage.
        /// </summary>
        private IEnumerator gameLifetime(){
            yield return new WaitForSeconds(stageLength);
            Debug.Log("Won stage 1");
            gameWon.Invoke();
            stagePassed = true;
        }
        private void FixedUpdate()
        {

        }
    }
}
