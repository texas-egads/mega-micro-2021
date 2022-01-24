using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class ChildCollider : MonoBehaviour
    {
        [SerializeField] private GroundEnemy _parentEnemy;
        private void Start() {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Debug.Log("child collider collided with " + other.name);
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("child collider player enter");
                _parentEnemy._isTouchingPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("child collider player exit");
                _parentEnemy._isTouchingPlayer = false;
            }
        }
    }
}
