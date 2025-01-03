using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState gameState;
    public GameMode gameMode { get; private set; }
    public WordGenerator.Theme theme { get; private set; }

    UIManager uiManager;
    GameModeManager gameModeManager;
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        switch (gameState)
        {
            case GameState.MainMenu:
                // Show the main menu
                uiManager.NextUI("GameOver", "MainMenu");
                break;
            case GameState.Playing:
                // Start the game
                uiManager.SetActiveGroup("NonPlay", false);
                uiManager.SetGameModeUI(gameMode);
                gameModeManager.StartGameMode(gameMode);
                ServiceLocator.Instance.fallingWordManager.StartGame(theme);
                break;
            case GameState.GameOver:
                // Show the game over screen
                uiManager.SetActiveGroup("Play", false);
                uiManager.SetActive("GameOver", true);
                break;
            case GameState.ModeSelect:
                // Show the mode select screen
                uiManager.NextUI("MainMenu", "ModeSelect");
                break;
            case GameState.ThemeSelect:
                // Show the theme select screen
                uiManager.NextUI("ModeSelect", "ThemeSelect");
                break;
        }
    }
    void Start()
    {
        ServiceLocator serviceLocator = ServiceLocator.Instance;
        uiManager = serviceLocator.uiManager;
        SetGameState(GameState.MainMenu);
        gameModeManager = serviceLocator.gameModeManager;
        // Debug stuff
        // gameState = GameState.Playing;
        // ServiceLocator.Instance.fallingWordManager.StartGame(WordGenerator.Theme.EverydayItems);
    }
    public void SetTheme(WordGenerator.Theme theme)
    {
        this.theme = theme;
    }
    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }
    public void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
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
        GameOver,
        ModeSelect,
        ThemeSelect
    }
    public enum GameMode
    {
        Time,
        Word,
        Endless
    }
}
