using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState gameState { get; private set; }
    public GameMode gameMode { get; private set; }
    public WordGenerator.Theme theme { get; private set; }
    UIManager uiManager;
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
                uiManager.NextUIGroup("NonPlay", "Play");
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
        uiManager = ServiceLocator.Instance.uiManager;
        SetGameState(GameState.MainMenu);
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
