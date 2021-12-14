using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostGOAT
{

    public class Fork : MonoBehaviour
    {
        
        private bool MissStab = false;
        private Animator forkAnimator;
        private bool gotMeat = false;

        // Start is called before the first frame update
        void Start()
        {
            forkAnimator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                //stabPasta();
                //Debug.Log("stabbing");
                forkAnimator.SetTrigger("Stab");
            }
        }

        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ground")
            {
                //Debug.Log("Ive hit meat");
                gotMeat = true;
                //collision.GetComponent<Animator>().SetTrigger("Rise");
                //collision.GetComponent<RythmMoves>().IveBeenGrabbed = true;
                //collision.GetComponent<RythmMoves>().Rise();
            }
            if (collision.tag == "Object 1")
            {
                
                if (!gotMeat)
                {
                    //you've missed
                    //Debug.Log("I've miss Stabbed D:");
                    MissStab = true;
                }

            }
        }
    }
}