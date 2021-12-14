using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Mindy{
    
    public class Game : MonoBehaviour
    {

        public enum GameStage
        {
            NONE,
            BASE,
            TOP
        };
            
        public Transform pointer;
        public Mindy.Slot[] slots;

        public Sprite correctBase;
        public Sprite correctTop;

        public Sprite[] badBases;
        public Sprite[] badTops;

        public SpriteRenderer sushiBase;
        public SpriteRenderer sushiTop;

        public float pointerInterval = 0.3f;
        public float topYOffset = -1.7f;
        public float winMusicDelay = 0.2f;
        public float defaultLoseDelay = 3.3f;

        [Header("donut touch")]
        public GameStage gameStage = GameStage.NONE;
        public int correctBaseIndex = 0;
        public int correctTopIndex = 0;
        public int activeSlotIndex = 0;
        public string endSound = "";

        public IEnumerator pointerLoop;
        public IEnumerator defaultLoseLoop;

        public bool baseWin = false;
        public bool topWin = false;


        private void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            
            bool baseWin = false; 
            bool topWin = false;
        
            SetActiveSlot(0);

            pointerLoop = PointerLoop();
            StartCoroutine(pointerLoop);

            defaultLoseLoop = DefaultLose();
            StartCoroutine(defaultLoseLoop);

            correctBaseIndex = Random.Range(0, slots.Length - 1);
            correctTopIndex = Random.Range(0, slots.Length - 1);
            
            ShuffleSprites(badBases);
            ShuffleSprites(badTops);
            
            // Bases first
            FillSlots(correctBaseIndex, correctBase, badBases);
            gameStage = GameStage.BASE;
            
            MinigameManager.Instance.PlaySound("MindyMusic");
        }

        private void Update()
        {
            if (Input.GetButtonDown("Space"))
            {

                if (gameStage == GameStage.BASE)
                {
                    sushiBase.sprite = slots[activeSlotIndex].spriteRenderer.sprite;
                    
                    FillSlots(correctTopIndex, correctTop, badTops);

                    baseWin = activeSlotIndex == correctBaseIndex;

                    if (baseWin)
                    {
                        MinigameManager.Instance.PlaySound("good");
                    }
                    else
                    {
                        MinigameManager.Instance.PlaySound("bad");
                    }
                    
                    // Shift all sprite renderers down for offset
                    for (int i = 0; i < slots.Length; i++)
                    {
                        Vector3 pos = slots[i].spriteRenderer.transform.position;
                        slots[i].spriteRenderer.transform.position = new Vector3(pos.x, pos.y + topYOffset, pos.z);
                    }
                    
                    gameStage = GameStage.TOP;

                } else if (gameStage == GameStage.TOP)
                {
                    sushiTop.sprite = slots[activeSlotIndex].spriteRenderer.sprite;
                    
                    topWin = activeSlotIndex == correctTopIndex;

                    if (topWin)
                    {
                        MinigameManager.Instance.PlaySound("good");
                    }
                    else
                    {
                        MinigameManager.Instance.PlaySound("bad");
                    }

                    if (baseWin && topWin)
                    {
                        endSound = "win";
                        MinigameManager.Instance.minigame.gameWin = true;
                    }
                    else
                    {
                        endSound = "lose";
                    }

                    StopCoroutine(defaultLoseLoop);
                    StartCoroutine(EndSound());
                    
                    gameStage = GameStage.NONE;
                }
            }
        }

        private void FillSlots(int correctIndex, Sprite correctSprite, Sprite[] badSprites)
        {
            int nextBadSprite = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == correctIndex)
                {
                    slots[i].spriteRenderer.sprite = correctSprite;
                }
                else
                {
                    slots[i].spriteRenderer.sprite = badSprites[nextBadSprite];
                    nextBadSprite++;
                }
            }
        }

        private void ShuffleSprites(Sprite[] list)
        {
            for (int i = 0; i < list.Length; i++) {
                Sprite temp = list[i];
                int randomIndex = Random.Range(i, list.Length);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        private void SetActiveSlot(int i)
        {
            activeSlotIndex = i;
            pointer.position = new Vector3(pointer.position.x, slots[i].transform.position.y, pointer.position.z);
        }
        
        private IEnumerator DefaultLose()
        {
            yield return new WaitForSeconds(defaultLoseDelay);
            MinigameManager.Instance.PlaySound("lose");
        }


        private IEnumerator EndSound()
        {
            yield return new WaitForSeconds(winMusicDelay);
            MinigameManager.Instance.PlaySound(endSound);
        }

        private IEnumerator PointerLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(pointerInterval);
                int newSlotIndex = (activeSlotIndex + 1) % slots.Length;
                SetActiveSlot(newSlotIndex);
            }
        }
    }
}