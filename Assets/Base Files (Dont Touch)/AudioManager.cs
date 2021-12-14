using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [Serializable] public struct Music
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume;
    }
    [SerializeField] private Music readyMusic;
    [SerializeField] private Music successMusic;
    [SerializeField] private Music failureMusic;
    [SerializeField] private Music gameOverMusic;
    [SerializeField] private Music startMusic;
    [SerializeField] private Music cutsceneMusic;
    [SerializeField] private Music title;
    
    public AudioSource _source;

    private void Awake()
    {
        _source = gameObject.AddComponent<AudioSource>();
        //_source.clip = music[Random.Range(0, music.Length-1)];
        GameManager.Instance.titleScreenListener.AddListener(TitleMusic);
        MainGameManager.Instance.FirstMainStart += IntroMusic;
        MainGameManager.OnMainStart += StartMusic;
        MainGameManager.Instance.GameOver += LoseMusic;
        _source.Play();
    }

    private void TitleMusic()
    {
        _source.loop = true;
        PlayMusic(title);
    }

    public void CutsceneMusic()
    {
        PlayMusic(cutsceneMusic);
    }

    private void IntroMusic()
    {
        //PlayMusic(startMusic);
        _source.loop = false;
        StartCoroutine(IntroMusicSequence());
    }

    private IEnumerator IntroMusicSequence()
    {
        _source.loop = false;
        PlayMusic(startMusic);
        yield return new WaitForSeconds(MainGameManager.ShortTime * 2 + MainGameManager.halfBeat);
        StartCoroutine(FadeMusic());
    }
    
    private void StartMusic(bool win)
    {
        _source.loop = false;
        if (MainGameManager.Instance.gameWin) return;
        StartCoroutine(MainMusicStart(win));
    }

    private IEnumerator MainMusicStart(bool win)
    {
        PlayMusic(win ? successMusic : failureMusic);
        /*yield return new WaitForSeconds(MainGameManager.ShortTime / 2);
        if (MainGameManager.Instance.gameOver) yield break;
        PlayMusic(readyMusic);
        yield return new WaitForSeconds(MainGameManager.ShortTime / 2);*/
        yield return new WaitForSeconds(MainGameManager.ShortTime);
        StartCoroutine(FadeMusic());
    }

    private void LoseMusic()
    {
        _source.loop = false;
        PlayMusic(gameOverMusic);
    }

    private IEnumerator FadeMusic()
    {
        if(MainGameManager.Instance.gameOver) yield break;
        while (_source.volume > .1f)
        {
            _source.volume -= .05f;
            yield return new WaitForSeconds(.02f);
        }
        _source.Stop();
    }

    public void PlayMusic(Music m)
    {
        _source.volume = m.volume;
        _source.clip = m.clip;
        _source.Play();
    }

    public void PlayReady()
    {
        PlayMusic(readyMusic);
    }

    private void OnDestroy()
    {
        MainGameManager.Instance.FirstMainStart -= IntroMusic;
        MainGameManager.OnMainStart -= StartMusic;
        MainGameManager.Instance.GameOver -= LoseMusic;
    }
}
