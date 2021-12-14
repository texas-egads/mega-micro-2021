using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t15
{
    public class plate : MonoBehaviour
    {
        private bool isBad;
        private int index;
        public Sprite[] foodItem;

        // Start is called before the first frame update
        void Start()
        {
            index = Random.Range(0, foodItem.Length);
            if (index == 0) {
                GetComponent<Animator>().enabled = true; // Bomb animation
            }
            GetComponent<SpriteRenderer>().sprite = foodItem[index];
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (index == 0)
            {
                GameObject.Find("GameHandler").GetComponent<GameHandler>().decScore();
                GameObject.Find("GameHandler").GetComponent<GameHandler>().boomSound();
            }
            else
            {
                GameObject.Find("GameHandler").GetComponent<GameHandler>().incScore();
                GameObject.Find("GameHandler").GetComponent<GameHandler>().eatSound();
            }
        }
    }
}