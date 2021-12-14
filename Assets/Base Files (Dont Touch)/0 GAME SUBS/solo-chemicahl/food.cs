using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chemicahl
{
    public class food : MonoBehaviour
    {
        int timePassed = 0;
        public seagull sg1;
        public seagull sg2;
        public seagull sg3;
        public seagull sg4;

        private IEnumerator coroutine;

        //public AudioSource hit;
        // Start is called before the first frame update
        void Start()
        {
            MinigameManager.Instance.PlaySound("gullcoming2");
            //Debug.Log("first sound");
            //Debug.Log(transform.position.x + ", " + transform.position.y);
            //coroutine = soundDelay(2.0f);
            //StartCoroutine(coroutine);
        }

        // Update is called once per frame
        void Update()
        {
            if (sg1.transform.position.x > -2.5 && sg1.transform.position.x < 0)
            {
                sg1.GetComponent<SpriteRenderer>().sprite = sg1.newSprite;
                if (Input.GetKeyDown(KeyCode.W))
                {
                    //Debug.Log("destroyed");
                    MinigameManager.Instance.PlaySound("slap");
                    
                    sg1.path1y.Set(0, 0.1f, 0);
                    MinigameManager.Instance.minigame.gameWin = true;
                }
                else if(sg1.transform.position.y < 0)
                {
                    //MinigameManager.Instance.PlaySound("seagullcry");
                    MinigameManager.Instance.minigame.gameWin = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            /*if (sg2.transform.position.x < 2.5 && sg2.transform.position.x > 0)  //game used to be long but now it's short so everything after this is useless
            {
                sg2.GetComponent<SpriteRenderer>().sprite = sg2.newSprite;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //Debug.Log("destroyed2");
                    MinigameManager.Instance.PlaySound("slap");
                    sg2.path2y.Set(0, 0.1f, 0);
                }
            }
            if (sg3.transform.position.x > -2.5 && sg3.transform.position.x < 0)
            {
                sg3.GetComponent<SpriteRenderer>().sprite = sg3.newSprite;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //Debug.Log("destroyed3");
                    MinigameManager.Instance.PlaySound("slap");
                    sg3.path3y.Set(0, -0.1f, 0);
                }
            }
            if (sg4.transform.position.x < 2.5 && sg4.transform.position.x > 0)
            {
                sg4.GetComponent<SpriteRenderer>().sprite = sg4.newSprite;
                if (Input.GetKeyDown(KeyCode.D))
                {
                    //Debug.Log("destroyed4");
                    MinigameManager.Instance.PlaySound("slap");
                    sg4.path4y.Set(0, -0.1f, 0);
                    MinigameManager.Instance.minigame.gameWin = true;
                }
            }*/
        }

        IEnumerator soundDelay(float waitTime)
        {
            yield return new WaitForSeconds(1.0f);
            //MinigameManager.Instance.PlaySound("gullcoming2");
            //Debug.Log("second sound");
        }


    }
}
