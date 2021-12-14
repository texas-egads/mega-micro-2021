using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers
{
    public class LiquidRenderer : MonoBehaviour
    {
        void Start()
        {

        }

        void LateUpdate()
        {
            transform.localScale = new Vector3((Camera.main.orthographicSize * 2) * Camera.main.aspect, Camera.main.orthographicSize * 2, 1);
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -1);
        }
    }
}

