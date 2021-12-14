using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePoggersGamers {
    public class BeeScript : MonoBehaviour
    {
        //editor specified sprites that the bee will use
        [SerializeField] Sprite beeIdle;
        [SerializeField] Sprite beeLeftArm;
        [SerializeField] Sprite beeRightArm;

        //easter egg bee sprites
        [SerializeField] Sprite beeIdleAlt;
        [SerializeField] Sprite beeLeftArmAlt;
        [SerializeField] Sprite beeRightArmAlt;

        //private variables to keep track of bee's sprite renderer and while sprite the bee will switch to next
        private SpriteRenderer spriteRenderer;
        private Sprite beeNext;

        //on game start, sets the bee's initial sprite and what it will switch to next 
        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = beeIdle;
            beeNext = beeLeftArm;
        }

        //when the game manager detects that the spacebar is pressed
        //updates the bee's sprite and set's the next one it will switch to
        public void SwitchArms() {
            spriteRenderer.sprite = beeNext;
            beeNext = (beeNext == beeLeftArm) ? beeRightArm : beeLeftArm;
            //uses the pop sound to simulate the bee's eating noise
            MinigameManager.Instance.PlaySound("pop");
        }

        //if manager detects the easter egg has been triggered, switch all the bee's sprites
        //and correctly change the next sprite to it's alt to guard against bug
        public void EasterEgg() {
            beeIdle = beeIdleAlt;
            beeNext = (beeNext == beeLeftArm) ? beeRightArmAlt : beeLeftArmAlt;
            spriteRenderer.sprite = beeIdle;
            beeLeftArm = beeLeftArmAlt;
            beeRightArm = beeRightArmAlt;
        }
    }
}
