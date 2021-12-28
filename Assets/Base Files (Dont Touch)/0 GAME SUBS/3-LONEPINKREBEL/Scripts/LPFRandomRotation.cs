using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LONEPINKFROG
{
    public class LPFRandomRotation : MonoBehaviour
    {

        //This script is attached to each asteroid graphic and causes it to rotate in a random direction at a random rate.
        //The asteroids look kinda weird when they're all oriented the same way and don't move

        public Rigidbody2D rigidBody;
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.AddTorque(Random.Range(-100, 100));
        }
    }
}
