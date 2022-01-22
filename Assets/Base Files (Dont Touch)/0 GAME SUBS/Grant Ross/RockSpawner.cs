using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grant
{
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D spawnCircle;
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private float spawnTimer;

        private float timer;

        private void Start()
        {
            timer = .2f;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0) SpawnRock();
        }

        private void SpawnRock()
        {
            timer = spawnTimer;
            var spawnPoint = Random.Range(0f,360f);
            var spawnPos = new Vector3(Mathf.Cos(spawnPoint), Mathf.Sin(spawnPoint)) * spawnCircle.radius;
            Instantiate(rockPrefab, spawnPos, Quaternion.identity);
        }
        
    }
}
