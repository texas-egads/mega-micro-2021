using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private bool walkRight;
    void Start()
    {
        
    }

    private IEnumerator EnemySpawn()
    {
        yield return null;
    }
}
