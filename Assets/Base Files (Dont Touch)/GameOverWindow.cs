using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private Animator loseAnim;
    [SerializeField] private Animator restaurant;
    private void Awake()
    {
        window.SetActive(false);
        MainGameManager.Instance.GameOver += GameLose;
    }

    private void GameLose()
    {
        window.SetActive(true);
        loseAnim.Play("lose-anim");
        //restaurant.Play("back-fade");
    }
    public void RestartButton()
    {
        window.SetActive(false);
        MainGameManager.Instance.RestartGame();
    }

    public void TitleButton()
    {
        window.SetActive(false);
        GameManager.Instance.LoadScene("TitleScreen");
    }

    private void OnDestroy()
    {
        MainGameManager.Instance.GameOver -= GameLose;
    }
}
