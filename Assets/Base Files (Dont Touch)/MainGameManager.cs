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
    public bool firstMinibossTry;
    [HideInInspector] public bool gameOver;

    [SerializeField] private Canvas boarderCanvas;

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
        firstMinibossTry = true;
        isMiniBossOver = false;
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

        if (!FindObjectOfType<AudioManager>()._source.isPlaying)
            FindObjectOfType<AudioManager>()._source.Play();

        yield return new WaitForSeconds(ShortTime/2 - halfBeat - .21f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText(sceneName);
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        GameStart();
        boarderCanvas.enabled = true;
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
        // Hardcoded start to miniboss at midpoint of game
        if (roundNumber == (roundsToWin + 1) / 2)
        {
            StartCoroutine(WaitForMiniBossEnd(minigame));
        }
        else
        {
            TimerManager.Instance.StartTimer(minigame.gameTime);
            var waitTime = minigame.gameTime == Minigame.GameTime.Short ? ShortTime : LongTime;
            StartCoroutine(WaitForMinigameEnd(minigame, waitTime));
        }
       
    }

    public bool isMiniBossOver = false;

    private IEnumerator WaitForMiniBossEnd(Minigame miniBoss)
    {
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync("Main");
        scene.allowSceneActivation = false;
        while (!isMiniBossOver) yield return null;

        LevelPreview.instance.HandleLevelPreview(false);
        yield return new WaitForSeconds(halfBeat);

        if (!miniBoss.gameWin) 
            remainingLives -= 1;
        else
            roundNumber++;
       
        firstMinibossTry = false;
        scene.allowSceneActivation = true;
        yield return null;
        MainStart(miniBoss.gameWin);
        if (remainingLives == 0)
        {
            gameOver = true;
            Invoke(nameof(OnGameOver), 0 /*ShortTime/2*/);
        }
        else if (!miniBoss.gameWin)
        {
            StartCoroutine(LoadMiniBoss());
        }
        else
        {
            StartCoroutine(LoadNextGame());
        }
    }

    private IEnumerator WaitForMinigameEnd(Minigame minigame, float time)
    {
        yield return new WaitForSeconds(.1f);

        AsyncOperation scene = SceneManager.LoadSceneAsync("Main");
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(time - .1f - halfBeat);
        
        LevelPreview.instance.HandleLevelPreview(false);
        yield return new WaitForSeconds(halfBeat);
        
        if (!minigame.gameWin) {
            remainingLives -= 1;
            // play smoke anim
        }
        roundNumber++;
        scene.allowSceneActivation = true;
        yield return null;
        MainStart(minigame.gameWin);
        if (remainingLives == 0)
        {
            //yield return null;
            gameOver = true;
            Invoke(nameof(OnGameOver), 0 /*ShortTime/2*/);
        }
        else if (roundNumber == (roundsToWin + 1) / 2)
        {
            //Load mini boss. Ideally handle it in the same way as the boss is handled 
            //with a BossGameManager and variables that control the if the player won and 
            //when the game is over. These variables are called gameWin and gameOver.

            //Potential solutions are to make miniboss counterparts to the LoadBossGame,
            //OnBossGameStart, and WaitForBossGameEnd functions. This would be better than setting 
            //a new minigame timer length since it would allow for premature minigame fail that ends the minigame.

            StartCoroutine(LoadMiniBoss());
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
    public int miniBossSceneIndex;
    
    private IEnumerator LoadMiniBoss()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(miniBossSceneIndex);
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(ShortTime / 2 - .1f);

        // miniboss alert
        if (firstMinibossTry)
        {
            BossAlertHandler alertHandler = FindObjectOfType<BossAlertHandler>();
            alertHandler.gameObject.GetComponent<Text>().text = "MINIBOSS TIME!";
            alertHandler.BossAlert();
            yield return new WaitForSeconds(ShortTime);
            FindObjectOfType<AudioManager>().PlayReady();
        }

        OnNextGameWait();
        yield return new WaitForSeconds(ShortTime / 2 - halfBeat - .21f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText(NameFromIndex(miniBossSceneIndex));
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        GameStart();

        // disable boarder for miniboss 
        boarderCanvas.enabled = false;

        LevelPreview.instance.HandleLevelPreview(true);

        // TODO: see where the boss game listens for the game to end and where it waits for boss end
    }


    private IEnumerator LoadBossGame()
    {
        yield return new WaitForSeconds(.1f);


        yield return new WaitForSeconds(ShortTime/2 - .15f); //matching the same amount of wait as the block below
        yield return new WaitForSeconds(ShortTime/2 - halfBeat - .21f);
        yield return new WaitForSeconds(.21f);
        //SceneManager.LoadScene("End");  //TODO replace with block below to allow for proper boss battle

        AsyncOperation scene = SceneManager.LoadSceneAsync(bossSceneIndex);
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(ShortTime/2 - .15f);
        if (firstBossTry)
        {
            BossAlertHandler alertHandler = FindObjectOfType<BossAlertHandler>();
            alertHandler.gameObject.GetComponent<Text>().text = "BOSS TIME!";
            alertHandler.BossAlert();
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

        // disable boarder for miniboss 
        boarderCanvas.enabled = false;

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
            Invoke(nameof(OnGameOver), 0 /*ShortTime/2*/);
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
