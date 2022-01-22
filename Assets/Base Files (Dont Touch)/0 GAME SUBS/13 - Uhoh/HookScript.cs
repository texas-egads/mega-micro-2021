using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uhoh
{
    public class HookScript : MonoBehaviour
    {
        public GameObject fishPrefab;
        
        private bool mIsCollidingFish;
        private GameObject mCollidingFishObject;

        public bool is_colliding_with_fish()
        {
            return mIsCollidingFish;
        }

        public GameObject get_colliding_game_object()
        {
            return mCollidingFishObject;
        }
        void Start()
        {
            mIsCollidingFish = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Object 1"))
            {
                mIsCollidingFish = true;
                mCollidingFishObject = other.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Object 1"))
            {
                mIsCollidingFish = false;
                mCollidingFishObject = null;
            }
        }
    }    
}
