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

    private double startAudioTime;
    private SoundAsset currSong;

    private void Start() 
    {
        startAudioTime = AudioSettings.dspTime;    
    }
    
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

    /// <summary>
    /// Get the SoundAsset for the currently playing song
    /// </summary>
    /// <returns>The SoundAsset for the currently playing song if there is
    /// one, and null otherwise</returns>
    public SoundAsset getCurrSong()
    {
        return currSong;
    }

    private SoundAsset findSound (string soundName)
    {
        foreach (SoundAsset s in bossGame.sounds)
        {
            if (s.soundName == soundName)
            {
                return s;
            }
        }
        return null;
    }

    /// <summary>
    /// Play given song and stop playing current song. No fade but song will
    /// be played on beat according to given bpm.
    /// </summary>
    /// <param name="songName">Name of song to be played</param>
    /// <param name="bpm">The beats per minute of the given song</param>
    /// <returns>True if the song is played and false otherwise</returns>
    public bool PlaySong(string songName, int bpm)
    {
        SoundAsset song = findSound(songName);
        if (song == null)
        {
            return false;
        }

        StartCoroutine(fadeSong(song, bpm, false, 0, 0));

        return true;
    }

    /// <summary>
    /// Play the given song and stop playing the current song. Will play the 
    /// song on beat according to the given bpm and can choose to fade in
    /// and whether the song should start at the same time as the 
    /// currently playing song.
    /// </summary>
    /// <param name="songName">Name of the song to be played</param>
    /// <param name="bpm">The beats per minute of the song</param>
    /// <param name="shouldFade">Whether the song should fade in and the
    /// current song should fade out</param>
    /// <param name="fadeDuration">Duration of the fade</param>
    /// <param name="shouldMatchTime">Whether the song should start playing
    /// at the same clip time as the currently playing song</param>
    /// <param name="loops">Whether song should loop</param>
    /// <returns>True if the song is played and false otherwise</returns>
    public bool PlaySong(string songName, int bpm, bool shouldFade = false, float fadeDuration = 1f, 
            bool shouldMatchTime = false, bool loops = true)
    {
        // find song
        SoundAsset song = findSound(songName);
        if (song == null)
        {
            return false;
        }

        song.source.loop = loops;
        StartCoroutine(fadeSong(song, bpm, shouldFade, fadeDuration, -1));

        return true;
    }

    /// <summary>
    /// Play the given song and stop playing the current song. Will play the 
    /// song on beat according to the given bpm and can choose to fade in
    /// and at what timestamp the song will start playing.
    /// </summary>
    /// <param name="songName">Name of the song to be played</param>
    /// <param name="bpm">The beats per minute of the song</param>
    /// <param name="startTime">Whether the song should start playing
    /// at the same clip time as the currently playing song</param>
    /// <param name="shouldFade">Whether the song should fade in and the
    /// current song should fade out</param>
    /// <param name="fadeDuration">Duration of the fade</param>
    /// <param name="loops">Whether song should loop</param>
    /// <returns>True if the song is played and false otherwise</returns>
    public bool PlaySong(string songName, int bpm, float startTime = 0, bool shouldFade = false, 
            float fadeDuration = 1f, bool loops = true)
    {
        // find song
        SoundAsset song = findSound(songName);
        if (song == null)
        {
            return false;
        }

        song.source.loop = loops;
        StartCoroutine(fadeSong(song, bpm, shouldFade, fadeDuration, startTime));

        return true;
    }

    private IEnumerator fadeSong(SoundAsset song, int bpm, bool shouldFade, float fadeDuration, float startTime)
    {
        // perform music calculations
        double secPerBeat = 60f / bpm;
        double currentTime = AudioSettings.dspTime - startAudioTime;
        double currentBeat = currentTime / secPerBeat;

        float timeElapsed = 0;
        bool isPlaying = false;

        // Start playing song on the beat
        while (!isPlaying)
        {
            if (Mathf.Abs((float)(Mathf.Ceil((float)currentBeat) - (float)currentBeat)) > 0.01f)
            {
                float currSongTime = (currSong != null) ? currSong.source.time : 0;
                song.source.time = (startTime < 0) ? currSongTime : startTime;
                song.source.Play();
                if (shouldFade)
                    song.source.volume = 0;
                isPlaying = true;
            }

            currentTime = AudioSettings.dspTime - startAudioTime;
            currentBeat = currentTime / secPerBeat;

            yield return null;
        }

        // Start fade
        while (shouldFade && timeElapsed < fadeDuration)
        {
            if (currSong != null)
            {
                float currSongVol = Mathf.Lerp(currSong.volume, 0, timeElapsed / fadeDuration);
                currSong.source.volume = currSongVol;
            }
            float newSongVol = Mathf.Lerp(0, song.volume, timeElapsed / fadeDuration);
            song.source.volume = newSongVol;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        song.source.volume = song.volume;
        if (currSong != null)
        {
            currSong.source.volume = 0;
            currSong.source.Stop();
        }
        
        currSong = song;
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
        MainGameManager.instance.OnBossGameStart(bossGame);
    }
}
