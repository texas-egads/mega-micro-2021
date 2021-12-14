using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XIRO
{
    public class FoodGrabPoint : MonoBehaviour
    {
        public bool canGetFood;
        public Food foodToGrab;

        // Start is called before the first frame update
        void Start()
        {
            canGetFood = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            canGetFood = true;
            foodToGrab = collision.gameObject.GetComponent<FoodOption>().food;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            canGetFood = false;
        }
    }
}
