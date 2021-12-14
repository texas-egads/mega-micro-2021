using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t15
{
    public class StartIcon : MonoBehaviour
    {
        SpriteRenderer sr;
        // Start is called before the first frame update
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position + new Vector3(0, -0.02f, 0);

        }
    }
}