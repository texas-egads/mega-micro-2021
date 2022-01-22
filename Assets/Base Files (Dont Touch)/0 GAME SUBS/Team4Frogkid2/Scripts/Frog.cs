using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FROGKID2
{

    public class Frog : MonoBehaviour
    {

        //frog anim
        [SerializeField]
        GameObject frogBody;

        [SerializeField]
        GameObject frogPoint;

        [SerializeField]
        GameObject frogWipe;

        //win anim
        [SerializeField]
        GameObject frogBodyWin;

        [SerializeField]
        GameObject frogHandWin;

        // lose anim
        [SerializeField]
        GameObject frogBodyLose;

        // Start is called before the first frame update
        void Start()
        {
            frogBody.SetActive(true);
            frogPoint.SetActive(true);
            frogWipe.SetActive(true);
            frogBodyWin.SetActive(false);
            frogHandWin.SetActive(false);
            frogBodyLose.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Win()
        {
            frogBody.SetActive(false);
            frogPoint.SetActive(false);
            frogWipe.SetActive(false);
            frogBodyWin.SetActive(true);
            frogHandWin.SetActive(true);
        }

        public void Lose()
        {
            frogBody.SetActive(false);
            frogPoint.SetActive(false);
            frogWipe.SetActive(false);
            frogBodyLose.SetActive(true);
        }
    }
}
