using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.GetComponentInChildren<AudioManager>().VictoryScreenMusic();
    }

    public void LoadTitle()
    {
        GameManager.Instance.LoadScene("TitleScreen");
    }
}
