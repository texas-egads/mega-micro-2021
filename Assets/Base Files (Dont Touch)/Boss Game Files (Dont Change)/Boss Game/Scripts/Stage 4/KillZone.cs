using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public int killCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        killCount++;
        Destroy(collision.gameObject);
    }
}
