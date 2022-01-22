using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class BossController : MonoBehaviour
    {
        public Transform player;
        public float speed;
        public GameObject attackingRectangle;
        public float attackingDistance;
        
        [HideInInspector]
        public Vector3 initialPosition;
        [HideInInspector]
        public bool isStageOver = false;
        void Awake() 
        {
            initialPosition = transform.position;
        }

        void Start() 
        {
            Stage4.instance.stageOver.AddListener(stageOver);
        }

        public void stageOver()
        {
            isStageOver = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().Play("Following");
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.tag == "Player")
            {
                // kill player on contact
                Stage4.instance.LoseGame();
            } 
            else 
            {
                // destory asteroids in boss' path
                AsteroidController asteroidCont = other.gameObject.GetComponent<AsteroidController>();
                if (asteroidCont != null)
                {
                    asteroidCont.health = 0;
                }
            } 
        }
    }
}

