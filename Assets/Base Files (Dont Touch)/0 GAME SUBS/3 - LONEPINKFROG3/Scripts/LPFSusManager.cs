using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class LPFSusManager : MonoBehaviour
    {
        public GameObject[] Frogs; // frogs!! 0 = blue, 1 = green, 2 = red
        private int susFrogIndex; // int that determines which frog is sus

        public GameObject susFrogSelector; // selector/cursor image
        private int selectorIndex; // int that determines which frog is selected/where the cursor should be
        public Transform[] selectorPosition; // the positions the selector can be at. 0 = blue, 1 = green, 2 = red

        public bool gameOver = false; // turned true when the spacebar is pressed. Once turned true, prevents player from pressing any buttons.

        public Animator[] animators; // animator components

        public GameObject ejectedFrogHolder; // a game object that contains sprites for each frog that can be ejected (blue/green/red normal, blue/green/red imposter)
        public GameObject[] ejectedFrogs; // each individual image located within the above game object

        public GameObject victoryText; // text that reads "AS SUSPECTED!" to signal victory

        public GameObject[] instructions; // gameobjects that teach the player how to play

        // Start is called before the first frame update
        void Start()
        {
            // SPAWN THE THREE FROGS //
            susFrogIndex = Random.Range(0, 3); // choose either frog 0, 1, or 2 to be the imposter
            LPFSusFrog susFrogScript = Frogs[susFrogIndex].GetComponent<LPFSusFrog>(); // locate the script on the imposter frog
            susFrogScript.isSus = true; // set the frog to be sus
            foreach (GameObject Frog in Frogs) { LPFSusFrog susFrogScripts = Frog.GetComponent<LPFSusFrog>(); susFrogScripts.SpawnFrog(); } // Spawn the three frogs

            // SET THE CURSOR LOCATION //
            selectorIndex = 0; // set the selector to position/frog 0 (blue frog)
            susFrogSelector.transform.position = selectorPosition[selectorIndex].position; // move the selector to the proper position

            // SET GAMEOBJECTS TO PROPER STARTING STATE //
            ejectedFrogHolder.SetActive(false);
            victoryText.SetActive(false);

        }

        void Update()
        {
            if (gameOver == false)
            {
                if (Input.GetKeyDown(KeyCode.D)) // If D key is pressed, move cursor to the right 
                {
                    selectorIndex++;
                    if (selectorIndex > 2) { selectorIndex = 0; } // value rolls back over to the leftmost frog if the value becomes too high
                    susFrogSelector.transform.position = selectorPosition[selectorIndex].position;
                }

                if (Input.GetKeyDown(KeyCode.A)) // If A key is pressed, move cursor to the left
                {
                    selectorIndex--;
                    if (selectorIndex < 0) { selectorIndex = 2; } // value rolls back to the rightmost frog if the value becomes too low
                    susFrogSelector.transform.position = selectorPosition[selectorIndex].position;
                }

                if (Input.GetKeyDown("space")) // if Space is pressed, select the highlighted frog and end the game
                {
                    foreach (GameObject UI in instructions) { UI.SetActive(false); }
                    gameOver = true; // remove control from the player
                    susFrogSelector.SetActive(false); // remove the selector
                    if (selectorIndex == susFrogIndex) { MinigameManager.Instance.minigame.gameWin = true; victoryText.SetActive(true); } // if the frog value and the selector value are the same, then the game is won
                    foreach (Animator animator in animators) { animator.SetTrigger("End"); } // play animation for ending the game


                    ejectedFrogHolder.SetActive(true); // enable the ejected frog holder
                    if (MinigameManager.Instance.minigame.gameWin == true) { ejectedFrogs[selectorIndex].SetActive(true); } // if the player won the game, eject the correct sus frog
                    else { ejectedFrogs[selectorIndex + 3].SetActive(true); } // otherwise, eject the corresponding normal frog

                }
            }
        }

    }
}

