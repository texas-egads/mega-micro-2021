using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class SideToSideMovement : MonoBehaviour
    {
        private float horizontal;
        public float moveSpeed;
        public Rigidbody2D rb;
        public float dampenFactor = 1.2f;

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                horizontal = Input.GetAxis("Horizontal");
                var newVel = rb.velocity;
                newVel.x = horizontal * moveSpeed;
                rb.velocity = newVel;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                horizontal = Input.GetAxis("Horizontal");
                var newVel = rb.velocity;
                newVel.x = horizontal * moveSpeed;
                rb.velocity = newVel;
            }

            if (Mathf.Abs((Input.GetAxisRaw("Horizontal"))) == 0 || !Input.anyKey)
            {
                var dampVel = rb.velocity / dampenFactor;
                rb.velocity = dampVel;
            }
        }
    }
}
