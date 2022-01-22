using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class TwinkleSoundEffect : MonoBehaviour
    {
        public void PlayTwinkle()
        {
            MinigameManager.Instance.PlaySound("twinkle");
        }
    }
}
