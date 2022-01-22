using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class MazeGenerator : MonoBehaviour
    {
        public GameObject[] mazeLayouts;

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(mazeLayouts[Random.Range(0, mazeLayouts.Length)], transform);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
