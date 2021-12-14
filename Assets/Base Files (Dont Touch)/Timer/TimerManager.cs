using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StartTimer(Minigame.GameTime time)
    {
        transform.GetChild(0).GetComponent<Animator>().Play(time == Minigame.GameTime.Short ? "short-timer" : "long-timer");
    }

    
}
