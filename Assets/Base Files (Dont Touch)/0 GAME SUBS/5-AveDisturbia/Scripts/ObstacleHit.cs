using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AveDisturbia{
    public class ObstacleHit : MonoBehaviour{
        public ParticleSystem collisionParticles;

        private void Awake() {
            //player wins as long as obstacle isn't hit 
            MinigameManager.Instance.minigame.gameWin = true;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.tag == "Player"){
                //stop scrolling
                Scrolling scroll = FindObjectOfType<Scrolling>()
                        .GetComponent<Scrolling>();
                scroll.StopScrolling();

                //stop all player control and movement
                PlayerController controller = 
                        FindObjectOfType<PlayerController>()
                        .GetComponent<PlayerController>();
                controller.stopEverything();

                //set game to loss
                MinigameManager.Instance.minigame.gameWin = false;

                //play sound
                MinigameManager.Instance.PlaySound("CrashBoom");

                //particles!
                collisionParticles.Play();
            }
        }
    }
}
