using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameManager : MonoBehaviour
{
    [SerializeField] private bool debugGameOnly;
    
    public BossGame bossGame;
    
    private static BossGameManager _instance;
    public static BossGameManager Instance => _instance ? _instance : _instance = FindObjectOfType<BossGameManager>();
    
    public void PlaySound(string soundName)
    {
        foreach (var s in bossGame.sounds)
        {
            if (s.soundName == soundName)
            {
                s.source.pitch = Random.Range(s.minPitch, s.maxPitch);
                s.source.Play();
            }
        }
    }
    private void Awake()
    {
        bossGame.gameWin = false;
        if (!debugGameOnly && GameManager.Instance == null)
        {
            debugGameOnly = true;
            SceneManager.LoadScene("Main");
        }
        else
        {
            foreach (var s in bossGame.sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
            }
            if(!debugGameOnly) StartCoroutine(GameDelayedStart());
        }
    }
    
    private IEnumerator GameDelayedStart()
    {
        yield return new WaitForSeconds(.2333f);
        MainGameManager.Instance.OnBossGameStart(bossGame);
    }
}
