using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FROGKID2
{

    public class SweatsManager : MonoBehaviour
    {

        [SerializeField]
        List<Button> buttons;

        [SerializeField]
        private List<int> buttonSelector;

        //frog anim
        [SerializeField]
        GameObject frogBody = null;

        [SerializeField]
        GameObject frogPoint = null;

        [SerializeField]
        GameObject frogWipe = null;

        //win anim
        [SerializeField]
        GameObject frogBodyWin = null;

        [SerializeField]
        GameObject frogHandWin = null;

        // lose anim
        [SerializeField]
        GameObject frogBodyLose = null;

        [SerializeField]
        List<Planet> planets;

        [SerializeField]
        ParticleSystem particleSystem;


        private bool isFailed = false;


        // Start is called before the first frame update
        void Start()
        {

            MinigameManager.Instance.minigame.gameWin = false;
            frogBody.SetActive(true);
            frogPoint.SetActive(true);
            frogWipe.SetActive(true);
            frogBodyWin.SetActive(false);
            frogHandWin.SetActive(false);
            frogBodyLose.SetActive(false);
            particleSystem.Stop();

            int winId = Random.Range(0, buttons.Count);


            for (int i = 0; i < buttons.Count; i++)
            {
                if (i != winId)
                {
                    // select a sprite at random
                    if (buttonSelector.Count > 0)
                    {
                        int index = Random.Range(0, buttonSelector.Count);
                        int p = buttonSelector[index];
                        buttonSelector.RemoveAt(index);
                        buttons[i].SetButtonAnim(p);
                    }
                    else
                    {
                        // safety fallback
                        buttons[i].SetButtonAnim(0);
                    }
                }
                else
                {
                    buttons[i].SetButtonAnim(0);
                    buttons[winId].SetIsWin(true);
                }
            }


        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool CheckCondition(bool isWin)
        {
            if (!isFailed && !MinigameManager.Instance.minigame.gameWin)
            {
                if (isWin)
                {
                    Win();
                }
                else
                {
                    Lose();
                    foreach(Planet p in planets)
                    {
                        p.SetShake();
                    }
                }
                return true;
            }

            return false;
        }

        void Win()
        {
            MinigameManager.Instance.minigame.gameWin = true;
            frogBody.SetActive(false);
            frogPoint.SetActive(false);
            frogWipe.SetActive(false);
            frogBodyWin.SetActive(true);
            frogHandWin.SetActive(true);
            MinigameManager.Instance.PlaySound("button");
        }

        void Lose()
        {
            isFailed = true;
            Debug.Log("lose");
            frogBody.SetActive(false);
            frogPoint.SetActive(false);
            frogWipe.SetActive(false);
            frogBodyLose.SetActive(true);
            particleSystem.Play();
            MinigameManager.Instance.PlaySound("explosion");
        }
    }
}