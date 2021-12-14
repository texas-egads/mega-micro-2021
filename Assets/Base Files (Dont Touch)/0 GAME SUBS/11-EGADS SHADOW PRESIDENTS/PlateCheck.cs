using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGaDSShadowPres
{
    public class PlateCheck : MonoBehaviour
    {
        private void Start()
        {
            MinigameManager.Instance.minigame.gameWin = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                MinigameManager.Instance.minigame.gameWin = false;
            }
        }
    }
}
 
