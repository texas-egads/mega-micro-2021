using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TEAM_NAMESPACE
{
    public class ExampleBossScript : MonoBehaviour
    {
        [SerializeField] private Text gameText;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip gameMusic;
        [SerializeField] private AudioClip winMusic;
        [SerializeField] private AudioClip loseMusic;
        private int spacesToWin;
        private bool winnable;
        private void Start()
        {
            spacesToWin = 10;
            gameText.text = "Press space 10 times to win";
            PlayMusic(gameMusic, true);
            winnable = true;
        }


        private void Update()
        {
            if (Input.GetButtonDown("Space") && winnable)
            {
                if (spacesToWin > 1)
                {
                    spacesToWin--;
                    gameText.text = "Press space " + spacesToWin + " times to win";
                }
                else if(spacesToWin == 1)
                {
                    spacesToWin--;
                    StartCoroutine(WinSequence());
                }
            }

            if (Input.GetAxis("Vertical") > 0)
            {
                winnable = false;
                StartCoroutine(LoseSequence());
            }
        }

        private IEnumerator WinSequence()
        {
            BossGameManager.Instance.bossGame.gameWin = true; 
            gameText.text = "You Win!";
            PlayMusic(winMusic, false);
            yield return new WaitForSeconds(3);
            BossGameManager.Instance.bossGame.gameOver = true; // This triggers the game to go back to the main scene and check gameWin
        }

        private IEnumerator LoseSequence()
        {
            BossGameManager.Instance.bossGame.gameWin = false;
            gameText.text = "You Lose!";
            PlayMusic(loseMusic, false);
            yield return new WaitForSeconds(3);
            BossGameManager.Instance.bossGame.gameOver = true;
        }

        private void PlayMusic(AudioClip music, bool loop)
        {
            musicSource.clip = music;
            musicSource.loop = loop;
            musicSource.volume = .8f;
            musicSource.Play();
        }
        
    }
}