using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CriticalBass
{
    public class TextureScroll : MonoBehaviour
    {
        [SerializeField]
        public float scrollSpeed = 0;

        Vector3 targetPos;

        void Start()
        {
            targetPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            targetPos = transform.position - transform.right * scrollSpeed * Time.deltaTime;
            transform.position = targetPos;
        }
    }
}
