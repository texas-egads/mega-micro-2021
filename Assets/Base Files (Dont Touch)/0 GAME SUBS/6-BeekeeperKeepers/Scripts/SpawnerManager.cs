using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeekeeperKeepers {
    public class SpawnerManager : MonoBehaviour {
        private const float BEAT_TIME = .42857f; //thanks Grant

        public GameObject ingPrefab;


        private ListManager listManager;
        // Start is called before the first frame update
        void Start() {
            listManager = FindObjectOfType<ListManager>();
            if (transform.childCount != listManager.GetDropCount()) {
                Debug.LogError("Mismatch between dropping objects and available spawners");
            }
            else {
                StartCoroutine(DropIngredients());
            }
        }

        private IEnumerator DropIngredients() {
            float numDropped = 1.0f;
            while (transform.childCount > 0) {
                yield return new WaitForSeconds(BEAT_TIME * 1.5f);
                Transform spawner = transform.GetChild(Random.Range(0, transform.childCount));

                // Create new position for ingredient such that they overlay in order as dropped
                Vector3 ingPos = new Vector3(spawner.position.x, spawner.position.y, -numDropped);

                // creates a new ingredient object from the prefab, then gets its Ingredient component
                Ingredient ing = Instantiate(ingPrefab, ingPos, spawner.rotation).GetComponent<Ingredient>();

                ing.Initialize(listManager.NextDrop(), this);

                spawner.SetParent(null); // actually decrements childCount
                Destroy(spawner.gameObject); // F

                numDropped++;
                // Debug.LogWarning("Child count: " + transform.childCount);
            }
        }
    }
}