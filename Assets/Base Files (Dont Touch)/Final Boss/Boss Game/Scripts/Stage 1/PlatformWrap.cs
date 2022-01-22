using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SecretPuddle
{
    public class PlatformWrap : MonoBehaviour
    {
        public Transform moveLocation;
        public int layer;
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.layer == layer)
            {
                Vector3 newPos = other.gameObject.transform.position;
                newPos.x = moveLocation.position.x;

                other.gameObject.transform.position = newPos;
            } 
        }
    }
}
