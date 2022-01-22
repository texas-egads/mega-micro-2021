using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace SecretPuddle {
    public class BatterySpawner : MonoBehaviour
    {
        public Vector2 spawnArea;
        public Vector2 spawnAreaOffset;
        public int initialBatteryCount;
        public GameObject batteryObj;
        public GameObject bombObj;

        private List<GameObject> batteries = new List<GameObject>();

        void Start()
        {
            // Spawn all the batteries in random positins and with random
            // rotations
            for(int i = 0; i < initialBatteryCount; i++){
                GameObject obj = (i % 6 != 0) ? Instantiate(batteryObj, randSpawnPos(), randSpawnRot()) 
                    : Instantiate(bombObj, randSpawnPos(), randSpawnRot());
                obj.transform.parent = transform;
                batteries.Add(obj);
                
                //Adjust scale
                Vector3 newScale = obj.transform.localScale;
                float proportionToTop = (obj.transform.position.y - spawnAreaOffset.y + spawnArea.y / 2) / spawnArea.y;
                newScale /= 1 + proportionToTop * .5f;
                obj.transform.localScale = newScale;
                
                obj.GetComponent<SpriteRenderer>().sortingOrder = 100 - (int)(obj.transform.position.y * 100);
            }
        }

        /// <summary>
        /// Get a random a battery from the pile of batteries
        /// </summary>
        /// <returns>A GameObject which is a battery. Returns null if
        /// no batteries are available</returns>
        public GameObject getRandomBattery(){
            if (batteries.Count == 0){
                return null;
            }

            int index = Random.Range(0, batteries.Count);
            return batteries[index];
        }

        /// <summary>
        /// Remove the given battery from the list of available batteries.
        /// </summary>
        /// <param name="battery">Battery to be removed from the list</param>
        public void removeBattery(GameObject battery){
            batteries.Remove(battery);
        }

        public void spawnBatteries(int num){
            for(int i = 0; i < num; i++){
                GameObject obj = (i % 5 == 0) ? Instantiate(batteryObj, randSpawnPos(), randSpawnRot()) 
                    : Instantiate(bombObj, randSpawnPos(), randSpawnRot());
                obj.transform.parent = transform;
                batteries.Add(obj);
                obj.GetComponent<SpriteRenderer>().sortingOrder = i + initialBatteryCount;
            }
            initialBatteryCount += num;
        }

        /// <summary>
        /// Generate a random position within the bounds of the spawn area.
        /// </summary>
        /// <returns>A vector 2 which represents a position within the
        /// bounds of the spawn area</returns>
        private Vector2 randSpawnPos(){
            Vector2 pos = new Vector2();

            // generate random local position within spawn area
            pos.x = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
            pos.y = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);

            // conver to global position
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            pos += spawnAreaOffset;
            
            return pos;
        }

        /// <summary>
        /// Return a random rotation
        /// </summary>
        /// <returns>Quaternion representing a random rotation</returns>
        private Quaternion randSpawnRot(){
            return Quaternion.Euler(0, 0, Random.Range(0, 360f));
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, spawnArea);
        }
    }
}
