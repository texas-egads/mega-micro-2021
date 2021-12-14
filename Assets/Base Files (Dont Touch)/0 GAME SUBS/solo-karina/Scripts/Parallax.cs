using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace karina
{
    public class Parallax : MonoBehaviour
    {
        private float startposX;
        private float startposY;

        public GameObject hand;
        public float parallaxEffect;

        void Start()
        {
            startposX = transform.position.x;
            startposY = transform.position.y;
        }

        void Update()
        {
            float distX = (hand.transform.position.x * parallaxEffect);
            float distY = (hand.transform.position.y * parallaxEffect);

            transform.position = new Vector3((startposX - distX), (startposY - distY), transform.position.z);
        }
    }
}
