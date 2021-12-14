using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePoggersGamers {
    public class HoneyScript : MonoBehaviour
    {
        //editor specified sprites that represent the different amounts of honey
        //number of phases is also set in the editor for best generalizability and reusability
        [SerializeField] Sprite[] honeyPhases;

        //easter egg array of alt sprites
        [SerializeField] Sprite[] honeyPhasesAlt;

        //multiple of spaces needed to change honey sprite
        [SerializeField] int spacesForPhaseChange;

        private SpriteRenderer spriteRenderer;
        private int index;
        
        //starts honey at the initial sprite when game starts
        void Start() {   
            index = 0;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = honeyPhases[index];
        }

        //for each spacebar press, check if we have to update the honey's phase and do so
        //if we keep pressing spacebar after we've won the game, return true so we don't lose the game
        public bool ProcessAction(int numSpacesPressed) {
            //guard against pressing space after the game has been won unwanted behavior
            if(!MinigameManager.Instance.minigame.gameWin) {
                //uses preincrement for the index so we can do this all in one line with ternary operator
                spriteRenderer.sprite = (numSpacesPressed % spacesForPhaseChange == 0) ? honeyPhases[++index] : spriteRenderer.sprite;
                return (index == honeyPhases.Length - 1);
            }
            //pressing space after game has been won keeps the game in the win state
            return true;
        }

        //switches the array of honey sprites to the easter egg one
        public void EasterEgg() {
            honeyPhases = honeyPhasesAlt;
            spriteRenderer.sprite = honeyPhases[index];
        }
    }    
}
