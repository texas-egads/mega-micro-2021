using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class StageManager : MonoBehaviour
    {
        public GameObject[] stages;
        private GameObject currentStage;
        private StageController curStageController;
        private int stageIndex;
        public Animator sceneTransition;
        public float stageLoadDelay;

        private void Start()
        {
            currentStage = Instantiate(stages[0]);
            curStageController = currentStage.GetComponent<StageController>();
            curStageController.gameWon.AddListener(NextStage);
        }

        private void NextStage()
        {
            sceneTransition.Play("Swipe");
            StartCoroutine(DelayedLoadStage(stageLoadDelay));
            if (stageIndex != stages.Length - 1)
            {
                BossGameManager.Instance.PlaySound("success");
            }
        }
        private IEnumerator DelayedLoadStage(float delay)
        {
            yield return new WaitForSeconds(delay);
            stageIndex++;
            if (stageIndex != stages.Length)
            {
                Destroy(currentStage);
                currentStage = Instantiate(stages[stageIndex]);
                curStageController = currentStage.GetComponent<StageController>();
                curStageController.gameWon.AddListener(NextStage);
            }
        }
    }
}
