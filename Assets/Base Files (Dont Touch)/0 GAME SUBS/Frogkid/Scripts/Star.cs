using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FROGKID
{


    public class Star : MonoBehaviour
    {


        [SerializeField]
        Transform transform;


        // Start is called before the first frame update
        void Start()
        {

        }

        private void Update()
        {
            transform.Rotate(0, 0, 10 * Time.deltaTime);
        }
    }

}