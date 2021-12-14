using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AveDisturbia{
    public class PlayerController : MonoBehaviour{
        //references
        public GameObject crowMan;
        public Animator crowAnim;
        public GameObject borgerMan;
        public Animator borgerAnim;
        public GameObject heart;
        public Animator heartAnim;


        //variables
        private Rigidbody2D crowRB;
        private Rigidbody2D borgerRB;
        private Rigidbody2D heartRB;
        private bool crowIsGrounded;
        private bool borgerIsGrounded;
        private bool heartIsGrounded;
        private bool gameOver;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector2 crowGroundCheckerSize;
        [SerializeField] private Vector2 borgerGroundCheckerSize;
        [SerializeField] private Vector2 heartGroundCheckerSize;
        [SerializeField] private float jumpVelocity;
        [SerializeField] private float fallForce;
        [SerializeField] private float bobFactor;

        void Awake(){
            crowRB = crowMan.GetComponent<Rigidbody2D>();
            borgerRB = borgerMan.GetComponent<Rigidbody2D>();
            heartRB = heart.GetComponent<Rigidbody2D>();
        }

        void Update(){
            if(!gameOver){
                groundChecks();    
                playerJumps();
                falling();    
            }
        }

        /// <summary>
        /// Check to see if all players are grounded
        /// </summary>
        private void groundChecks(){
            crowIsGrounded = Physics2D.OverlapBox(crowMan.transform.position,
                    crowGroundCheckerSize, 0f, groundLayer);
            borgerIsGrounded = Physics2D.OverlapBox(borgerMan.transform.position,
                    borgerGroundCheckerSize, 0f, groundLayer);
            heartIsGrounded = Physics2D.OverlapBox(heart.transform.position,
                    heartGroundCheckerSize, 0f, groundLayer);
        }

        /// <summary>
        /// Make characters jump with A, S, D if they are grounded
        /// </summary>
        private void playerJumps(){ 
            if(crowIsGrounded && Input.GetKey(KeyCode.A)){
                crowRB.velocity = new Vector2(0f, jumpVelocity);
            }

            if(borgerIsGrounded && Input.GetKey(KeyCode.S)){
                borgerRB.velocity = new Vector2(0f, jumpVelocity);
            }

            if(heartIsGrounded && Input.GetKey(KeyCode.D)){
                heartRB.velocity = new Vector2(0f, jumpVelocity);
            }
        }

        /// <summary>
        /// Make characters fall faster
        /// </summary>
        private void falling(){
            //if any character has a negative y velocity then add a force downwards
            if(crowRB.velocity.y < 0){
                crowRB.AddForce(new Vector2(0f, -fallForce));
            }

            if(borgerRB.velocity.y < 0){
                borgerRB.AddForce(new Vector2(0f, -fallForce));
            }

            if(heartRB.velocity.y < 0){
                heartRB.AddForce(new Vector2(0f, -fallForce));
            }
        }

        /// <summary>
        /// Stops all inputs and movement of characters
        /// </summary>
        public void stopEverything(){
            //end game so that no inputs are read
            gameOver = true;

            //stop all movement
            crowRB.velocity = new Vector2(0f, 0f);
            borgerRB.velocity = new Vector2(0f, 0f);
            heartRB.velocity = new Vector2(0f, 0f);

            //send all characters flying
            sendCharactersFlying();

            //stop all animations
            crowAnim.speed = 0f;
            borgerAnim.speed = 0f;
            heartAnim.speed = 0f;
        }

        /// <summary>
        /// Send all characters flying in random directions
        /// </summary>
        private void sendCharactersFlying(){
            crowRB.constraints = RigidbodyConstraints2D.None;
            borgerRB.constraints = RigidbodyConstraints2D.None;
            heartRB.constraints = RigidbodyConstraints2D.None;
            crowRB.AddForce(randomForce());
            borgerRB.AddForce(randomForce());
            heartRB.AddForce(randomForce());
        }

        /// <summary>
        /// Returns a random upwards force in a random x direction
        /// </summary>
        /// <returns>
        /// A Vector2 with a random upwards force in a random x direction
        /// </returns>
        private Vector2 randomForce(){
            float x = Random.Range(-500,500);
            float y = Random.Range(400f, 800f);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Draw the ground check boxes
        /// </summary>
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(crowMan.transform.position, crowGroundCheckerSize);
            Gizmos.DrawWireCube(borgerMan.transform.position, borgerGroundCheckerSize);
            Gizmos.DrawWireCube(heart.transform.position, heartGroundCheckerSize);
        }
    }
}
