using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D rb;

        void Start() 
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -speed);
        }
    }
}

