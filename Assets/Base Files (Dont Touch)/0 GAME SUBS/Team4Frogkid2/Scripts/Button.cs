using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FROGKID2
{

    public class Button : MonoBehaviour
    {

        [SerializeField]
        SweatsManager sweats;

        [SerializeField]
        Animator animator;

        [SerializeField]
        KeyCode key;

        bool isWin = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                bool check = sweats.CheckCondition(isWin);
                if (check)
                {
                    animator.SetBool("pressed", true);
                }
            }
        }

        public void SetIsWin(bool w)
        {
            isWin = w;
        }

        public void SetButtonAnim(int i)
        {
            animator.SetInteger("button", i);
        }
    }

}