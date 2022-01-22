using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float platformSpacing = 2.33333f;
        [SerializeField] private float spawnDelay = 1f;
        [SerializeField] private float multipleSpawnChance = .2f;
        private int maxMultSpawns = 3;
        
        [Range(0, 1)]
        public float blockSpawnChance;

        public GameObject enemyObj;
        public GameObject blockObj;
        
        private List<Vector3> fromList = new List<Vector3>();
        private List<Vector3> toList = new List<Vector3>();
        private bool flipActiveList = true;
        private bool shouldSpawn = true;

        private void Start() 
        {
            Vector3 platSpacing = new Vector3(0, platformSpacing, 0);
            fromList.Add(transform.position - platSpacing);
            fromList.Add(transform.position);
            fromList.Add(transform.position + platSpacing);
            
            Stage1.instance.gameLost.AddListener(stopSpawning);

            StartCoroutine(startSpawning());
        }

        private void stopSpawning()
        {
            shouldSpawn = false;
        }


        /// <summary>
        /// Continuously spawn enemies and blocks in random platforms
        /// </summary>
        private IEnumerator startSpawning()
        {
            while(shouldSpawn)
            {
                List<Vector3> activeList = (flipActiveList) ? fromList : toList;
                List<Vector3> inactiveList = (flipActiveList) ? toList : fromList;
                
                // determine random spawn spot
                int index = Random.Range(0, activeList.Count);
                Vector3 spawnPos = activeList[index];

                // move chosen spot to inactive list
                inactiveList.Add(spawnPos);
                activeList.RemoveAt(index);

                // determine if enemy or block should be spawned
                float enemyType = Random.Range(0, 1f);
                if(enemyType <= blockSpawnChance)
                {
                    GameObject obj = Instantiate(blockObj, spawnPos, Quaternion.identity);
                    obj.transform.parent = transform;
                }
                else
                {
                    GameObject obj = Instantiate(enemyObj, spawnPos, Quaternion.identity);
                    obj.transform.parent = transform;

                    float multipleCheck = Random.Range(0, 1f);
                    int multSpawns = 0;
                    while (multipleCheck < multipleSpawnChance && multSpawns <= maxMultSpawns)
                    {
                        yield return new WaitForSeconds(.2f);
                        multSpawns++;
                        GameObject multObj = Instantiate(enemyObj, spawnPos, Quaternion.identity);
                        multObj.transform.parent = transform;
                        multipleCheck = Random.Range(0, 1f);
                    }
                }

                // flip which list is active if the current active list is empty
                if(activeList.Count == 0)
                {
                    flipActiveList = !flipActiveList;
                }

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
