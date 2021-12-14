using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBuckets {
    public class Steven : MonoBehaviour {
        public GameObject[] children;

        void Start() {
            for(int x = 0;x<children.Length;x++) {
                GameObject favoriteChild = children[x];
                int newFavorite = Random.Range(0, children.Length);
                children[x] = children[newFavorite];
                children[newFavorite] = favoriteChild;
            }
            for(int y = 0;y<children.Length;y++) {
                children[y].transform.position = Vector2.up * (y + .5f);
            }
        }
    }
}