using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AveDisturbia{
    public class Scrolling : MonoBehaviour{

        //variables
        private List<Rigidbody2D> objects;
        private bool canScroll;
        [SerializeField] private float moveVelocity;

        void Awake(){
            canScroll = true;
            objects = new List<Rigidbody2D>();
            foreach(Transform child in transform){
                objects.Add(child.gameObject.GetComponent<Rigidbody2D>());
            }
        }

        void Update(){
            if(canScroll){
                foreach(Rigidbody2D rb in objects){
                    rb.velocity = new Vector2(-moveVelocity, rb.velocity.y);
                }    
            }else{
               foreach(Rigidbody2D rb in objects){
                    rb.velocity = new Vector2(0, 0);
                }   
            }   
        }

        public void StopScrolling(){
            canScroll = false;
        }
    }
}
