using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class LPFSlowPan : MonoBehaviour
    {
        public Vector3 moveSpeed = new Vector3(-.01f, 0f, 0f);

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position + moveSpeed;
        }
    }
}

