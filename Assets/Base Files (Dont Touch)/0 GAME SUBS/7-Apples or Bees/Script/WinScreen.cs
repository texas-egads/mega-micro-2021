using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    AudioSource MyAudio;
    // Start is called before the first frame update
    void Start()
    {
        MyAudio = GetComponent<AudioSource>();
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MinigameManager.Instance.minigame.gameWin)
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
