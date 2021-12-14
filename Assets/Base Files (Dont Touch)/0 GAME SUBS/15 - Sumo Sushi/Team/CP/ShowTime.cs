using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace t15
{
    public class ShowTime : MonoBehaviour
    {

        [SerializeField]
        private Text _text; // ezer to do this than get game object, get component
        ElapsedTime timer;
        // Start is called before the first frame update
        void Start()
        {
            timer = GameObject.Find("TimerObject").GetComponent<ElapsedTime>();
        }

        // Update is called once per frame
        void Update()
        {
            _text.text = Time.time.ToString("0.0");
        }
    }
}