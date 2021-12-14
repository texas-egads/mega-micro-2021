using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace t15
{
    public class grabber : MonoBehaviour
    {
        SumoEater script;
        // Start is called before the first frame update
        void Start()
        {
            script = GameObject.Find("SumoDude").GetComponent<SumoEater>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            script.setCaughtFood(true);
            other.gameObject.SetActive(false);
        }
    }
}
