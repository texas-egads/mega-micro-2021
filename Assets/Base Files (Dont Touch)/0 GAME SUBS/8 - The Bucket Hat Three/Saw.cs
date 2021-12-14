using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBuckets {
    public class Saw : MonoBehaviour {
        Rigidbody2D rb;
        public float gravity;
        public GameObject spinner;
        float speed = 0;

        void Start() {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            StartCoroutine(SawRevving());
            StartCoroutine(Fall());
        }

        void Update() {
            spinner.transform.Rotate(Vector3.forward * speed);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.name.Equals("Cow"))
                BBuckets.instance.Lose();
        }

        IEnumerator SawRevving() {
            float timer = 0;
            yield return new WaitForSeconds(.5f);
            while(speed<=110) {
                timer += Time.deltaTime;
                speed = 27.5f * (timer * timer);
                yield return new WaitForEndOfFrame();
            }
            speed = 110;
        }

        IEnumerator Fall() {
            yield return new WaitForSeconds(2f);
            if(transform.position.y == .5)
                rb.gravityScale = 2 * gravity;
            yield return new WaitForSeconds(.8f);
            if(transform.position.y == 1.5)
                rb.gravityScale = 2 * gravity;
            yield return new WaitForSeconds(.7f);
            if(transform.position.y == 2.5)
                rb.gravityScale = 2*gravity;
            yield return new WaitForSeconds(.6f);
            if(transform.position.y == 3.5)
                rb.gravityScale = 2 * gravity;
        }
    }
}
