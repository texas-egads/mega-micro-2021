using UnityEngine;

namespace ToadallyAwesome {
    public class Asteroid : MonoBehaviour {
        //Asteroid Launcher handles the physics and timing of spawning asteroids
        //so each individual asteroid just needs to check for collision
        void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.tag == "Player") {
                MinigameManager.Instance.PlaySound("Explosion");
                MinigameManager.Instance.minigame.gameWin = false;
            }
            else if(other.gameObject.tag == "Object 1") {
                Destroy(gameObject);
            }    
        }
    }
}
