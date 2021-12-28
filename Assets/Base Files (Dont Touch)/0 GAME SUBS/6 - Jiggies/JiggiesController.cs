using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggiesController : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpPow = 400;
    public float maxUp = 0;
    public float moveSpeed = 3;
    public Minigame minigame;
    private bool control = true;

    public Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = true;
        minigame.gameWin = true;
    }

    // Update is called once per frame
    void Update()
    {

        position = rb.position;
        if (control == true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) { if (rb.velocity.y <= maxUp) { rb.AddForce(new Vector2(0, jumpPow)); MinigameManager.Instance.PlaySound("Jump"); } else { rb.AddForce(new Vector2(0, jumpPow / 4)); MinigameManager.Instance.PlaySound("Jump"); } }

            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            if (Input.GetAxisRaw("Horizontal") == -1) { GetComponent<SpriteRenderer>().flipX = true; } else { GetComponent<SpriteRenderer>().flipX = false; }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        minigame.gameWin = false;
        control = false;
        MinigameManager.Instance.PlaySound("Hit");
        GetComponent<Collider2D>().enabled = false;
    }
}
