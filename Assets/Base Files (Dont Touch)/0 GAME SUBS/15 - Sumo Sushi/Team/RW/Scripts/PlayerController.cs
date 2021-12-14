using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using t15;

namespace t15
{
    public class PlayerController : MonoBehaviour
    {
        private bool isFacingLeft = true;
        private Rigidbody2D rb2d;

        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            GetComponent<BoxCollider2D>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && !isFacingLeft)
            {
                gameObject.transform.Rotate(0, 180, 0);
                isFacingLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.D) && isFacingLeft)
            {
                gameObject.transform.Rotate(0, 180, 0);
                isFacingLeft = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}