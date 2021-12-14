using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BeekeeperKeepers {
    public class Message : MonoBehaviour {
        public Sprite gross;
        public Sprite delicious;
        SpriteRenderer myRenderer;
        Animator myAnim;
        // Start is called before the first frame update
        void Start() {
            myRenderer = GetComponent<SpriteRenderer>();
            myAnim = GetComponent<Animator>();
        }

        public void DisplayMessage(bool hasWon) {
            myRenderer.sprite = hasWon ? delicious : gross;
            myAnim.SetBool("start", true);
        }
    }
}