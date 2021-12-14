using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t15
{
    public class moveUp : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position += new Vector3(0, -0.08f, 0);
        }
    }
}