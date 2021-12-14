using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILives : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        MainGameManager.OnMainStart += CheckLives;
    }

    private void CheckLives(bool win)
    {
        if(win) animator.Play("flowers-" + MainGameManager.Instance.remainingLives);
        else
        {
            animator.Play("flowers-" + (MainGameManager.Instance.remainingLives == 3 ? 3 : MainGameManager.Instance.remainingLives + 1));
            StartCoroutine(LoseLifeHelper());
        }
    }

    private IEnumerator LoseLifeHelper()
    {
        yield return new WaitForSeconds(MainGameManager.ShortTime/4);
        animator.Play("flowers-die-" + MainGameManager.Instance.remainingLives); 
    }

    private void OnDestroy()
    {
        MainGameManager.OnMainStart -= CheckLives;
    }
}
