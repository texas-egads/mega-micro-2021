using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EGaDSShadowPres
{
    public class playerController : MonoBehaviour
    {

        private GameObject player;
        private Transform tf;
        public float rotateAmount = 5f;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            tf = player.GetComponent<Transform>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                tf.Rotate(new Vector3(0, 0, 1), rotateAmount);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                tf.Rotate(new Vector3(0, 0, 1), -rotateAmount);
            }

        }

    }
}

