using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundText : MonoBehaviour
{
    [SerializeField] private Text roundText;

    private void Start()
    {
        StartCoroutine(SetRound(MainGameManager.Instance.roundNumber));
        roundText.text = "Round " + MainGameManager.Instance.roundNumber;
        MainGameManager.Instance.GameOver += GameLose;
    }

    private IEnumerator SetRound(int round)
    {
        roundText.text = "Round " + (round == 1 ? 1 : round - 1);
        yield return new WaitForSeconds(MainGameManager.ShortTime/2);
        roundText.text = "Round " + round;
    }

    private void GameLose()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        MainGameManager.Instance.GameOver -= GameLose;
    }
}
