using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArtsAndLovecrafts
{
    public class CroakGameScript : MonoBehaviour
    {
        private int currentFroggeStage, currentLadyStage;
        private bool isFinished;

        [SerializeField] Animator froggeAnimator;
        [SerializeField] Animator ladyAnimator;

        [SerializeField] List<int> froggeStageCounters;
        [SerializeField] List<int> ladyStageCounters;

        // Start is called before the first frame update
        void Start()
        {
            currentFroggeStage = 0;
            currentLadyStage = 0;

            isFinished = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isFinished) 
            { 
                // check for input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    froggeStageCounters[currentFroggeStage]--;
                    ladyStageCounters[currentLadyStage]--;
                }

                // update frogge stage when counter for current stage is depeleted
                if (froggeStageCounters[currentFroggeStage] <= 0)
                {
                    currentFroggeStage++;

                    // if reached end of stages, win
                    if (currentFroggeStage == froggeStageCounters.Count)
                    {
                        MinigameManager.Instance.minigame.gameWin = isFinished = true;
                        MinigameManager.Instance.PlaySound("Croak");
                    }

                    // advance frogge animation
                    froggeAnimator.SetTrigger("advance");
                }

                // update lady stage when counter for current stage is depeleted
                if (ladyStageCounters[currentLadyStage] <= 0)
                {
                    currentLadyStage++;
                    Debug.Log("Made it");
                    // if reached end of stages, win
                    if (currentLadyStage == ladyStageCounters.Count)
                    {
                        MinigameManager.Instance.minigame.gameWin = isFinished = true;
                    }

                    // advance frogge animation
                    ladyAnimator.SetTrigger("advance");
                }
            }
        }
    }
}
