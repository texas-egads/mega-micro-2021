using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class LPFRandomizeSpawn : MonoBehaviour
    {

        //This script is attached to each asteroid, as well as the banana. It causes each object to spawn at a random Y value.

        public Transform currentPosition;
        void Start()
        {
            currentPosition = gameObject.transform;
            gameObject.transform.position = new Vector2(currentPosition.position.x, Random.Range(-3, 5));
        }
    }
}
