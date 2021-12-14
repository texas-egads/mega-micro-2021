using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t15
{
    public class ElapsedTime : MonoBehaviour
    {
        public float time = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
        }
    }
}
