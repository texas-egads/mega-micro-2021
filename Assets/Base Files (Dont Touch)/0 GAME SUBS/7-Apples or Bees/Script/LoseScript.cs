using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && !MinigameManager.Instance.minigame.gameWin)
        {
            StartCoroutine(AttemptCoroutine());
        }
    }

    IEnumerator AttemptCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}