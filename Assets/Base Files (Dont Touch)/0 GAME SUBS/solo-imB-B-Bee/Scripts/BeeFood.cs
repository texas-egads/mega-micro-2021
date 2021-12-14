using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFood : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite bacon;
    public Sprite egg;
    public Sprite toast;
    public Sprite honeybutterchickenbiscuit;
    public Sprite happbee;

    private void Awake()
    {
        int randNum = Random.Range(0, 4);
        switch (randNum)
        {
            case 0:
                spriteRenderer.sprite = bacon;
                break;
            case 1:
                spriteRenderer.sprite = egg;
                break;
            case 2:
                spriteRenderer.sprite = toast;
                break;
            case 3:
                spriteRenderer.sprite = honeybutterchickenbiscuit;
                break;
        }
    }

    private void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Object 2")
        {
            other.GetComponent<SpriteRenderer>().sprite = happbee;
            MinigameManager.Instance.PlaySound("Crunch");
            Destroy(gameObject);
        }

        if (other.tag == "Object 4")
        {
            Destroy(gameObject);
        }
    }
}
