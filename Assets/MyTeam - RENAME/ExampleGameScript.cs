using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TEAM_NAME_SPACE{
    public class ExampleGameScript : MonoBehaviour
    {
        // DELETE THIS FILE BEFORE YOU SUBMIT //
        public Text UIText;
        public string startText;
        public string winText;
        private MinigameManager minigameManager;
        private Minigame minigame;
        
        private void Start()
        {
            UIText.text = startText;
            
            minigameManager = FindObjectOfType<MinigameManager>();
            minigame = minigameManager.minigame;

            minigame.gameWin = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Space"))
            {
                if (!minigame.gameWin)
                {
                    minigame.gameWin = true;
                    UIText.text = winText;
                    minigameManager.PlaySound("win");
                }
            }
        }
    }
}
