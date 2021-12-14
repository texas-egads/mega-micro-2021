using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Animator container;
    private void Start()
    {
        FindObjectOfType<AudioManager>().CutsceneMusic();
        StartCoroutine(WaitLoadMain());
    }

    private IEnumerator WaitLoadMain()
    {
        yield return new WaitForSeconds(7.56f);
        container.Play("camera-intense");
        yield return new WaitForSeconds(12.23f - 7.56f);
        LoadMain();
    }
    
    public void LoadMain()
    {
        GameManager.Instance.LoadScene("Main");
    }
}
