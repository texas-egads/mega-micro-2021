using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BeekeeperKeepers {
    public class MenuManager : MonoBehaviour {
        ListManager lm;
        public SpriteList menuSprites;
        // Start is called before the first frame update
        void Start() {
            lm = FindObjectOfType<ListManager>();
            DisplayMenu();
        }

        // displays the winning ingredients to the player
        public void DisplayMenu() {
            int childIndex = 1;
            foreach (IngredientType ing in lm.winList) {
                SpriteRenderer childRenderer = transform.GetChild(childIndex).gameObject.GetComponent<SpriteRenderer>();
                if (childRenderer == null) {
                    Debug.LogError("It needs a renderer, doofus");
                }
                else {
                    childRenderer.sprite = menuSprites.sprites[(int)ing];
                }
                childIndex++;
            }
        }
    }
}