using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BEEZNUTZ
{
    public class MenuButton : MonoBehaviour
    {
        private bool isWin = false;
        public int thisIndex;
        [SerializeField] Toggle[] group;
        private bool scrollReady = true;
        private bool hasLost = false;
        public GameObject hand;
        [SerializeField] GameObject win, lose;

        private void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            thisIndex = 0;
        }
        private void Update()
        {
            if (Input.GetButtonDown("Space"))toggleSelect();
            if (!isWin)
            {
                if (scrollReady && Input.GetAxisRaw("Vertical") != 0) StartCoroutine(VerticalScroll(Input.GetAxisRaw("Vertical")));
            }

            if (group[1].isOn)
            {
                hand.transform.Translate(Vector2.right);
                gameLose();
                isWin = true;
                lose.SetActive(true);
            }
            else if (group[0].isOn & group[2].isOn)
            {
                hand.transform.Translate(Vector2.right);
                gameWin();
                isWin = true;
                win.SetActive(true);
            }

        }

        private IEnumerator VerticalScroll(float input)
        {
            scrollReady = false;
            int newIndex = thisIndex; 
            if (input > 0)
            {
                if(thisIndex != 0)
                {
                    newIndex--;
                    hand.transform.position = group[newIndex].transform.position;
                }
            }
            else
            {
                if (thisIndex != group.Length-1)
                {
                    newIndex++;
                    hand.transform.position = group[newIndex].transform.position;
                }
            }
            thisIndex = newIndex;
            while (Input.GetAxisRaw("Vertical") != 0) yield return null;
            scrollReady = true;
        }

        private void toggleSelect() 
        {
            group[thisIndex].isOn = !group[thisIndex].isOn;
            MinigameManager.Instance.PlaySound("click");
        }
        public void gameWin()
        {

            if (!MinigameManager.Instance.minigame.gameWin)
            {
                MinigameManager.Instance.minigame.gameWin = true;
                MinigameManager.Instance.PlaySound("win");
            }
        }
        public void gameLose()
        {
            if (!hasLost)
            {
                MinigameManager.Instance.PlaySound("lose");
                hasLost = true;
            }
        }
    }
}