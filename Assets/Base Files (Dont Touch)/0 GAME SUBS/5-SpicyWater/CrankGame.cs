using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpicyWater
{
    public class CrankGame : MonoBehaviour
    {
        //public Text UIText;
        public string startText;
        public string winText;
        public int crankMax;
        public int crankCurrent;
        public float crankStep;
        public int letterPos;
        private bool won;
        public string[] crankSequence = new string[4] ;

        SpriteRenderer spriteRenderer;
        public GameObject windowL;
        public GameObject windowR;
        //public SoundAsset crankBeep;

        Vector3 spin = new Vector3(0, 0, 90);
        // Start is called before the first frame update
        void Start()
        {
            //UIText.text = startText;
            MinigameManager.Instance.minigame.gameWin = false;
            crankCurrent = 0;
            won = false;
            crankMax = Random.Range(10, 15);
            if (crankMax != 0)
                crankStep = 1.0f / crankMax;
            letterPos = 0;
            crankSequence = new string[] { "w", "a", "s", "d" };
            print(crankSequence);
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (letterPos >= crankSequence.Length)
                letterPos = 0;
            if (correctKey())
            {
                crankCurrent++;
                spriteRenderer.transform.Rotate(spin);
                
                print("inputs " + crankCurrent);;
                windowL.transform.Translate(crankStep * -5f, 0, 0);
                windowR.transform.Translate(crankStep * -5f, 0, 0);
            }
            if ((crankCurrent >= crankMax)&(!won))
            {
                MinigameManager.Instance.PlaySound("win");
                MinigameManager.Instance.minigame.gameWin = true;
                won = true;
            }
        }
        bool correctKey()
        {
            if (Input.GetKey(crankSequence[letterPos]))
            {
                letterPos++;
                MinigameManager.Instance.PlaySound("crankBeep");
                return true;

            }

            else return false;
                     
        }
        
    }
}