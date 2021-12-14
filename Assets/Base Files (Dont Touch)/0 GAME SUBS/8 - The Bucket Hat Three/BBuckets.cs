using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBuckets {
    public class BBuckets : MonoBehaviour {
        public static BBuckets instance;

        private void Start() {
            MinigameManager.Instance.minigame.gameWin = true;
            StartCoroutine(SawsSound());
            StartCoroutine(FinalMoo());
        }

        private void Awake() {
            instance = this;
        }

        public void Lose() {
            if (MinigameManager.Instance.minigame.gameWin) {
                MinigameManager.Instance.minigame.gameWin = false;
                MinigameManager.Instance.PlaySound("squish");
                Cow.instance.Death();
            }
        }

        IEnumerator SawsSound() {
            yield return new WaitForSeconds(.5f);
            MinigameManager.Instance.PlaySound("saws");
        }

        IEnumerator FinalMoo() {
            yield return new WaitForSeconds(6.2f);
            if(MinigameManager.Instance.minigame.gameWin)
                MinigameManager.Instance.PlaySound("moo");
        }
    }
}
