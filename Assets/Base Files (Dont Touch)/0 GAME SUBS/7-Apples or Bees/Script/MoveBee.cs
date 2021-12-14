using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Random = UnityEngine.Random;

namespace ApplesOrBees
{
    public class MoveBee : MonoBehaviour
    {
        public Vector2 target;
        public float speed;
        private Vector2 position;
        GameObject attackBeeObject;

        // Start called before first frame update
        void Start()
        {
            target = new Vector2(3, 3);
            position = gameObject.transform.position;
            speed = 3.0f;
        }

        //Update called once per frame
        void Update()
        {
            if (transform.position.y == 3)
            {
                target = new Vector2(3, -3);
            } else if (transform.position.y == -3)
            {
                target = new Vector2(3, 3);
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            
            if (Input.GetKey("space"))
            {
                speed = 0.0f;
                gameObject.active = false;
            }
        }

    }
}
