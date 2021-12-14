using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFoodSpawn : MonoBehaviour
{
    public Transform firePoint;
    public GameObject foodPrefab;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TossFood();
        }
    }

    void TossFood()
    {
        Instantiate(foodPrefab, firePoint.position, firePoint.rotation);
    }
}
