using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneFinity {
    public class BGGalaxy : MonoBehaviour
    {
        public float spinSpeed;
        private SpriteRenderer spriteRenderer;
        public SpriteRenderer otherBG;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate() {
            transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
        }

        public void FadeIn() {
            StartCoroutine(DoFadeIn());
        }
        IEnumerator DoFadeIn() {
            Color otherStartColor = otherBG.color;
            for (float t = 0; t < 2f; t += Time.deltaTime) {
                spriteRenderer.color = new Color(1, 1, 1, t / 2f);
                otherBG.color = Color.Lerp(otherStartColor, Color.black, t / 2f);
                yield return 0;
            }
            spriteRenderer.color = Color.white;
        }
    }
}
