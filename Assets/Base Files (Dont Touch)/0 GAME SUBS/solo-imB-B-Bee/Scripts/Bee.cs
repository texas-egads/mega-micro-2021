using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public GameObject hearts;
    public SpriteRenderer spriteRenderer;
    public Sprite happbee;
    public bool happy = false;
    public Animation beefloat;

    // Start is called before the first frame update
    void Start()
    {
        hearts.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.sprite == happbee)
        {
            happy = true;
            
        }

        PlayHearts();
    }

    void PlayHearts()
    {
        if (happy && (hearts != null))
        {
            hearts.SetActive(true);
        }
    }

    public bool GetHappy()
    {
        return happy;
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Object3")
        {
            spriteRenderer.sprite = happbee;
            happy = true;
            Destroy(other);
            Debug.Log("it wokrd");
        }
    }
    */
}
