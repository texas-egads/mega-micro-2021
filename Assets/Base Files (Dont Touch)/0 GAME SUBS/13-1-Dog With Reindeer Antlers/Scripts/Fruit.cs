using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogWithReindeerAntlers
{
    public class Fruit : MonoBehaviour
    {
        public LiquidType fruitType;
        public GameObject liquidPrefab;

        public int juiceDrops = 1;

        public float maxDropSize = 1;
        public float minDropSize = .5f;

        public float defaultDrag = .1f;

        public float liquidDrag = 10f;

        bool inLiquid = false;

        Vector2 liquidVelocity = Vector2.zero;

        Rigidbody2D fruitRB;


        Transform liquidRendrer;

        GameObject liquidSpawn;

        void Start()
        {
            liquidRendrer = GameObject.FindGameObjectWithTag("GameController").transform;
            fruitRB = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (inLiquid)
            {
                fruitRB.drag = liquidDrag;
                fruitRB.angularDrag = liquidDrag;
            }
            else
            {
                fruitRB.drag = defaultDrag;
                fruitRB.angularDrag = defaultDrag;
            }

            inLiquid = false;
        }

        public void SpawnJuice()
        {
            for (int i = 0; i < juiceDrops; i++)
            {
                Vector3 randomSpawn = transform.position + (Quaternion.AngleAxis(Random.Range(0f, 359f), Vector3.forward) * (Vector3.up * Random.Range(0f, .5f)));
                Vector3 spawnPosition = new Vector3(randomSpawn.x, randomSpawn.y, 0);
                liquidSpawn = Instantiate(liquidPrefab, spawnPosition, Quaternion.identity, liquidRendrer);
                liquidSpawn.GetComponent<LiquidDrop>().startingType = fruitType;
                liquidSpawn.GetComponent<LiquidDrop>().scale = Random.Range(minDropSize, maxDropSize);
            }

            Destroy(gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Liquid")
            {
                inLiquid = true;
                fruitRB.AddForceAtPosition(collision.attachedRigidbody.velocity, collision.transform.position);

                collision.attachedRigidbody.velocity *= .95f;
            }
        }
    }
}
