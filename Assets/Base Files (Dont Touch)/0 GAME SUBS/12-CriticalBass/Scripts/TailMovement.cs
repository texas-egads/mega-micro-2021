using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalBass
{
    public class TailMovement : MonoBehaviour
    {
        [SerializeField]
        private float frequency = 5f;
        [SerializeField]
        private float magnitude = 3f;
        [SerializeField]
        private float offset = 0f;

        public bool isEat;

        private Vector3 targetPos;

        void Start()
        {
            isEat = false;
            MinigameManager.Instance.minigame.gameWin = false;
            targetPos = transform.position;
        }

        void Update()
        {
            transform.position = targetPos + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            isEat = true;
            Destroy(this.gameObject);
        }
    }
}