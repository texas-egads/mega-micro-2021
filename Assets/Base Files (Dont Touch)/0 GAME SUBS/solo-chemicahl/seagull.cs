using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chemicahl
{
    public class seagull : MonoBehaviour
    {
        public int pathNum = 0;
        public Vector3 path1x;
        public Vector3 path1y;

        public Vector3 path2x;
        public Vector3 path2y;

        public Vector3 path3x;
        public Vector3 path3y;

        public Vector3 path4x;
        public Vector3 path4y;

        //public SpriteRenderer sr;
        public Sprite newSprite;

        private IEnumerator coroutine1;

        // Start is called before the first frame update
        void Start()
        {
            path1x = (Vector3.right * .05f);
            path1y = (Vector3.down * .05f);

            path2x = (Vector3.left * Time.deltaTime);
            path2y = (Vector3.down * Time.deltaTime);

            path3x = (Vector3.right * Time.deltaTime);
            path3y = (Vector3.up * Time.deltaTime);

            path4x = (Vector3.left * Time.deltaTime);
            path4y = (Vector3.up * Time.deltaTime);

        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (pathNum == 1)
            {
                path1();

            }
            /*else if(pathNum == 2)
            {
                coroutine1 = nextBird(5.0f);
                StartCoroutine(nextBird(5.0f));
                //path2();
            }
            coroutine1 = nextBird(5.0f);
            StartCoroutine(nextBird(5.0f));*/

        }
        void path1()
        {
            transform.Translate(path1x);//moves object sideways
            transform.Translate(path1y);//moves object down
            //Debug.Log("path1");
            //Debug.Log(transform.position.x);
            //Debug.Log("help");
            if (transform.position.y <= 0)
            {
                //Debug.Log("lost");
                MinigameManager.Instance.minigame.gameWin = false;
            }

        }
        /*void path2()    //game used to be long but now it's short so everything after this is useless
        {
            transform.Translate(path2x);
            transform.Translate(path2y, Space.World);
            //Debug.Log("path2");
            if (transform.position.y <= 0)
            {
                //Debug.Log("lost");
                MinigameManager.Instance.minigame.gameWin = false;
            }
        }
        void path3()
        {
            transform.Translate(path3x);
            transform.Translate(path3y, Space.World);
            if (transform.position.y >= 0)
            {
                //Debug.Log("lost");
                MinigameManager.Instance.minigame.gameWin = false;
            }
        }
        void path4()
        {
            transform.Translate(path4x);
            transform.Translate(path4y, Space.World);
            if (transform.position.y >= 0)
            {
                //Debug.Log("lost");
                MinigameManager.Instance.minigame.gameWin = false;
            }
        }

        IEnumerator nextBird(float waitTime)
        {
            yield return new WaitForSeconds(1.3f);
            //Debug.Log("incoming");
            if (pathNum == 2)
            {
                path2();
            }
            if (pathNum == 3)
            {
                yield return new WaitForSeconds(1.6f);
                path3();
            }
            if (pathNum == 4)
            {
                yield return new WaitForSeconds(2.7f);
                path4();
            }
        }*/
    }
}







