using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        MainGameManager.OnMainStart += BeeAnimation;
        MainGameManager.Instance.FirstMainStart += PreFirstGameAnim;
    }

    private void PreFirstGameAnim()
    {
        animator.Play("pre-first-game");
    }

    private void BeeAnimation(bool win)
    {
        animator.Play(win ? "bee-win" : "bee-lose");
    }

    private void OnDestroy()
    {
        MainGameManager.OnMainStart -= BeeAnimation;
        MainGameManager.Instance.FirstMainStart -= PreFirstGameAnim;
    }
}
