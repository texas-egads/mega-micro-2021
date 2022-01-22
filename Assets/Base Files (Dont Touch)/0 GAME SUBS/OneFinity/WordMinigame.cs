using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneFinity {

    public class WordMinigame : MonoBehaviour
    {
        [Header("Animations")]
        public float cursorMoveTime;
        public float swapTime;
        public float swapArcSize;
        public float selectOffset;
        public float selectFadeTime;
        public float winOffset;
        public float winFadeTime;
        public float letterGrowTime;
        public Color normalColor;
        public Color selectedColor;
        public Color winColor;

        public ParticleSystem[] winParticles;

        [Header("Debug")]
        static readonly string TARGET_WORD = "LAUNCH";
        [SerializeField] int cursorPos = 0;

        public LetterTile[] letterTiles { get; private set; }
        public float[] tileXPositions { get; private set; }
        public WordCursor cursor { get; private set; }

        private Coroutine shakeCoroutine;
        private float shakeAmount;
        private float shakeRandom;

        private void Awake() {
            letterTiles = GetComponentsInChildren<LetterTile>();
            tileXPositions = letterTiles.Select(lt => lt.transform.position.x).ToArray();
            cursor = GetComponentInChildren<WordCursor>();
        }

        void Start()
        {
            for (int i = 0; i < letterTiles.Length; i++) {
                letterTiles[i].Letter = TARGET_WORD[i] + "";
            }

            // swap two letters at random
            int swapPos = Random.Range(1, letterTiles.Length - 1);
            letterTiles[swapPos].MoveXImmediate(tileXPositions[swapPos + 1]);
            letterTiles[swapPos + 1].MoveXImmediate(tileXPositions[swapPos]);
            var temp = letterTiles[swapPos];
            letterTiles[swapPos] = letterTiles[swapPos + 1];
            letterTiles[swapPos + 1] = temp;

            // set initial selection colors
            for (int i = 0; i < letterTiles.Length; i++) {
                letterTiles[i].SetTileColor(i <= 1 ? selectedColor : normalColor, selectFadeTime);
                letterTiles[i].SetTileOffsetImmediate(i <= 1 ? selectOffset : 0);
            }

            MinigameManager.Instance.minigame.gameWin = false;
            MinigameManager.Instance.PlaySound("Pad");
        }
        
        void Update()
        {
            if (MinigameManager.Instance.minigame.gameWin) {
                return;
            }

            int hInput =
                (Input.GetKeyDown(KeyCode.D) ? 1 : 0) -
                (Input.GetKeyDown(KeyCode.A) ? 1 : 0);
            if (hInput != 0) {
                // move cursor
                cursorPos += hInput;
                if (cursorPos > letterTiles.Length - 2)
                    cursorPos = 0;
                else if (cursorPos < 0) {
                    cursorPos = letterTiles.Length - 2;
                }

                cursor.MoveX(tileXPositions[cursorPos]);

                // do animations
                for (int i = 0; i < letterTiles.Length; i++) {
                    letterTiles[i].SetTileColor(i == cursorPos || i == cursorPos + 1 ? selectedColor : normalColor, selectFadeTime);
                    letterTiles[i].SetTileOffset(i == cursorPos || i == cursorPos + 1 ? selectOffset : 0, selectFadeTime);
                }

                // play sound
                MinigameManager.Instance.PlaySound("Select" + Random.Range(0, 3));
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                // handle swap
                letterTiles[cursorPos].MoveX(tileXPositions[cursorPos + 1]);
                letterTiles[cursorPos + 1].MoveX(tileXPositions[cursorPos]);
                var temp = letterTiles[cursorPos];
                letterTiles[cursorPos] = letterTiles[cursorPos + 1];
                letterTiles[cursorPos + 1] = temp;

                // play sound
                MinigameManager.Instance.PlaySound("Swap" + Random.Range(0, 2));
            }

            // check for a win
            bool win = true;
            for (int i = 0; win && i < letterTiles.Length; i++) {
                win = (i == letterTiles.Length - 1 || letterTiles[i].transform.position.x < letterTiles[i+1].transform.position.x)
                    && Mathf.Abs(letterTiles[i].transform.position.y - letterTiles[i].yPos) <= 0.1f
                    && letterTiles[i].Letter == TARGET_WORD[i] + "";
            }
            if (win) {
                MinigameManager.Instance.minigame.gameWin = true;
                StartCoroutine(DoWinSequence());
                return;
            }
        }


        private void FixedUpdate() {
            shakeRandom += Time.deltaTime * 10.0f;
            transform.position = Quaternion.Euler(0, 0, Mathf.PerlinNoise(0, shakeRandom) * 360f) * Vector3.one * shakeAmount;
        }


        IEnumerator DoWinSequence() {
            MinigameManager.Instance.PlaySound("Win");

            GetComponentInChildren<Animator>().Play("Launch");
            foreach (ParticleSystem ps in winParticles) {
                ps.Play();
            }

            cursor.FadeOut(selectFadeTime);
            for (int i = 0; i < letterTiles.Length; i++) {
                letterTiles[i].SetTileColorImmediate(winColor);
                letterTiles[i].SetTileColor(normalColor, winFadeTime);
                letterTiles[i].SetTileOffsetImmediate(winOffset);
                letterTiles[i].SetTileOffset(0, winFadeTime);
                yield return new WaitForSeconds(0.075f);
            }

            yield return new WaitForSeconds(0.1f);

            GetComponentInChildren<BGGalaxy>().FadeIn();
            for (int i = 0; i < letterTiles.Length; i++) {
                letterTiles[i].GrowLetter(letterGrowTime);
                yield return new WaitForSeconds(0.25f);
            }
        }



        public void Shake() {
            if (shakeCoroutine != null) {
                StopCoroutine(shakeCoroutine);
            }
            shakeCoroutine = StartCoroutine(DoShake());
        }
        IEnumerator DoShake() {
            shakeAmount = 0.5f;
            for (float t = 0; t < 0.5f; t += Time.deltaTime) {
                shakeAmount = Mathf.Lerp(0.5f, 0f, t / 0.5f);
                yield return 0;
            }
            shakeAmount = 0.0f;
            shakeCoroutine = null;
        }

    }
    

}
