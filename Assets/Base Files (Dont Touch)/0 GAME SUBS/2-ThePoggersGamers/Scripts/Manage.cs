using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePoggersGamers {
    public class Manage : MonoBehaviour
    {
        //objects and their associated scripts that this manager needs
        [SerializeField] GameObject bee;
        private BeeScript beeScript;

        [SerializeField] GameObject honey;
        private HoneyScript honeyScript;

        [SerializeField] GameObject background;
        private MiscScript backgroundScript;

        [SerializeField] GameObject table;
        private MiscScript tableScript;

        [SerializeField] GameObject pikachu;
        private MiscScript pikaScript;

        [SerializeField] GameObject spacebar;
        private MiscScript spacebarScript;

        //makes sure we only activate the easter egg once
        private bool easterEgg;

        private int numSpacesPressed; 
        private bool playedWinSound;

        //getting the relevant scripts from the objects so we can access them
        void Start() {
            beeScript = bee.GetComponent<BeeScript>();
            honeyScript = honey.GetComponent<HoneyScript>();
            backgroundScript = background.GetComponent<MiscScript>();
            tableScript = table.GetComponent<MiscScript>();
            pikaScript = pikachu.GetComponent<MiscScript>();
            spacebarScript = spacebar.GetComponent<MiscScript>();
            numSpacesPressed = 0;
            playedWinSound = false;
        }

        //each frame checks for spacebar press and/or easter egg activation
        void Update() {
            checkEasterEgg();
            if(Input.GetKeyDown(KeyCode.Space)) {
                ++numSpacesPressed;
                beeScript.SwitchArms();
                //honey does what it needs to on spacebar press
                //method returns true if we finished the bottle (game win) or false if not (game still losing)
                bool win = honeyScript.ProcessAction(numSpacesPressed);
                //ensures that we only play the win sound once, upon first triggering the win
                if(win && !playedWinSound) {
                    MinigameManager.Instance.PlaySound("win");
                    playedWinSound = true;
                }
                //tracks whether we have won or lost the game
                MinigameManager.Instance.minigame.gameWin = win;
            }
        }

        //OOOOOOOO SUPER SECRET EASTER EGG!!!!
        //checks for the triggering of the easter egg and handles components directly in their respective scripts
        void checkEasterEgg() {
            if(!easterEgg && Input.GetKeyDown(KeyCode.B)) {
                easterEgg = true;
                beeScript.EasterEgg();
                honeyScript.EasterEgg();
                backgroundScript.EasterEgg();
                tableScript.EasterEgg();
                pikaScript.EasterEgg();
                spacebarScript.EasterEgg();
            }
        }
    }
}
