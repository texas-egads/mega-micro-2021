using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class KillZone : MonoBehaviour
    {
        public int enemyLayer;
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer == enemyLayer)
                Destroy(other.gameObject);
        }
    }
}
