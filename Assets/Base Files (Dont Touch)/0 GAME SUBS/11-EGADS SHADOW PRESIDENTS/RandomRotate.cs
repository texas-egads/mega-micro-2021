using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EGaDSShadowPres
{
    public class RandomRotate : MonoBehaviour
    {
        public float rotateBy;
        public float rotateMax = .4f;
        public float rotateMin = -.4f;

        public GameObject player;
        private Transform playerTf;

        private void Start()
        {
            playerTf = player.GetComponent<Transform>();
            InvokeRepeating("NewRotate", 0f, 1.5f);
        }

        private void Update()
        {
            playerTf.Rotate(new Vector3(0, 0, 1), rotateBy);
        }

        public void NewRotate()
        {
            rotateBy = Random.Range(rotateMin, rotateMax);
        }
    }
}

