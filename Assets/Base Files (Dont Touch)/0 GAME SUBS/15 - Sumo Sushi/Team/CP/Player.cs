using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t15
{
    public class Player : MonoBehaviour
    {
        private bool isEating = false;

        SpriteRenderer sr;
        // Start is called before the first frame update
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                print("A key was pressed");
                sr.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                print("d key was pressed");
                sr.flipX = false;
            }
        }

        void FixedUpdate()
        {
            if (isEating)
            {
                // play animation for eat
                // WAIT (?) until finish
                isEating = false;
            }

            // scrap space bar... A/D for swipe.
            if (!isEating && Input.GetKeyDown(KeyCode.Space))
            {
                print("CHOMP"); // eat
                isEating = true;
            }
        }
    }
}
