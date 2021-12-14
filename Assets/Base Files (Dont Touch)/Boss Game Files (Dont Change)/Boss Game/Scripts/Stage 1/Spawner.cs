using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeNice
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject[] spawnItems;
        private int spawnIndex = 0;
        public float startDelay;
        public string spawnSFX;
        public float spawnSoundFreq;
        public float spawnDelay;
        // Start is called before the first frame update
        void Start()
        {
            if (spawnPoints.Length == 0)
            {
                spawnPoints = new Transform[transform.childCount];
                int index = 0;
                foreach (Transform e in transform)
                {
                    spawnPoints[index] = e;
                    index++;
                }
            }
            StartCoroutine(SpawnItem(startDelay));
        }

        private IEnumerator SpawnItem(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (Random.Range(0f, 1f) < spawnSoundFreq)
            {
                BossGameManager.Instance.PlaySound(spawnSFX);
            }
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spawnItems[spawnIndex], spawnPoint.position, spawnPoint.rotation, transform);
            StartCoroutine(SpawnItem(spawnDelay));

            spawnIndex++;
            if(spawnIndex >= spawnItems.Length)
            {
                spawnIndex = 0;
            }
        }
        public void StopSpawning()
        {
            StopAllCoroutines();
            foreach (Ingredient g in GameObject.FindObjectsOfType<Ingredient>())
            {
                Destroy(g.gameObject);
            }
        }
    }
}
