using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace BeeNice
{
    public class StageController : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent gameWon;
        public GameObject introText;
        public string introName;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            if(gameWon == null)
            {
                gameWon = new UnityEvent();
            }
            if(introName != "")
            {
                var intro = Instantiate(introText, transform);
                intro.GetComponentInChildren<Text>().text = introName;
            }
        }

        public virtual void LoseGame()
        {
            StartCoroutine(LoseGameHelper());
        }
        private IEnumerator LoseGameHelper()
        {
            BossGameManager.Instance.bossGame.gameWin = false;
            yield return new WaitForSeconds(.01f);
            BossGameManager.Instance.bossGame.gameOver = true;
            BossGameManager.Instance.PlaySound("fail");
        }
    }
}
