using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palepiney
{
    public class Destroy : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            Destroy(col.gameObject);
        }
    }
}
