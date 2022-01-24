using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColliderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger enter");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("collision enter");
    }

    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("trigger stay");
    }

    private void OnCollisionStay2D(Collision2D other) {
        
    }
}
