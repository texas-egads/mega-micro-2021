using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGaDSChan : MonoBehaviour
{
    public float speed = 5;

    private float dirX;

    public bool faceRight = true;

    Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite panUp;
    public Sprite panDown;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * speed;

        if (dirX > 0 && !faceRight)
        {
            Flip();
        } else if (dirX < 0 && faceRight)
        {
            Flip();
        }

        rb.velocity = new Vector3(dirX, 0f, 0f);

        if (Input.GetKeyDown("space"))
        {
            spriteRenderer.sprite = panUp;
            MinigameManager.Instance.PlaySound("SlideWhistle");
        }
        if (Input.GetKeyUp("space"))
        {
            spriteRenderer.sprite = panDown;
        }
    }
    
    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
