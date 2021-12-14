using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice {
    public class Ingredient : MonoBehaviour
    {
        public Vector2 fallSpeed;
        public bool dangerous;
        public Rigidbody2D rb;
        public SpriteRenderer sprtRnd;
        public Sprite[] sprites;
        public float maxRotation;
        public SpriteRenderer outline;
        // Start is called before the first frame update
        void Start()
        {
            sprtRnd.sprite = sprites[Random.Range(0, sprites.Length)];
            rb.velocity = fallSpeed;
            rb.angularVelocity = Random.Range(-maxRotation, maxRotation);
            outline.sprite = sprtRnd.sprite;
        }

    }
}
