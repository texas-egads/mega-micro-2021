using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panic
{
    public class WallMovement : MonoBehaviour
    {
        public Rigidbody2D wallRb;
        public Rigidbody2D playerRb;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Ground")
            {
                wallRb.simulated = false;
                playerRb.simulated = false;
            }
        }
    }
}