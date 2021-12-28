using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LONEPINKFROG
{
    public class LPFCharacterController : MonoBehaviour
    {
        public float moveSpeed = 1f;
        Vector2 movement;
        public Rigidbody2D rigidBody;
        public CapsuleCollider2D characterCollider;
        
        bool canControl = true;

        bool isInvincible = false;
        float invincibilityTimer = .5f;
        float flashTimer = .05f;
        public Renderer[] renderers;

        public int playerHealth = 3;
        public GameObject[] healthUI;

        public GameObject POTASSIUM;

        public GameObject smokeParticleObject;
        ParticleSystem smokeParticle;
        ParticleSystem jetParticle;


        void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            characterCollider = gameObject.GetComponent<CapsuleCollider2D>();
            HandleHealth();
            smokeParticle = smokeParticleObject.GetComponent<ParticleSystem>();
            smokeParticle.Stop();
            jetParticle = gameObject.GetComponent<ParticleSystem>();
        }

        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal"); //Gets movement input based on WASD and arrow keys
            movement.y = Input.GetAxisRaw("Vertical");

            HandleTimers();
        }

        void HandleHealth() // activates and deactivates the player's health UI. Any healthUI with an index value greater than the player's current health gets deactivated.
        {
            for (int i = 0; i < healthUI.Length; i++) 
            {
                if (i < playerHealth){healthUI[i].SetActive(true); } 
                else {healthUI[i].SetActive(false);}
            }
        }

        void HandleTimers()
        {
            if (isInvincible == true)
            {
                //isInvincible is triggered when the player takes damage.
                flashTimer -= Time.deltaTime; //flashTimer is a countdown until the next time the character's renderer becomes invisible or invisible
                if (flashTimer <= 0) //once flashTimer hits 0, the renderer will flip on or off, and then the flashTimer will reset
                {
                    foreach (Renderer renderer in renderers) { renderer.enabled = !renderer.enabled; }
                    flashTimer = .05f;
                }
                invincibilityTimer -= Time.deltaTime; //invincibilityTimer is a countdown until the player becomes vulnerable again.
                if (invincibilityTimer <= 0) //once the InvincibilityTimer runs out, the player becomes vulnerable. This also makes sure the player is visible when vulnerable
                {
                    isInvincible = false;
                    invincibilityTimer = .5f;
                    foreach (Renderer renderer in renderers) { renderer.enabled = true; }
                }
            }
        }

        private void FixedUpdate()
        {
            // moves the character. canControl will disable the player's movement when the player runs out of health.
            if (canControl == true) { rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime); } 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.tag == "Object 1" && playerHealth > 0) 
            //Object 1 = BANANA. When the player collides with it, it spawns the flash POTASSIUM text, plays a cheering sound, and flags the game as won.
            {
                other.gameObject.SetActive(false);
                POTASSIUM.SetActive(true);
                MinigameManager.Instance.PlaySound("LPFCheer");                
                MinigameManager.Instance.minigame.gameWin = true;

            }
            if (other.tag == "Object 2" && isInvincible == false && MinigameManager.Instance.minigame.gameWin == false)
            //Object 2 = Asteroid. When the player collides with one, they lose 1 health, a "hit" sound is played, and the player becomes invincible.
            {
                other.gameObject.SetActive(false);
                playerHealth -= 1;
                HandleHealth();
                MinigameManager.Instance.PlaySound("LPFHit");
                isInvincible = true;

                //If the player's health is 1, the red/orange particle stops playing. 
                if (playerHealth == 1) { jetParticle.Stop(); }
 
                //If the player's health reaches 0, they rotate out of control and drift towards the bottom of the screen. This also plays a smoke particle.
                //The characterCollider is disabled to prevent the player from collecting the banana after they've already lost.
                if (playerHealth <= 0)
                {
                    rigidBody.constraints = RigidbodyConstraints2D.None;
                    rigidBody.AddTorque(300f);
                    rigidBody.gravityScale = .1f;
                    smokeParticle.Play();
                    characterCollider.enabled = false;
                    canControl = false;
                }
            }
        }
    }
}
