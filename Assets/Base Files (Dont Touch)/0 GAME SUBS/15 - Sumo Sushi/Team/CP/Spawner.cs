using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace t15
{
    public class Spawner : MonoBehaviour
    {
        private float nextSpawnTime;

        [SerializeField]
        private GameObject foodPrefab; // must not be an instanced prefab. (rather, from assets)

        private float spawnDelay = 1.0f;
        ElapsedTime timer;

        void Start()
        {
            timer = GameObject.Find("TimerObject").GetComponent<ElapsedTime>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // spawnDelay = 1/ (3* Time.deltaTime * (float) Math.Pow(timer.time, 1.1)); //(float) Math.Pow(initSpawnDelay, 0.909091);
            //      print(spawnDelay);
            if (ShouldSpawn())
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            nextSpawnTime = Time.time + spawnDelay;
            //print(nextSpawnTime);
            Destroy(Instantiate(foodPrefab, transform.position, transform.rotation), 5);
        }


        private bool ShouldSpawn()
        {
            return Time.time >= nextSpawnTime;
        }
    }
}
