using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBuckets {
    public class Chunk : MonoBehaviour {
        void Start() {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddTorque(Random.Range(-90f,90f));
            rb.AddForce(Vector2.up * 200 + Vector2.right * Random.Range(-100f, 100f));
        }
    }
}
