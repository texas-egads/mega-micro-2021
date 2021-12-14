using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterHandler : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        MainGameManager.Instance.NextGameWait += WaiterIn;
        MainGameManager.OnMainStart += WaiterOut;
    }

    private void WaiterIn()
    {
        StartCoroutine(WaiterInHelper());
    }

    private IEnumerator WaiterInHelper()
    {
        yield return new WaitForSeconds(.28f);
        _animator.Play("waiter-in");
    }

    private void WaiterOut(bool win)
    {
        StartCoroutine(WaiterOutHelper());
    }

    private IEnumerator WaiterOutHelper()
    {
        yield return new WaitForSeconds(.4f);
        _animator.Play("waiter-out");
    }
    private void OnDestroy()
    {
        MainGameManager.Instance.NextGameWait -= WaiterIn;
        MainGameManager.OnMainStart -= WaiterOut;
    }
}
