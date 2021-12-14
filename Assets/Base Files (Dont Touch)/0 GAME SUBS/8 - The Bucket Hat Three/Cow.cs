using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBuckets {
    public class Cow : MonoBehaviour {
        public float speed;
        public GameObject[] deathChunks;
        public ParticleSystem blood;

        Rigidbody2D rb;
        SpriteRenderer sp;

        public static Cow instance;

        private void Awake() {
            instance = this;
        }

        void Start() {
            MinigameManager.Instance.PlaySound("moo");
            rb = GetComponent<Rigidbody2D>();
            sp = GetComponent<SpriteRenderer>();
        }

        void Update() {
            rb.velocity = Vector2.right * Input.GetAxis("Horizontal") * speed;
        }

        public void Death() {
            sp.enabled = false;
            foreach(GameObject g in deathChunks) {
                g.SetActive(true);
            }
            blood.Play();
        }
    }
}