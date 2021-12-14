using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostGOAT
{
    public class RythmMoves : MonoBehaviour
    {
        protected float Animation;
        private bool isMoving;
        private Vector3 startPos;
        private Vector3 targetPos;

        public bool IveBeenGrabbed = false;

        [SerializeField] private GameObject indention;

        public bool IStabbedAMeatyBall = false;

        private GameObject Fork;

        private void Awake()
        {
            Fork = GameObject.FindGameObjectWithTag("Object 1");
        }

        // Start is called before the first frame update
        void Start()
        {
            targetPos = transform.position;
            startPos = transform.position;
            indention.SetActive(false);
            this.GetComponent<Animator>().SetBool("Dance", true);

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 forkPos = Fork.transform.position;
            //print(IveBeenGrabbed + " " + IStabbedAMeatyBall);
            if (IveBeenGrabbed == false && IStabbedAMeatyBall == false) {
                Animation += Time.deltaTime;

                Animation = Animation % .429f;

                if (isMoving == true)
                {
                    transform.position = MathParabola.Parabola(transform.position, targetPos, 25f, Animation / .429f);
                }

                if (isMoving == false)
                {
                    StartCoroutine(Move());
                }
            }
            else if (IStabbedAMeatyBall == true)
            {
                //Debug.Log("GO UP");
                indention.SetActive(true);
                this.GetComponent<Animator>().SetBool("Dance", false);
                //this.transform.position += new Vector3(0, 1.1f, 0);
                //this.transform.SetParent(Fork.transform, false);
                this.transform.position = new Vector3(forkPos.x, forkPos.y - 250*.75f, forkPos.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, -90);
                StartCoroutine(Kill());
            }
        }

        private IEnumerator Move()
        {
            targetPos += new Vector3(112.5f, 0f, 0f);
            isMoving = true;
            //print("HI " + startPos + " " + targetPos);
            //transform.position += new Vector3(112.5f, 0f, 0f);
            yield return new WaitForSeconds(.429f);
            isMoving = false;
            //startPos += new Vector3(112.5f, 0f, 0f);
        }
        private IEnumerator Kill()
        {
            yield return new WaitForSeconds(.7f);
            Destroy(this.gameObject);
        }
    }
}
