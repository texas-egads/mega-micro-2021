using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager _instance;
    public static MainGameManager Instance => _instance ? _instance : FindObjectOfType<MainGameManager>();

    public event Action GrowMainScene;
    public void OnGrowMainScene() { GrowMainScene?.Invoke(); }

    public event Action FirstMainStart;
    private void OnFirstMainStart() { FirstMainStart?.Invoke(); }
    public static event Action<bool> OnMainStart;
    private static void MainStart(bool win) { OnMainStart?.Invoke(win); }
    public static event Action OnGameStart;
    private static void GameStart() { OnGameStart?.Invoke(); }

    public event Action NextGameWait;
    private void OnNextGameWait() { NextGameWait?.Invoke(); }
    
    public event Action GameOver;
    public void OnGameOver() { GameOver?.Invoke(); }
    
    [SerializeField] private Text impactText;
    [SerializeField] private GameObject foodSilhouette;
    [HideInInspector] public int roundNumber;


    private const int StartingLives = 3;
    [HideInInspector] public int remainingLives;
    private int numberOfGames;
    private List<int> _remainingGames;

    public const float ShortTime = 3.4286f;
    private const float LongTime = 6.8571f;
    public const float halfBeat = .23333f;

    public float startWaitTime;
    public int indexOffset;
    [SerializeField] private int roundsToWin;

    public void SetRounds(int r) { roundsToWin = r; }

    [SerializeField] private bool debugBossMode;
    [SerializeField] private bool testMode;
    public bool firstBossTry;
    [HideInInspector] public bool gameOver;

    private void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(gameObject);
        numberOfGames = SceneManager.sceneCountInBuildSettings - indexOffset;
        GameManager.Instance.mainGameListener.AddListener(StartGame);
    }
    private void StartGame()
    {
        remainingLives = StartingLives;
        roundNumber = 1;
        _remainingGames = new List<int>();
        firstBossTry = true;
        gameOver = false;
        gameWin = false;
        if (debugBossMode) StartCoroutine(LoadBossGame());
        else
        {
            for(int i = 0; i < numberOfGames; i++) _remainingGames.Add(i + indexOffset);
            StartCoroutine(LoadFirstGame());
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScreen");
        SceneManager.LoadScene("Main");
        StartGame();
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        print(name.Substring(0, dot));
        return name.Substring(0, dot);
    }
    
    struct GameInfo
    {
       public string name;
       public int id;
    }

    private IEnumerator LoadFirstGame()
    {
        yield return null;
        OnFirstMainStart();
        yield return new WaitForSeconds(ShortTime + halfBeat);//TODO: Change to start wait time
        StartCoroutine(LoadNextGame());
    }

    private int _currentFood;
    public int currentFood
    {
        get => _currentFood;
        set => _currentFood = value;
    }

    private IEnumerator LoadNextGame()
    {
        yield return new WaitForSeconds(.1f);
        var nextGame = GetNextGame();
        var sceneName = nextGame.name;
        AsyncOperation scene = SceneManager.LoadSceneAsync(nextGame.id);
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(ShortTime/2 -.1f);
        OnNextGameWait();
        yield return new WaitForSeconds(ShortTime/2 - halfBeat - .21f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText(sceneName);
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        GameStart();
        LevelPreview.instance.HandleLevelPreview(true);
        
    }


    private GameInfo GetNextGame()
    {
        GameInfo game = new GameInfo();
        //print(_remainingGames.Count);
        var gameIndex = Random.Range(0, _remainingGames.Count);
        game.id = _remainingGames[gameIndex];
        game.name = NameFromIndex(game.id);
        if(!testMode)_remainingGames.RemoveAt(gameIndex);
        return game;
    }
    

    public void OnMinigameStart(Minigame minigame)
    {
        TimerManager.Instance.StartTimer(minigame.gameTime);
        var waitTime = minigame.gameTime == Minigame.GameTime.Short ? ShortTime : LongTime;
        StartCoroutine(WaitForMinigameEnd(minigame, waitTime));
    }

    private IEnumerator WaitForMinigameEnd(Minigame minigame, float time)
    {
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync("Main");
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(time - .1f - halfBeat);
        
        LevelPreview.instance.HandleLevelPreview(false);
        yield return new WaitForSeconds(halfBeat);
        
        if (!minigame.gameWin) remainingLives -= 1;
        roundNumber++;
        scene.allowSceneActivation = true;
        yield return null;
        MainStart(minigame.gameWin);
        if (remainingLives == 0)
        {
            //yield return null;
            gameOver = true;
            Invoke(nameof(OnGameOver), ShortTime/2);
        }
        else if (roundNumber <= roundsToWin)
        {
            
            StartCoroutine(LoadNextGame());
        }
        else
        {
            StartCoroutine(LoadBossGame());
        }
    }
    public int bossSceneIndex;
    
    private IEnumerator LoadBossGame()
    {
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync(bossSceneIndex);
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(ShortTime/2 - .15f);
        if (firstBossTry)
        {
            
            FindObjectOfType<BossAlertHandler>().BossAlert();
            yield return new WaitForSeconds(ShortTime);
            FindObjectOfType<AudioManager>().PlayReady();
        }
        OnNextGameWait();

        yield return new WaitForSeconds(ShortTime/2 - halfBeat - .21f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText(NameFromIndex(bossSceneIndex));
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        GameStart();
        LevelPreview.instance.HandleLevelPreview(true);
        
    }
    public void OnBossGameStart(BossGame bossGame)
    {
        StartCoroutine(WaitForBossGameEnd(bossGame));
    }

    public bool gameWin;
    private IEnumerator WaitForBossGameEnd(BossGame bossGame)
    {
        bossGame.gameOver = false;
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync("Main");
        scene.allowSceneActivation = false;
        while (!bossGame.gameOver) yield return null;

        LevelPreview.instance.HandleLevelPreview(false);
        yield return new WaitForSeconds(halfBeat);
        
        if (!bossGame.gameWin) remainingLives -= 1;
        firstBossTry = false;
        scene.allowSceneActivation = true;
        yield return null;
        gameWin = bossGame.gameWin;
        MainStart(bossGame.gameWin);
        if (remainingLives == 0)
        {
            gameOver = true;
            Invoke(nameof(OnGameOver), ShortTime/2);
        }
        else if (bossGame.gameWin)
        {
            SceneManager.LoadScene("End");
        }
        else
        {
            StartCoroutine(LoadBossGame());
        }
    }
}
