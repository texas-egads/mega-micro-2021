using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggiesSpawner : MonoBehaviour
{
    public float spawnTimer;
    public float moveSpeed;
    private float timer;

    public GameObject[] Asteroids;
    public GameObject Player;
    Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        playerRb = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnTimer) { timer += Time.deltaTime; } 
        else { timer = 0;
            Vector2 spawnPos;
            spawnPos = playerRb.position + new Vector2(Random.Range(0, 20), 10 + -20 * Random.Range(0, 2));
            GameObject newAsteroid = Instantiate(Asteroids[Random.Range(0, 3)], spawnPos, transform.rotation);
            newAsteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(playerRb.position.x - spawnPos.x, playerRb.position.y - spawnPos.y).normalized * moveSpeed *Random.Range(1f,1.5f);
            }
    }
}
