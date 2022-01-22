using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
    {
    public class AsteroidSpawner : MonoBehaviour
    {
        public GameObject asteroidObj;
        public Transform [] spawnLocations;
        [SerializeField] private float spawnDelay;

        private bool shouldSpawn = true;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(startSpawning());
            Stage4.instance.stageOver.AddListener(stopSpawning);
        }

        void stopSpawning()
        {
            shouldSpawn = false;
        }

        private IEnumerator startSpawning()
        {
            bool goingRight = false;
            int index = Random.Range(0, spawnLocations.Length);

            int index2 = Random.Range(0, spawnLocations.Length);
            while (shouldSpawn)
            {
                // generate a random staircase pattern
                index += (goingRight) ? 1 : -1;
                if (index < 0 || index >= spawnLocations.Length)
                {
                    index = Random.Range(0, spawnLocations.Length);
                    goingRight = Random.Range(0, 2) == 0 ? true : false;
                }

                GameObject obj = Instantiate(asteroidObj, spawnLocations[index].position, Quaternion.identity);
                obj.transform.parent = transform;

                // generate a second staricase pattern
                index2 += (!goingRight) ? 1 : -1;
                if (index2 < 0 || index2 >= spawnLocations.Length)
                {
                    index2 = Random.Range(0, spawnLocations.Length);
                }

                if (index2 != index)
                {
                    obj = Instantiate(asteroidObj, spawnLocations[index2].position, Quaternion.identity);
                    obj.transform.parent = transform;
                }

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
