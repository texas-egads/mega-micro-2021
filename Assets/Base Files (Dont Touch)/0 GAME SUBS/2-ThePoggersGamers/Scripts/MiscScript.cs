using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePoggersGamers {
    public class MiscScript : MonoBehaviour
    {

        [SerializeField] Sprite defaultSprite;
        [SerializeField] Sprite altSprite;

        private SpriteRenderer spriteRenderer;

        //Starts the object with whatever is deemed its default 
        void Start() {  
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = defaultSprite;
        }

        //on easter egg activation object switches to it's alt
        //this can be "none" in the inspector, which in turn causes new sprite to be nothing, meaning it disappears (correct behavior in some cases)
        public void EasterEgg() {
            spriteRenderer.sprite = altSprite;
        }
    }
}
