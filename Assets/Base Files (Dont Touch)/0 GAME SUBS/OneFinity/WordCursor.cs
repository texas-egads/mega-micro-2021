using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneFinity {
    public class WordCursor : MonoBehaviour
    {
        WordMinigame wordMinigame;
        SpriteRenderer cursorSprite;

        Coroutine moveCoroutine;

        private void Awake() {
            wordMinigame = GetComponentInParent<WordMinigame>();
            cursorSprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void MoveX(float newXPos) {
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(DoMoveX(newXPos));
        }
        IEnumerator DoMoveX(float newXPos) {
            float startXPos = transform.position.x;
            for (float t = 0; t < wordMinigame.cursorMoveTime; t += Time.deltaTime) {
                float frac = 1 - t / wordMinigame.cursorMoveTime;
                transform.position = new Vector2(Mathf.Lerp(newXPos, startXPos, frac * frac * frac), transform.position.y);
                yield return 0;
            }
            transform.position = new Vector2(newXPos, transform.position.y);
            moveCoroutine = null;
        }


        public void FadeOut(float time) {
            StartCoroutine(DoFadeOut(time));            
        }
        IEnumerator DoFadeOut(float time) {
            Color startColor = cursorSprite.color;
            for (float t = 0; t < time; t += Time.deltaTime) {
                cursorSprite.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0), t / time);
                yield return 0;
            }
            cursorSprite.color = Color.clear;
        }
    }
}