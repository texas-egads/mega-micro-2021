using System.Collections;
using UnityEngine;

namespace ToadallyAwesome {
    public class AsteroidSpawner : MonoBehaviour {

        [SerializeField]
        private GameObject asteroidPrefab;

        [SerializeField]
        private float spawnX = 9.0f;

        [SerializeField]
        private float minSpawnY = -2.0f;

        [SerializeField]
        private float maxSpawnY = 2.0f;

        [SerializeField]
        private float spawnZ = 0.0f;

        [SerializeField]
        private Vector2 asteroidVelocity = new Vector2(-7, 0);

        [SerializeField]
        private float timeBetweenAsteroids = 1.0f;

        private void Start() {
            StartCoroutine(SpawnAsteroids());
        }

        private IEnumerator SpawnAsteroids() {
            while(true) {
                float spawnY = Random.Range(minSpawnY, maxSpawnY);
                Vector3 asteroidSpawnPosition = new Vector3(spawnX, spawnY, spawnZ);
                GameObject asteroid = Instantiate(asteroidPrefab, asteroidSpawnPosition, Quaternion.identity);
                asteroid.GetComponent<Rigidbody2D>().velocity = asteroidVelocity;
                yield return new WaitForSeconds(timeBetweenAsteroids);
            }
        }
    }
}
