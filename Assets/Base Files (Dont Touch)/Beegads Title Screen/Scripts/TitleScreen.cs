using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terry
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private GameObject difficultyWindow;

        private void Start()
        {
            difficultyWindow.SetActive(false);
        }

        public void StartButton()
        {
            difficultyWindow.SetActive(true);
        }

        // Update is called once per frame
        public void ExitGame()
        {
            Application.Quit();
        }

        public void CreditsButton()
        {
            GameManager.Instance.LoadScene("Credits");
        }

        public void DifficultyButton(int rounds)
        {
            MainGameManager.Instance.SetRounds(rounds);
            GameManager.Instance.LoadScene("Intro");
        }
    }
}
