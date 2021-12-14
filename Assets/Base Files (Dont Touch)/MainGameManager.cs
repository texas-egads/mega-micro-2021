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
    public enum MainGameState
    {
        Main, Game
    }
    
    public MainGameState gameState
    {
        get => _mainGameState;
        set
        {
            _mainGameState = value;
            switch (value)
            {
                case MainGameState.Main:
                    mainListener.Invoke();
                    break;
                case MainGameState.Game:
                    gameListener.Invoke();
                    break;
            }
        }
    }

    public UnityEvent mainListener = new UnityEvent();
    public UnityEvent gameListener = new UnityEvent();

    public event Action GrowMainScene;
    public void OnGrowMainScene()
    {
        if(GrowMainScene != null) GrowMainScene.Invoke();
    }
    public event Action ShrinkMainScene;
    public void OnShrinkMainScene()
    {
        if(ShrinkMainScene != null) ShrinkMainScene.Invoke();
    }

    public event Action<bool> MainStart;
    public void OnMainStart(bool win)
    {
        if(MainStart != null) MainStart.Invoke(win);
    }
    
    public event Action GameOver;
    public void OnGameOver()
    {
        if(GameOver != null) GameOver.Invoke();
    }


    private MainGameState _mainGameState;
    
    [SerializeField] private Text impactText;
    [SerializeField] private GameObject foodSilhouette;
    [HideInInspector] public int roundNumber;


    private const int StartingLives = 3;
    [HideInInspector] public int remainingLives;
    private int numberOfGames;
    private List<string> _remainingGames;

    public const float ShortTime = 3.4286f;
    private const float LongTime = 6.8571f;
    public const float halfBeat = .23333f;

    public float startWaitTime;
    public int indexOffset;
    [SerializeField] private int roundsToWin;
    [SerializeField] private Image gameBorder;
    [SerializeField] private bool debugBossMode;

    private void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(gameObject);
        numberOfGames = SceneManager.sceneCountInBuildSettings - indexOffset;
        GameManager.Instance.mainGameListener.AddListener(StartGame);
    }
    public void StartGame()
    {
        remainingLives = StartingLives;
        roundNumber = 1;
        _remainingGames = new List<string>();
        for(int i = 0; i < numberOfGames; i++) _remainingGames.Add(NameFromIndex(i+indexOffset));
        gameBorder.enabled = false;
        if (debugBossMode) StartCoroutine(LoadBossGame());
        else
        {
            for(int i = 0; i < numberOfGames; i++) _remainingGames.Add(NameFromIndex(i+indexOffset));
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
        OnMainStart(true); // TODO: replace with start music
        yield return new WaitForSeconds(0);//TODO: Change to start wait time
        StartCoroutine(LoadNextGame());
    }

    
    private IEnumerator LoadNextGame()
    {
        yield return new WaitForSeconds(.1f);
        var nextGame = GetNextGame();
        var sceneName = nextGame.name;
        AsyncOperation scene = SceneManager.LoadSceneAsync(nextGame.id);
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(ShortTime - halfBeat - .31f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText(sceneName);
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        gameBorder.enabled = true;
        LevelPreview.instance.HandleLevelPreview(true);
        
    }


    private GameInfo GetNextGame()
    {
        GameInfo game = new GameInfo();
        game.id = Random.Range(0, _remainingGames.Count);
        game.name = _remainingGames[game.id];
        game.id += indexOffset;
        //remainingGames.Remove(game);
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
        gameBorder.enabled = false;
        scene.allowSceneActivation = true;
        yield return null;
        OnShrinkMainScene();
        if (remainingLives == 0)
        {
            yield return null;
            OnGameOver();
        }
        else if (roundNumber <= roundsToWin)
        {
            OnMainStart(minigame.gameWin);
            StartCoroutine(LoadNextGame());
        }
        else
        {
            GameManager.Instance.LoadScene("End");
        }
    }
    public int bossSceneIndex;
    private IEnumerator LoadBossGame()
    {
        OnMainStart(true); // TODO: Replace with boss load music
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync(bossSceneIndex);
        //TODO: Change this to a boss sequence:
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(ShortTime - halfBeat - .31f);
        OnGrowMainScene();
        ImpactWord.instance.HandleImpactText(NameFromIndex(bossSceneIndex));
        yield return new WaitForSeconds(.21f);
        scene.allowSceneActivation = true;
        LevelPreview.instance.HandleLevelPreview(true);
        
    }
    public void OnBossGameStart(BossGame bossGame)
    {
        StartCoroutine(WaitForBossGameEnd(bossGame));
    }
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
        scene.allowSceneActivation = true;
        yield return null;
        OnShrinkMainScene();
        if (remainingLives == 0)
        {
            Invoke(nameof(OnGameOver), .1f);
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
