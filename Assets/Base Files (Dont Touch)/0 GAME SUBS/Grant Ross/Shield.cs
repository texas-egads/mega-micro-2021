using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grant
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Transform planet;
        [SerializeField] private float speed;
        

        private float lastSpeed = 0;
        private void Update()
        {
            if(planet != null){
                var move = Input.GetAxis("Horizontal");
                if (move > 0) move = lastSpeed > move ? 0 : 1;
                else if (move < 0) move = lastSpeed < move ? 0 : -1;
                transform.RotateAround(planet.position, Vector3.back, move*speed*Time.deltaTime);
                lastSpeed = Input.GetAxis("Horizontal");
            }
        }
    }
}
