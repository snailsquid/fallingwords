using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState gameState { get; private set; } = GameState.MainMenu;
    public GameMode gameMode { get; private set; }
    void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        if (gameState == GameState.Playing)
        {
            // Start the game
        }
    }
    void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }
    public void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }
    public enum GameMode
    {
        Time,
        Word,
        Endless
    }
}
