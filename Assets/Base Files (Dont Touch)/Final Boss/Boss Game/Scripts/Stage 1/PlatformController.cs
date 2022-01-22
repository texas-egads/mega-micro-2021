using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformController : MonoBehaviour
    {
        public float speed;
    
        private Rigidbody2D rb;
    
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
    
            rb.velocity = new Vector2(-speed, 0);
    
            Stage1.instance.gameLost.AddListener(stopMoving);
        }
    
        private void stopMoving()
        {
            rb.velocity = new Vector2(0, 0);
        }
    }    
}

