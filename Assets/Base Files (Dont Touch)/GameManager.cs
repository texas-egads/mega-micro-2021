﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public enum GameState
    {
        TitleScreen, MainGame, EndScreen, Intro
    }
    
    public GameState gameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            switch (value)
            {
                case GameState.TitleScreen:
                    titleScreenListener.Invoke();
                    break;
                case GameState.MainGame:
                    mainGameListener.Invoke();
                    break;
                case GameState.EndScreen:
                    endScreenListener.Invoke();
                    break;
            }
        }
    }

    public UnityEvent titleScreenListener = new UnityEvent();
    public UnityEvent mainGameListener = new UnityEvent();
    public UnityEvent endScreenListener = new UnityEvent();
    
    private GameState _gameState;

    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        //SceneManager.sceneUnloaded += StartGame<SceneManager.GetSceneByName("Title Screen")>;
        //if(SceneManager.GetActiveScene().name == "Main") StartGame();
        //TitleScreenListener.AddListener(StartGame);
    }

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScreen": gameState = GameState.TitleScreen;
                break;
            case "Main": gameState = GameState.MainGame;
                break;
            case "End": gameState = GameState.EndScreen;
                break;
        }
    }

    public void LoadScene(string scene)
    {
        switch (scene)
        {
            case "TitleScreen":
                gameState = GameState.TitleScreen;
                break;
            case "Main":
                gameState = GameState.MainGame;
                break;
            case "End":
                gameState = GameState.EndScreen;
                break;
            default:
                print("Wrong scene name buddy: " + scene);
                break;
        }
        SceneManager.LoadScene(scene);
    }
}
