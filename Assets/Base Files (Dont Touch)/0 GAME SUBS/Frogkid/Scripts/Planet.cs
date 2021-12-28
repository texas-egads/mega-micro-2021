using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FROGKID
{
    public class Planet : MonoBehaviour
    {
        [SerializeField]
        string name = "";

        [SerializeField]
        Transform transform;

        [SerializeField]
        Animator animator;

        private bool isHome = false;

        private void Update()
        {
            transform.Rotate(0, 0, 10 * Time.deltaTime);
        }

        public void setPlanetNum(int num)
        {
            animator.SetInteger("planet", num);
            if(num == 0)
            {
                isHome = true;
            }
        }

        public bool getIsHome()
        {
            return isHome;
        }

    }
}
