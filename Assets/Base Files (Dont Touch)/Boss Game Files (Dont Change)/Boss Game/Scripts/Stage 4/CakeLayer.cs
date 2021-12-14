using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeLayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                BossGameManager.Instance.PlaySound("tier1");
                break;
            case 1:
                BossGameManager.Instance.PlaySound("tier2");
                break;
            case 2:
                BossGameManager.Instance.PlaySound("tier3");
                break;
        }
    }
}
