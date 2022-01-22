using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class LPFSpaceship : MonoBehaviour
    {
        float charge; // limit for how long the player can hold down space
        bool isCharging; // is the player charging right now?
        bool canControl; // can the player control the game right now?
        bool isRumbling;

        public Animator animator; // animator component for spaceship
        public GameObject HOLD; // UI telling the player to hold down spacebar
        public GameObject RELEASE; // UI telling the player to release spacebar
        public GameObject MiniMe; // Mini spaceship object that zooms off into the background
        public GameObject SmokeParticleObject; // Smoke particle; played when the player fails
        public GameObject Fire; // Fire object; turned on by default, turned off if the player fails
        public GameObject BLASTOFF; // Text saying "BLASTOFF!!" that is animated when the player succeeds
        public GameObject blastoffParticleObject; // A particle accompanying the BLASTOFF object
        public GameObject TooEarly; // Text saying "TOO EARLY..." that is animated when the player releases too soon
        public GameObject TooLate;// Text saying "TOO LATE..." that is animated when the player holds down too long



        void Start()
        {
            animator = GetComponent<Animator>(); //Get animator component from spaceship object

            charge = 1.5f; // limit for how long the player can hold down space

            //Setting all the bools and gameobjects to their proper default state
            isCharging = false;
            canControl = true;
            isRumbling = false;

            HOLD.SetActive(true);
            RELEASE.SetActive(false);
            MiniMe.SetActive(false);
            BLASTOFF.SetActive(false);
            blastoffParticleObject.SetActive(false);
            TooEarly.SetActive(false);
            TooLate.SetActive(false);
        }


        void Update()
        {
            if (canControl == true) // canControl determines whether or not the player can affect the game by holding the spacebar. True by default, turned false once they release spacebar
            {
                if (Input.GetKeyDown("space")) // if the spacebar is pressed, start charging
                {
                    isCharging = true;
                    animator.SetTrigger("startCharging");
                    isRumbling = true;
                    StartCoroutine(PlayRumbleSound());
                }
                if (Input.GetKeyUp("space") || charge <= 0) // if the spacebar is released OR if the player charges for too long, stop charging
                {
                    isCharging = false;
                    canControl = false;
                    isRumbling = false;
                    FinalCode();
                }
            }

            if (isCharging == true) { charge -= Time.deltaTime; } // if the player is charging (holding down space), the charge timer will deplete.

            if (charge < .75f && isCharging == true) // when the player has charged enough, turn off HOLD instruction and turn on RELEASE instruction
            {
                HOLD.SetActive(false);
                RELEASE.SetActive(true);
            }
        }

        private IEnumerator PlayRumbleSound()
        {
            while (isRumbling == true)
            {
                MinigameManager.Instance.PlaySound("rumble");
                yield return new WaitForSeconds(.5f);
                Debug.Log("this code is being run");
            }
        }

        void FinalCode()
        {
            HOLD.SetActive(false); // As soon as the player lets go of space, there's no reason to keep the control instructions on-screen.
            RELEASE.SetActive(false); // So, both of them will be set inactive.

            if (charge > 0f && charge < 0.75f) { WinGame(); } // If the player's charge is in the correct range when they stop charging, win the game
            else { LoseGame(); } // In any other case, the player loses the game
        }

        void WinGame()
        {
            MinigameManager.Instance.minigame.gameWin = true; // This flags the game as won

            MinigameManager.Instance.PlaySound("takeoff");

            animator.SetTrigger("Liftoff"); // Send a signal to the animator to play the animation of the rocket flying off into space

            MiniMe.SetActive(true); // When the rocket is off-screen, a smaller version is instantiated that zooms off into the distance and becomes a star

            BLASTOFF.SetActive(true); // Text with cool animation!
            blastoffParticleObject.SetActive(true); // Particle for the text with cool animation!
        }

        void LoseGame()
        {
            animator.SetTrigger("Fail"); // Send a signal to the animator to play the animation of the rocket jumping up a bit and then falling into oblivion

            SmokeParticleObject.SetActive(true); // A particle that is a puff of smoke that appears when the player loses; some "juice" for failure
            Fire.SetActive(false); // turn off the fire graphic that should only be visible if the rocket is being propelled into space

            if (charge <= 0) { TooLate.SetActive(true); } // if the player charged for too long, reveal the "Too Late" text
            if (charge > .75) { TooEarly.SetActive(true); } // if the player didn't charge enough, reveal the "Too Early" text

            // Final notes: Add text objects and particles for losing the game, similar to the BLASTOFF effect
        }
    }
}

