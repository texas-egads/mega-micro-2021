using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneFinity {
    public class LetterTile : MonoBehaviour
    {
        WordMinigame wordMinigame;

        [SerializeField] string letter;
        public string Letter {
            get { return letter; }
            set {
                letter = value;
                if (textMesh != null) {
                    textMesh.text = letter;
                }
            }
        }

        TMPro.TextMeshPro textMesh;
        public SpriteRenderer tileSprite;
        public Transform letterMask;
        public float yPos { get; private set; }

        public float maxLetterSpinSpeed;
        private float letterSpinSpeed;

        Coroutine moveCoroutine;
        Coroutine colorCoroutine;
        Coroutine offsetCoroutine;

        private void Awake() {
            wordMinigame = GetComponentInParent<WordMinigame>();
            textMesh = GetComponentInChildren<TMPro.TextMeshPro>();
            yPos = transform.position.y;
        }


        private void FixedUpdate() {
            letterMask.Rotate(new Vector3(0, 0, letterSpinSpeed * Time.deltaTime));
        }


        public void MoveXImmediate(float newXPos) {
            transform.position = new Vector2(newXPos, yPos);
        }
        public void MoveX(float newXPos) {
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(DoMoveX(newXPos));
        }
        IEnumerator DoMoveX(float newXPos) {
            float startXPos = transform.position.x;
            float startYPos = transform.position.y;
            float arcDir = Mathf.Sign(newXPos - startXPos);
            float arcYPos = yPos + arcDir * wordMinigame.swapArcSize;
            float endYPos = yPos;

            for (float t = 0; t < wordMinigame.swapTime; t += Time.deltaTime) {
                float xFrac = 1 - t / wordMinigame.swapTime;
                xFrac = xFrac * xFrac * xFrac;

                float currentEndY, yFrac;
                if (xFrac > 0.5f) {
                    currentEndY = startYPos;
                    yFrac = xFrac / 0.5f - 1f;
                }
                else {
                    currentEndY = endYPos;
                    yFrac = 1f - xFrac / 0.5f;
                }
                yFrac = yFrac * yFrac * yFrac;

                transform.position = new Vector2(
                    Mathf.Lerp(newXPos, startXPos, xFrac),
                    Mathf.Lerp(arcYPos, currentEndY, yFrac)
                );

                yield return 0;
            }

            transform.position = new Vector2(newXPos, yPos);
            moveCoroutine = null;
        }


        public void SetTileColorImmediate(Color newColor) {
            tileSprite.color = newColor;
        }
        public void SetTileColor(Color newColor, float time) {
            if (colorCoroutine != null) {
                StopCoroutine(colorCoroutine);
            }
            colorCoroutine = StartCoroutine(DoSetColor(newColor, time));
        }
        IEnumerator DoSetColor(Color newColor, float time) {
            Color startColor = tileSprite.color;
            for (float t = 0; t < time; t += Time.deltaTime) {
                tileSprite.color = Color.Lerp(startColor, newColor, t / wordMinigame.swapTime);
                yield return 0;
            }
            tileSprite.color = newColor;
            colorCoroutine = null;
        }


        public void SetTileOffsetImmediate(float offset) {
            tileSprite.transform.localPosition = new Vector2(0, offset);
        }
        public void SetTileOffset(float offset, float time) {
            if (offsetCoroutine != null) {
                StopCoroutine(offsetCoroutine);
            }
            offsetCoroutine = StartCoroutine(DoSetOffset(offset, time));
        }
        IEnumerator DoSetOffset(float offset, float time) {
            float startOffset = tileSprite.transform.localPosition.y;
            for (float t = 0; t < time; t += Time.deltaTime) {
                float frac = 1 - t / time;
                tileSprite.transform.localPosition = new Vector2(0, Mathf.Lerp(offset, startOffset, frac * frac * frac));
                yield return 0;
            }
            tileSprite.transform.localPosition = new Vector2(0, offset);
            offsetCoroutine = null;
        }


        public void GrowLetter(float time) {
            letterMask.SetParent(wordMinigame.transform);
            StartCoroutine(DoGrowLetter(time));
        }
        IEnumerator DoGrowLetter(float time) {
            float targetSpinSpeed = Random.Range(0, 1);
            targetSpinSpeed = targetSpinSpeed * targetSpinSpeed;
            targetSpinSpeed = (1 - targetSpinSpeed) * maxLetterSpinSpeed;
            targetSpinSpeed *= Random.Range(0f, 1f) > 0.5f ? 1 : -1;

            float startX = letterMask.position.x;
            float endX = letterMask.position.x * 2;
            float startY = letterMask.position.y;
            float startScale = letterMask.localScale.x;

            for (float t = 0; t < time; t += Time.deltaTime) {
                float frac = t / time;
                frac = frac * frac * frac;
                letterMask.transform.position = new Vector2(Mathf.Lerp(startX, endX, frac), Mathf.Lerp(startY, 0, frac));
                letterMask.transform.localScale = Vector3.one * Mathf.Lerp(startScale, 1, frac);
                letterSpinSpeed = Mathf.Lerp(0, targetSpinSpeed, t / time);

                yield return 0;
            }

            MinigameManager.Instance.PlaySound("Kick");
            wordMinigame.Shake();
        }
    }
}