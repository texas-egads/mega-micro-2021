using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class LPFSusFrog : MonoBehaviour
    {
        public bool isSus = false;
        public GameObject realFrog;
        public GameObject fakeFrog;
        // Start is called before the first frame update

        public void SpawnFrog() { if (isSus == false) { realFrog.SetActive(true); } else { fakeFrog.SetActive(true); } }
    }
}

