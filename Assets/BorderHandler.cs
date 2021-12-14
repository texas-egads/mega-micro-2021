using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderHandler : MonoBehaviour
{
    [SerializeField] private GameObject border;

    private void Awake()
    {
        MainGameManager.OnGameStart += BorderOn;
        MainGameManager.OnMainStart += BorderOff;
    }

    private void BorderOn()
    {
        border.SetActive(true);
    }

    private void BorderOff(bool win)
    {
        border.SetActive(false);
    }

    private void OnDestroy()
    {
        MainGameManager.OnGameStart -= BorderOn;
        MainGameManager.OnMainStart -= BorderOff;
    }
}
