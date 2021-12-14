using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers2
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform followObject;
        public Vector3 camOffset;

        void Start()
        {

        }

        void LateUpdate()
        {
            transform.position = followObject.position + (camOffset);
        }
    }
}
