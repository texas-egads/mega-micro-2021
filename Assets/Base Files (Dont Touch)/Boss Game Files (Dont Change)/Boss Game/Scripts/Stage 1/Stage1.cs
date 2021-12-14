using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeNice
{
    public class Stage1 : StageController
    {
        public static StageController instance;
        public Spawner spawner;
        public float stageLength;
        public Slider timer;
        private AudioSource mainMusic;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            instance = this;
            gameWon.AddListener(WipeScene);
            mainMusic = GameObject.Find("Main Music").GetComponent<AudioSource>();
            mainMusic.Play();
        }

        private void WipeScene()
        {
            //spawner.StopSpawning();
        }
        private void FixedUpdate()
        {
            timer.value -= Time.deltaTime;
            if(timer.value <= Time.deltaTime)
            {
                LoseGame();
            }
        }
    }
}
