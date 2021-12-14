using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Rocket : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BossGameManager.Instance.PlaySound("rocketexplode");
            GetComponent<RocketHit>().RocketIsHit();
        }
    }
}
