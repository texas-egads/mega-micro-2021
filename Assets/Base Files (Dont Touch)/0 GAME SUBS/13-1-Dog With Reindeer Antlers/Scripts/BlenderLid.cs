using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers
{
    public class BlenderLid : MonoBehaviour
    {
        public Transform blenderHolderTransform;
        public Transform blenderLidHolder;

        private bool blending = true;
        private float progress = 0;

        void Update()
        {
            if (blending)
            {
                transform.position = new Vector3(blenderHolderTransform.position.x, blenderHolderTransform.position.y + (progress / 5), blenderHolderTransform.position.z);
            }
        }

        public void UpdateProgress(float newProgress)
        {
            progress = newProgress;
        }

        public void Lose()
        {
            blending = false;
        }
    }
}