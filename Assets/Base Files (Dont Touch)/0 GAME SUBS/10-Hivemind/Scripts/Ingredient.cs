using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hivemind{
    public class Ingredient : MonoBehaviour
    {
        public Ingredient_ID id;
        public float speed;

        void Update()
        {
            //constantly move down until you're off the track
            Vector2 p = transform.position;
            p.y -= speed * Time.deltaTime;
            transform.position = p;
            if(transform.position.y < -2.5f){
                Destroy(gameObject);
            }
        }
    }
}
